using System;
using System.Collections.Generic;
using System.Linq;

namespace DiagramDesigner
{

    /// <summary>
    /// 元件设计项基类
    /// </summary>
    public abstract class DesignerElement : VisualElement
    {
        private double _left;
        private double _top;
        private bool _showConnectors = false;
        private List<FullyCreatedConnectorInfo> _connectors = new List<FullyCreatedConnectorInfo>();

        private double _itemWidth = 65;
        private double _itemHeight = 65;

        public DesignerElement(int id, IDiagram parent, double left, double top) : base(id, parent)
        {
            this._left = left;
            this._top = top;
            Init();
        }

        public DesignerElement(int id, IDiagram parent, double left, double top, double itemWidth, double itemHeight) : base(id, parent)
        {
            this._left = left;
            this._top = top;
            this._itemWidth = itemWidth;
            this._itemHeight = itemHeight;
            Init();
        }

        public DesignerElement() : base()
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

     
        public FullyCreatedConnectorInfo TopConnector => _connectors[0];

        public FullyCreatedConnectorInfo BottomConnector => _connectors[1];

        public FullyCreatedConnectorInfo LeftConnector => _connectors[2];

        public FullyCreatedConnectorInfo RightConnector => _connectors[3];

        public FullyCreatedConnectorInfo GetConnectorInfo(ConnectorOrientation connectorOrientation)
        {
            switch (connectorOrientation)
            {
                case ConnectorOrientation.None:
                    return null;
                case ConnectorOrientation.Left:
                    return LeftConnector;
                case ConnectorOrientation.Right:
                    return RightConnector;
                case ConnectorOrientation.Top:
                    return TopConnector;
                case ConnectorOrientation.Bottom:
                    return BottomConnector;
            }
            return null;
        }

        public bool ShowConnectors
        {
            get
            {
                return _showConnectors;
            }
            set
            {
                if (_showConnectors != value)
                {
                    _showConnectors = value;
                    TopConnector.ShowConnectors = value;
                    BottomConnector.ShowConnectors = value;
                    RightConnector.ShowConnectors = value;
                    LeftConnector.ShowConnectors = value;
                    NotifyOfPropertyChange(()=> ShowConnectors);
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
            _connectors.Add(new FullyCreatedConnectorInfo(this, ConnectorOrientation.Top));
            _connectors.Add(new FullyCreatedConnectorInfo(this, ConnectorOrientation.Bottom));
            _connectors.Add(new FullyCreatedConnectorInfo(this, ConnectorOrientation.Left));
            _connectors.Add(new FullyCreatedConnectorInfo(this, ConnectorOrientation.Right));
        }

    }
}
