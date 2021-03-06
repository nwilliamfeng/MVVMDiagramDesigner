﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace DiagramDesigner
{
    /// <summary>
    ///  用于DesignerElement 端口连接
    ///  we use when we know the actual associated diagram item associated with the Connector, and provides the full set of data 
    ///  required to represent an end of a connection to an actual diagram item Connector.
    /// </summary>
    public class FullyCreatedConnectorInfo : ConnectorInfoBase
    {
        private bool _showConnectors = false;

        public FullyCreatedConnectorInfo(DesignerElement dataItem, ConnectorOrientation orientation)
            : base(orientation)
        {
            this.DataItem = dataItem;
        }


        public DesignerElement DataItem { get; private set; }

        public bool ShowConnectors
        {
            get => _showConnectors;
            set => this.Set(ref _showConnectors, value);
        }
    }
}
