﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiagramDesigner
{
    public static class DesignerItemExtension
    {
        /// <summary>
        /// 连接两元素
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="fromOrientation"></param>
        /// <param name="toOrientation"></param>
        public static void Connect(this DesignerElement from, DesignerElement to, ConnectorOrientation fromOrientation, ConnectorOrientation toOrientation)
        {
            Connector con = new Connector(from.GetConnectorInfo(fromOrientation), to.GetConnectorInfo(toOrientation));
            con.Parent = from.Parent;
            con.Parent?.Items.Add(con);
        }
 
    }
}
