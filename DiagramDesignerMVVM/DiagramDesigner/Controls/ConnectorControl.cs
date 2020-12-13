using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DiagramDesigner.Controls
{
    public class ConnectorControl:Control
    {
        public static readonly DependencyProperty AreaProperty = DependencyProperty.Register(nameof(Area)
         , typeof(Rect)
         , typeof(ConnectorControl), new PropertyMetadata(default(Rect)));


        public static readonly DependencyProperty PointsProperty = DependencyProperty.Register(nameof(Points)
        , typeof(List<Point>)
        , typeof(ConnectorControl), new PropertyMetadata(default(List<Point>)));

        public static readonly DependencyProperty EndPointProperty = DependencyProperty.Register(nameof(EndPoint)
      , typeof(Point)
      , typeof(ConnectorControl), new PropertyMetadata(default(Point)));


        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(nameof(IsSelected)
       , typeof(bool)
       , typeof(ConnectorControl), new PropertyMetadata(false));

        public Rect Area
        {
            get => (Rect)this.GetValue(AreaProperty);
            set => this.SetValue(AreaProperty, value);
        }

        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => this.SetValue(IsSelectedProperty, value);
        }


        public List<Point> Points
        {
            get => (List<Point>)this.GetValue(PointsProperty);
            set => this.SetValue(PointsProperty, value);
        }

        public  Point  EndPoint
        {
            get => (Point)this.GetValue(EndPointProperty);
            set => this.SetValue(EndPointProperty, value);
        }
    }
}
