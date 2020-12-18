using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace DiagramDesigner
{
    /// <summary>
    /// 设计器菜单项
    /// </summary>
    public class ActionItem:NotifyObject
    {
        private bool _isChecked;

        public bool IsChecked
        {
            get => _isChecked;
            set => this.Set(ref _isChecked, value);
        }

        private bool _checkable;

        public bool IsCheckable
        {
            get => _checkable;
            set => this.Set(ref _checkable, value);
        }

        /// <summary>
        /// 标识Id
        /// </summary>
        public string Id { get; set; }

        private string _name;

        public string Name
        {
            get => _name;
            set => this.Set(ref _name, value);
        }

        public ObservableCollection<ActionItem> Items { get; private set; } = new ObservableCollection<ActionItem>();

        public ICommand Command { get; set; }

        private object _commandParam;
        public object CommandParameter
        {
            get => _commandParam;
            set => this.Set(ref _commandParam, value);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ActionItem other)) return false;
            return this.Id == other.Id;
        }

        public override int GetHashCode()
        {
            if (Id == null)
                return base.GetHashCode();
            return this.Id.GetHashCode();
        }
    }
}
