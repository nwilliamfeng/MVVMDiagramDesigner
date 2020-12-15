using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace DiagramDesigner
{
    /// <summary>
    ///  可视化基类
    /// </summary>
    public abstract class VisualElement : NotifyObject
    {
        private bool _isSelected;

        protected VisualElement(int id, IDiagram parent):this()
        {
            this.Id = id;
            this.Parent = parent;           
        }

        protected VisualElement()
        {
            SelectItemCommand = new InnerCommand(ExecuteSelectItemCommand);
            DoubleClickCommand = new InnerCommand(x => OnDoubleClick());
        }

        /// <summary>
        /// 双击时处理，默认不处理
        /// </summary>
        protected virtual void OnDoubleClick()
        {

        }

        /// <summary>
        /// 获取Diagram的所有项
        /// </summary>
        public List<VisualElement> SelectedItems=> Parent.SelectedItems;
        

        public IDiagram Parent { get; set; }

        public ICommand SelectItemCommand { get; }

        public ICommand DoubleClickCommand { get; }

        private object _doubleClickCommandParameter;

        public object DoubleClickCommandParameter
        {
            get => _doubleClickCommandParameter;
            set => this.Set(ref _doubleClickCommandParameter, value);
        }

        /// <summary>
        /// 获取Id
        /// </summary>
        public int Id { get;  }

        /// <summary>
        /// 获取或设置是否被选中
        /// </summary>
        public bool IsSelected
        {
            get => _isSelected;
            set => this.Set(ref _isSelected, value);
            
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
                    designerItemViewModelBase._isSelected = false;
                }
            }

            IsSelected = select;
        }
    
        
    }
}
