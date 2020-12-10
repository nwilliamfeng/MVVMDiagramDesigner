using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace DiagramDesigner
{
    public abstract class ConnectorInfoBase : NotifyObject
    {
        

        public ConnectorInfoBase(ConnectorOrientation orientation)
        {
            this.Orientation = orientation;
        }

        public ConnectorOrientation Orientation { get; private set; }
 
    }


}
