using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace DiagramDesigner
{
    /// <summary>
    /// 当一个DesignerElement尝试连接另外一个时将会创建此类
    /// </summary>
    public class PartCreatedConnectionInfo : ConnectorInfoBase
    {
        public Point CurrentLocation { get; private set; }

        public PartCreatedConnectionInfo(Point currentLocation) : base(ConnectorOrientation.None)
        {
            this.CurrentLocation = currentLocation;
        }
    }
}
