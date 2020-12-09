using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;


namespace DiagramDesigner
{
    public class DiagramViewModel : NotifyObject, IDiagram
    {
        private ObservableCollection<SelectableDesignerItem> items = new ObservableCollection<SelectableDesignerItem>();

        public DiagramViewModel()
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
            foreach (var item in Items.OfType<DesignerItemViewModelBase>())
            {
                item.ShowConnectors = false;
            }
        }


        public SimpleCommand AddItemCommand { get; private set; }
        public SimpleCommand RemoveItemCommand { get; private set; }
        public SimpleCommand ClearSelectedItemsCommand { get; private set; }
        public SimpleCommand CreateNewDiagramCommand { get; private set; }

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
