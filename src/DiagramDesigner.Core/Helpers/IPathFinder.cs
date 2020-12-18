using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace DiagramDesigner
{
     interface IPathFinder
    {
        PointCollection GetConnectionLine(ConnectorInfoMeta source, ConnectorInfoMeta sink, bool showLastLine);
        PointCollection GetConnectionLine(ConnectorInfoMeta source, Point sinkPoint, ConnectorOrientation preferredOrientation);
    }
}
