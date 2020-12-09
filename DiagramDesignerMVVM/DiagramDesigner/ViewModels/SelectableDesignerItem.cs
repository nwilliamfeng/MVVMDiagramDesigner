using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace DiagramDesigner
{

   
    /// <summary>
    /// 支持选中的设计项
    /// </summary>
    public abstract class SelectableDesignerItem : NotifyObject
    {
        private bool isSelected;

        public SelectableDesignerItem(int id, IDiagram parent)
        {
            this.Id = id;
            this.Parent = parent;
            Init();
        }

        public SelectableDesignerItem()
        {
            Init();
        }

        public List<SelectableDesignerItem> SelectedItems
        {
            get { return Parent.SelectedItems; }
        }

        public IDiagram Parent { get; set; }
        public SimpleCommand SelectItemCommand { get; private set; }
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
    
        private void Init()
        {
            SelectItemCommand = new SimpleCommand(ExecuteSelectItemCommand);
        }
    }
}
