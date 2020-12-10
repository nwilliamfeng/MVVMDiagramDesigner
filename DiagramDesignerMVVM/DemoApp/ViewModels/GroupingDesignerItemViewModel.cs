﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DiagramDesigner;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace DemoApp
{
    public class GroupingDesignerItemViewModel : ElementDesignerItem, IDiagram
    {

       

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

        public ICommand AddItemCommand { get; private set; }
        public ICommand RemoveItemCommand { get; private set; }
        public ICommand ClearSelectedItemsCommand { get; private set; }
        public ICommand CreateNewDiagramCommand { get; private set; }



        public ObservableCollection<DesignerItemBase> Items { get; private set; } = new ObservableCollection<DesignerItemBase>();
       

        new public List<DesignerItemBase> SelectedItems
        {
            get { return Items.Where(x => x.IsSelected).ToList(); }
        }

        private void ExecuteAddItemCommand(object parameter)
        {
            if (parameter is DesignerItemBase)
            {
                DesignerItemBase item = (DesignerItemBase)parameter;
                item.Parent = this;
                Items.Add(item);
            }
        }

        private void ExecuteRemoveItemCommand(object parameter)
        {
            if (parameter is DesignerItemBase)
            {
                DesignerItemBase item = (DesignerItemBase)parameter;
                Items.Remove(item);
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
