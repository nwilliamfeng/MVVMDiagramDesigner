using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiagramDesigner;
using System.Windows.Input;

namespace DemoApp
{
    public class LightViewModel:DesignerElement
    {

        private ActionItem _startActionItem;

        public bool IsOpen
        {
            get => _startActionItem.IsChecked;
          
        }

        public LightViewModel()
        {
            _startActionItem = new ActionItem
            {
                IsCheckable = true,
                Name="打开",
                

            };
            _startActionItem.PropertyChanged += (s,e)=>
            {
                if (e.PropertyName == nameof(ActionItem.IsChecked))
                    this.NotifyOfPropertyChange(() => this.IsOpen);
            };
            this.MenuItems.Add(_startActionItem);
        }

        
    }
}
