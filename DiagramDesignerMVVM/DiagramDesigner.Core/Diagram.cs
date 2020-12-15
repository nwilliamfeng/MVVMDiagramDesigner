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
        private ObservableCollection<VisualElement> items = new ObservableCollection<VisualElement>();

        public Diagram()
        {
            AddItemCommand = new InnerCommand(ExecuteAddItemCommand);
            
            ClearSelectedItemsCommand = new InnerCommand(ExecuteClearSelectedItemsCommand);
            CreateNewDiagramCommand = new InnerCommand(ExecuteCreateNewDiagramCommand);
            RemoveItemCommand= new InnerCommand(ExecuteRemoveItemCommand);
            Mediator.Instance.Register(this);
        }



        [MediatorMessageSink("DoneDrawingMessage")]
        public void OnDoneDrawingMessage(bool dummy)
        {
            foreach (var item in Items.OfType<DesignerElement>())
            {
                item.ShowConnectors = false;
            }
        }

        public ICommand RemoveItemCommand { get; private set; }

        public ICommand AddItemCommand { get; private set; }
        
        public ICommand ClearSelectedItemsCommand { get; private set; }
        public ICommand CreateNewDiagramCommand { get; private set; }

        public ObservableCollection<VisualElement> Items
        {
            get { return items; }
        }

        public List<VisualElement> SelectedItems
        {
            get { return Items.Where(x => x.IsSelected).ToList(); }
        }

        private void ExecuteAddItemCommand(object parameter)
        {
            if (parameter is VisualElement)
            {
                VisualElement item = (VisualElement)parameter;
                item.Parent = this;
                items.Add(item);
            }
        }

        private void ExecuteRemoveItemCommand(object parameter)
        {
            if (parameter is VisualElement)
            {
                VisualElement item = (VisualElement)parameter;
                items.Remove(item);
            }
        }

        private void ExecuteClearSelectedItemsCommand(object parameter)
        {
            foreach (VisualElement item in Items)
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
            List<VisualElement> connectionsToAlsoRemove = new List<VisualElement>();

            foreach (var connector in this.Items.OfType<Connector>())
            {
                if (items.Contains(connector.SourceConnectorInfo.DataItem))
                    connectionsToAlsoRemove.Add(connector);

                if (items.Contains(((FullyCreatedConnectorInfo)connector.SinkConnectorInfo).DataItem))
                    connectionsToAlsoRemove.Add(connector);
            }
            items.AddRange(connectionsToAlsoRemove);
            foreach (var selectedItem in items)
            {
                this.Items.Remove(selectedItem);
            }
        }
    }
}
