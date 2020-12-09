using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DiagramDesigner;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace DemoApp
{
    public class GroupingDesignerItemViewModel : DesignerItemViewModelBase, IDiagram
    {

        private ObservableCollection<SelectableDesignerItem> items = new ObservableCollection<SelectableDesignerItem>();

        public GroupingDesignerItemViewModel(int id, IDiagram parent, double left, double top)
            : base(id, parent, left, top)
        {
            Init();
        }

        public GroupingDesignerItemViewModel()
        {
            Init();
        }

        public GroupingDesignerItemViewModel(int id, IDiagram parent, double left, double top, double itemWidth, double itemHeight) : base(id, parent, left, top, itemWidth, itemHeight)
        {
            Init();
        }

        public SimpleCommand AddItemCommand { get; private set; }
        public SimpleCommand RemoveItemCommand { get; private set; }
        public SimpleCommand ClearSelectedItemsCommand { get; private set; }
        public SimpleCommand CreateNewDiagramCommand { get; private set; }



        public ObservableCollection<SelectableDesignerItem> Items
        {
            get { return items; }
        }

        new public List<SelectableDesignerItem> SelectedItems
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


        private void Init()
        {
            AddItemCommand = new SimpleCommand(ExecuteAddItemCommand);
            RemoveItemCommand = new SimpleCommand(ExecuteRemoveItemCommand);
            ClearSelectedItemsCommand = new SimpleCommand(ExecuteClearSelectedItemsCommand);
            CreateNewDiagramCommand = new SimpleCommand(ExecuteCreateNewDiagramCommand);

            this.ShowConnectors = false;
        }
    }
}
