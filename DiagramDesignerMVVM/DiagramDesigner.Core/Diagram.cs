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
        private ObservableCollection<DesignerItemBase> items = new ObservableCollection<DesignerItemBase>();

        public Diagram()
        {
            AddItemCommand = new SimpleCommand(ExecuteAddItemCommand);
            
            ClearSelectedItemsCommand = new SimpleCommand(ExecuteClearSelectedItemsCommand);
            CreateNewDiagramCommand = new SimpleCommand(ExecuteCreateNewDiagramCommand);
            RemoveItemCommand= new SimpleCommand(ExecuteRemoveItemCommand);
            Mediator.Instance.Register(this);
        }



        [MediatorMessageSink("DoneDrawingMessage")]
        public void OnDoneDrawingMessage(bool dummy)
        {
            foreach (var item in Items.OfType<ElementDesignerItem>())
            {
                item.ShowConnectors = false;
            }
        }

        public ICommand RemoveItemCommand { get; private set; }

        public ICommand AddItemCommand { get; private set; }
        
        public ICommand ClearSelectedItemsCommand { get; private set; }
        public ICommand CreateNewDiagramCommand { get; private set; }

        public ObservableCollection<DesignerItemBase> Items
        {
            get { return items; }
        }

        public List<DesignerItemBase> SelectedItems
        {
            get { return Items.Where(x => x.IsSelected).ToList(); }
        }

        private void ExecuteAddItemCommand(object parameter)
        {
            if (parameter is DesignerItemBase)
            {
                DesignerItemBase item = (DesignerItemBase)parameter;
                item.Parent = this;
                items.Add(item);
            }
        }

        private void ExecuteRemoveItemCommand(object parameter)
        {
            if (parameter is DesignerItemBase)
            {
                DesignerItemBase item = (DesignerItemBase)parameter;
                items.Remove(item);
            }
        }

        private void ExecuteClearSelectedItemsCommand(object parameter)
        {
            foreach (DesignerItemBase item in Items)
            {
                item.IsSelected = false;
            }
        }

        private void ExecuteCreateNewDiagramCommand(object parameter)
        {
            Items.Clear();
        }

        public void DeleteSelectedItems()
        {
            var items =this.SelectedItems;
            List<DesignerItemBase> connectionsToAlsoRemove = new List<DesignerItemBase>();

            foreach (var connector in this.Items.OfType<Connector>())
            {
                if (items.Contains(connector.SourceConnectorInfo.DataItem))
                {
                    connectionsToAlsoRemove.Add(connector);
                }

                if (items.Contains(((FullyCreatedConnectorInfo)connector.SinkConnectorInfo).DataItem))
                {
                    connectionsToAlsoRemove.Add(connector);
                }

            }
            items.AddRange(connectionsToAlsoRemove);
            foreach (var selectedItem in items)
            {
                this.Items.Remove(selectedItem);
            }
        }
    }
}
