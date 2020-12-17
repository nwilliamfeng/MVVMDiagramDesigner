using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DemoApp.Controls
{
    /// <summary>
    /// 水箱
    /// </summary>
    [TemplatePart(Name = PART_Percent_Ellipse, Type = typeof(Ellipse))]
    public class Tank : Control
    {
        public const string PART_Percent_Ellipse = "PART_Percent_Ellipse";

        public static readonly DependencyProperty PercentProperty = DependencyProperty.Register(nameof(Percent), typeof(double), typeof(Tank),new PropertyMetadata(0.0,new PropertyChangedCallback(OnPercentPropertyChanged)));

        /// <summary>
        /// 显示百分比(0~100)
        /// </summary>
        public double Percent
        {
            get => (double)this.GetValue(PercentProperty);
            set => this.SetValue(PercentProperty, value);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.Loaded -= Tank_Loaded;
            this.Loaded += Tank_Loaded;
            this.SizeChanged -= Tank_SizeChanged;
            this.SizeChanged += Tank_SizeChanged;
        }

        private void Tank_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            (sender as Tank)?.UpdateWithPercentChange();
        }

        private void Tank_Loaded(object sender, RoutedEventArgs e)
        {
            (sender as Tank)?.UpdateWithPercentChange();
        }

        private static void OnPercentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as Tank)?.UpdateWithPercentChange();
        }

        private void UpdateWithPercentChange()
        {
            
            if (!(this.GetTemplateChild(PART_Percent_Ellipse) is Ellipse ellipse)) return;

            var full = this.ActualWidth > this.ActualHeight ? this.ActualWidth : this.ActualHeight;
            var percent = (double)(100-this.Percent) * full / 100;
            if (percent < 0) percent=0;
            if (percent > 100) percent = 100;
            ellipse.Clip = new RectangleGeometry(new Rect(0, percent, full, full));
        }
    }
}
