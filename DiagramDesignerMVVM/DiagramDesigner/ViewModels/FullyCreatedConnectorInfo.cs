using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace DiagramDesigner
{
    public class FullyCreatedConnectorInfo : ConnectorInfoBase
    {
        private bool showConnectors = false;

        public FullyCreatedConnectorInfo(DesignerItemBase dataItem, ConnectorOrientation orientation)
            : base(orientation)
        {
            this.DataItem = dataItem;
        }


        public DesignerItemBase DataItem { get; private set; }

        public bool ShowConnectors
        {
            get
            {
                return showConnectors;
            }
            set
            {
                if (showConnectors != value)
                {
                    showConnectors = value;
                    NotifyOfPropertyChange("ShowConnectors");
                }
            }
        }
    }
}
