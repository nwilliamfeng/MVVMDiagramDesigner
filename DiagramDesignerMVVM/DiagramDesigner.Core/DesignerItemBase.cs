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
        private double _left;
        private double _top;
        private bool showConnectors = false;
        private List<FullyCreatedConnectorInfo> connectors = new List<FullyCreatedConnectorInfo>();

        private double _itemWidth = 65;
        private double _itemHeight = 65;

        public DesignerItemBase(int id, IDiagram parent, double left, double top) : base(id, parent)
        {
            this._left = left;
            this._top = top;
            Init();
        }

        public DesignerItemBase(int id, IDiagram parent, double left, double top, double itemWidth, double itemHeight) : base(id, parent)
        {
            this._left = left;
            this._top = top;
            this._itemWidth = itemWidth;
            this._itemHeight = itemHeight;
            Init();
        }

        public DesignerItemBase(): base()
        {
            Init();
        }

        public double ItemWidth
        {
            get => _itemWidth;
            set => this.Set(ref _itemWidth, value);
        }

        public double ItemHeight
        {
            get => _itemHeight;
            set => this.Set(ref _itemHeight, value);            
        }

        public FullyCreatedConnectorInfo TopConnector=> connectors[0];

        public FullyCreatedConnectorInfo BottomConnector=> connectors[1];

        public FullyCreatedConnectorInfo LeftConnector => connectors[2];

        public FullyCreatedConnectorInfo RightConnector=> connectors[3];

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
            get => _left;
            set => this.Set(ref _left, value);          
        }

        public double Top
        {
            get => _top;
            set => this.Set(ref _top, value);           
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
