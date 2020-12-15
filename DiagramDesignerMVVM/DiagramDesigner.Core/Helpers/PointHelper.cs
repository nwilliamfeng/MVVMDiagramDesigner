using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace DiagramDesigner
{
    class PointHelper
    {
        /// <summary>
        /// 连接器的Size，默认Width和Height都为8
        /// </summary>
        public static readonly Size ConnectorSize = new Size(8,8);

     

        public static Point GetPointForConnector(FullyCreatedConnectorInfo connector)
        {
            Point point = new Point();

            switch (connector.Orientation)
            {
                case ConnectorOrientation.Top:
                    point = new Point(connector.DataItem.Left + (connector.DataItem.Width / 2), connector.DataItem.Top - (ConnectorSize.Height));
                    break;
                case ConnectorOrientation.Bottom:
                    point = new Point(connector.DataItem.Left + (connector.DataItem.Width / 2), (connector.DataItem.Top + connector.DataItem.Height) + (ConnectorSize.Height / 2));
                    break;
                case ConnectorOrientation.Right:
                    point = new Point(connector.DataItem.Left + connector.DataItem.Width + (ConnectorSize.Width), connector.DataItem.Top + (connector.DataItem.Height / 2));
                    break;
                case ConnectorOrientation.Left:
                    point = new Point(connector.DataItem.Left - ConnectorSize.Width, connector.DataItem.Top + (connector.DataItem.Height / 2));
                    break;
            }

            return point;
        }


    }
}
