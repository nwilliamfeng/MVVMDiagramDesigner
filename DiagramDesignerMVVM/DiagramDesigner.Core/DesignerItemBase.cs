﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace DiagramDesigner
{
    /// <summary>
    ///  设计项基类
    /// </summary>
    public abstract class DesignerItemBase : NotifyObject
    {
        private bool isSelected;

        public DesignerItemBase(int id, IDiagram parent):this()
        {
            this.Id = id;
            this.Parent = parent;           
        }

        public DesignerItemBase()=> SelectItemCommand = new SimpleCommand(ExecuteSelectItemCommand);


        public List<DesignerItemBase> SelectedItems=> Parent.SelectedItems;
        

        public IDiagram Parent { get; set; }

        public ICommand SelectItemCommand { get; }

        public int Id { get; set; }

        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                if (isSelected != value)
                {
                    
                    isSelected = value;
                    NotifyOfPropertyChange("IsSelected");
                }
            }
        }

        private void ExecuteSelectItemCommand(object param)
        {
            SelectItem((bool)param, !IsSelected);
        }
        
        private void SelectItem(bool newselect, bool select)
        {
            if (newselect)
            {
                foreach (var designerItemViewModelBase in Parent.SelectedItems.ToList())
                {
                    designerItemViewModelBase.isSelected = false;
                }
            }

            IsSelected = select;
        }
    
        
    }
}
