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


        public static readonly DependencyProperty LineThicknessProperty = DependencyProperty.Register(nameof(LineThickness)
            , typeof(double)
            , typeof(ConnectorControl), new PropertyMetadata(2.0));

        public static readonly DependencyProperty LineTypeProperty = DependencyProperty.Register(nameof(LineType)
           , typeof(ConnectorLineType)
           , typeof(ConnectorControl), new PropertyMetadata(ConnectorLineType.Solid));

        public static readonly DependencyProperty ArrowOrientationProperty = DependencyProperty.Register(nameof(ArrowOrientation)
        , typeof(ConnectorOrientation)
        , typeof(ConnectorControl), new PropertyMetadata(ConnectorOrientation.None));

        public static readonly DependencyProperty ShowArrowProperty = DependencyProperty.Register(nameof(ShowArrow)
      , typeof(bool)
      , typeof(ConnectorControl), new PropertyMetadata(false));

        public Rect Area
        {
            get => (Rect)this.GetValue(AreaProperty);
            set => this.SetValue(AreaProperty, value);
        }

        /// <summary>
        /// 显示箭头
        /// </summary>
        public bool ShowArrow
        {
            get =>(bool) this.GetValue(ShowArrowProperty);
            set => this.SetValue(ShowArrowProperty, value);
        }

        /// <summary>
        /// 连接线厚度，默认为2
        /// </summary>
        public double LineThickness
        {
            get => (double)GetValue(LineThicknessProperty);
            set => this.SetValue(LineThicknessProperty, value);
        }

        /// <summary>
        /// 连接线类型
        /// </summary>
        public ConnectorLineType LineType
        {
            get => (ConnectorLineType)GetValue(LineTypeProperty);
            set => this.SetValue(LineTypeProperty, value);
        }

        /// <summary>
        /// 箭头方向
        /// </summary>
        public ConnectorOrientation ArrowOrientation
        {
            get => (ConnectorOrientation)GetValue(ArrowOrientationProperty);
            set => this.SetValue(ArrowOrientationProperty, value);
        }

        /// <summary>
        /// 是否选中
        /// </summary>
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
