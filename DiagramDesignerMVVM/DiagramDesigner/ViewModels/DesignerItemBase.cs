using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace DiagramDesigner
{



    /// <summary>
    /// 所有显示在图上的设计项基类
    /// </summary>
    public abstract class DesignerItemBase : SelectableDesignerItem
    {
        private double left;
        private double top;
        private bool showConnectors = false;
        private List<FullyCreatedConnectorInfo> connectors = new List<FullyCreatedConnectorInfo>();

        private double itemWidth = 65;
        private double itemHeight = 65;

        public DesignerItemBase(int id, IDiagram parent, double left, double top) : base(id, parent)
        {
            this.left = left;
            this.top = top;
            Init();
        }

        public DesignerItemBase(int id, IDiagram parent, double left, double top, double itemWidth, double itemHeight) : base(id, parent)
        {
            this.left = left;
            this.top = top;
            this.itemWidth = itemWidth;
            this.itemHeight = itemHeight;
            Init();
        }

        public DesignerItemBase(): base()
        {
            Init();
        }

        public double ItemWidth
        {
            get
            {
                return itemWidth;
            }
            set
            {
                if (itemWidth != value)
                {
                    itemWidth = value;
                    NotifyOfPropertyChange("ItemWidth");
                }
            }
        }

        public double ItemHeight
        {
            get
            {
                return itemHeight;
            }
            set
            {
                if (itemHeight != value)
                {
                    itemHeight = value;
                    NotifyOfPropertyChange("ItemHeight");
                }
            }
        }

        public FullyCreatedConnectorInfo TopConnector
        {
            get { return connectors[0]; }
        }


        public FullyCreatedConnectorInfo BottomConnector
        {
            get { return connectors[1]; }
        }


        public FullyCreatedConnectorInfo LeftConnector
        {
            get { return connectors[2]; }
        }


        public FullyCreatedConnectorInfo RightConnector
        {
            get { return connectors[3]; }
        }


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
                    TopConnector.ShowConnectors = value;
                    BottomConnector.ShowConnectors = value;
                    RightConnector.ShowConnectors = value;
                    LeftConnector.ShowConnectors = value;
                    NotifyOfPropertyChange("ShowConnectors");
                }
            }
        }


        public double Left
        {
            get
            {
                return left;
            }
            set
            {
                if (left != value)
                {
                    left = value;
                    NotifyOfPropertyChange("Left");
                }
            }
        }

        public double Top
        {
            get
            {
                return top;
            }
            set
            {
                if (top != value)
                {
                    top = value;
                    NotifyOfPropertyChange("Top");
                }
            }
        }


        private void Init()
        {
            connectors.Add(new FullyCreatedConnectorInfo(this, ConnectorOrientation.Top));
            connectors.Add(new FullyCreatedConnectorInfo(this, ConnectorOrientation.Bottom));
            connectors.Add(new FullyCreatedConnectorInfo(this, ConnectorOrientation.Left));
            connectors.Add(new FullyCreatedConnectorInfo(this, ConnectorOrientation.Right));
        }

    }
}
