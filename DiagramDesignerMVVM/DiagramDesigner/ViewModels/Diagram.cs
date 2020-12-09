using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DiagramDesigner
{
    public class Diagram : NotifyObject, IDiagram
    {
        private ObservableCollection<SelectableDesignerItem> items = new ObservableCollection<SelectableDesignerItem>();

        public Diagram()
        {
            AddItemCommand = new SimpleCommand(ExecuteAddItemCommand);
            RemoveItemCommand = new SimpleCommand(ExecuteRemoveItemCommand);
            ClearSelectedItemsCommand = new SimpleCommand(ExecuteClearSelectedItemsCommand);
            CreateNewDiagramCommand = new SimpleCommand(ExecuteCreateNewDiagramCommand);

            Mediator.Instance.Register(this);
        }



        [MediatorMessageSink("DoneDrawingMessage")]
        public void OnDoneDrawingMessage(bool dummy)
        {
            foreach (var item in Items.OfType<DesignerItemBase>())
            {
                item.ShowConnectors = false;
            }
        }


        public ICommand AddItemCommand { get; private set; }
        public ICommand RemoveItemCommand { get; private set; }
        public ICommand ClearSelectedItemsCommand { get; private set; }
        public ICommand CreateNewDiagramCommand { get; private set; }

        public ObservableCollection<SelectableDesignerItem> Items
        {
            get { return items; }
        }

        public List<SelectableDesignerItem> SelectedItems
        {
            get { return Items.Where(x => x.IsSelected).ToList(); }
        }

        private void ExecuteAddItemCommand(object parameter)
        {
            if (parameter is SelectableDesignerItem)
            {
                SelectableDesignerItem item = (SelectableDesignerItem)parameter;
                item.Parent = this;
                items.Add(item);
            }
        }

        private void ExecuteRemoveItemCommand(object parameter)
        {
            if (parameter is SelectableDesignerItem)
            {
                SelectableDesignerItem item = (SelectableDesignerItem)parameter;
                items.Remove(item);
            }
        }

        private void ExecuteClearSelectedItemsCommand(object parameter)
        {
            foreach (SelectableDesignerItem item in Items)
            {
                item.IsSelected = false;
            }
        }

        private void ExecuteCreateNewDiagramCommand(object parameter)
        {
            Items.Clear();
        }
    }
}
