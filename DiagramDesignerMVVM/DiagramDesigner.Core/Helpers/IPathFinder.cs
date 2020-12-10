using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace DiagramDesigner
{
     interface IPathFinder
    {
        List<Point> GetConnectionLine(ConnectorInfoMeta source, ConnectorInfoMeta sink, bool showLastLine);
        List<Point> GetConnectionLine(ConnectorInfoMeta source, Point sinkPoint, ConnectorOrientation preferredOrientation);
    }
}
