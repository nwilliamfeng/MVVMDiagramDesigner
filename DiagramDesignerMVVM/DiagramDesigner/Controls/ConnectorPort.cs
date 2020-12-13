using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DiagramDesigner.Controls
{
    /// <summary>
    /// 连接器端口
    /// </summary>
    public class ConnectorPort : Control
    {
        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(nameof(Orientation)
            , typeof(ConnectorOrientation)
            , typeof(ConnectorPort), new PropertyMetadata(ConnectorOrientation.None));


        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DesignerCanvas canvas = GetDesignerCanvas(this);
            if (canvas != null)
            {
                canvas.SourceConnector = this;
            }
        }

        public ConnectorOrientation Orientation
        {
            get => (ConnectorOrientation)this.GetValue(OrientationProperty);
            set => this.SetValue(OrientationProperty, value);
        }

        // iterate through visual tree to get parent DesignerCanvas
        private DesignerCanvas GetDesignerCanvas(DependencyObject element)
        {
            while (element != null && !(element is DesignerCanvas))
                element = VisualTreeHelper.GetParent(element);

            return element as DesignerCanvas;
        }

    }

}
