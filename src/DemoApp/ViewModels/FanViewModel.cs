using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiagramDesigner;
using System.Windows.Input;
using System.ComponentModel;

namespace DemoApp
{
    [Description("风机")]
    public class FanViewModel:DesignerElement
    {

        private ActionItem _startActionItem;

        public bool IsStart
        {
            get => _startActionItem.IsChecked;
          
        }

        public FanViewModel()
        {
            _startActionItem = new ActionItem
            {
                IsCheckable = true,
                Name="启动",
                

            };
            _startActionItem.PropertyChanged += (s,e)=>
            {
                if (e.PropertyName == nameof(ActionItem.IsChecked))
                    this.NotifyOfPropertyChange(() => this.IsStart);
            };
            this.MenuItems.Add(_startActionItem);
        }

        
    }
}
