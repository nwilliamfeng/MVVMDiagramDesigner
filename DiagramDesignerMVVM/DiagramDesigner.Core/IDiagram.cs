using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace DiagramDesigner
{
    /// <summary>
    /// 图接口
    /// </summary>
    public interface IDiagram
    {
        ICommand AddItemCommand { get; }

        ICommand RemoveItemCommand { get;  }

        ICommand ClearSelectedItemsCommand { get;  }

        List<DesignerItemBase> SelectedItems { get; }

        ObservableCollection<DesignerItemBase> Items { get; }
    }
}
