using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DiagramDesigner;

namespace DemoApp
{
    /// <summary>
    /// This is passed to the PopupWindow.xaml window, where a DataTemplate is used to provide the
    /// ContentControl with the look for this data. This class is also used to allow
    /// the popup to be cancelled without applying any changes to the calling ViewModel
    /// whos data will be updated if the PopupWindow.xaml window is closed successfully
    /// </summary>
    public class PersistDesignerItemData: NotifyObject
    {
        private string _hostUrl = "";

        public PersistDesignerItemData(string currentHostUrl)
        {
            _hostUrl = currentHostUrl;
        }

        public string HostUrl
        {
            get
            {
                return _hostUrl;
            }
            set
            {
                this.Set(ref _hostUrl, value);
            }
        }
    }
}
