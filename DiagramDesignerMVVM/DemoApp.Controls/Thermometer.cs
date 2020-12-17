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
    /// 温度计
    /// </summary>
    [TemplatePart(Name = PART_Path, Type = typeof(Path))]
    public class Thermometer : Control
    {
        public const string PART_Path = "PART_Path";

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof(Value), typeof(double), typeof(Thermometer),new PropertyMetadata(0.0,new PropertyChangedCallback(OnPercentPropertyChanged)));

        /// <summary>
        /// 显示百分比(0~100)
        /// </summary>
        public double Value
        {
            get => (double)this.GetValue(ValueProperty);
            set => this.SetValue(ValueProperty, value);
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
            (sender as Thermometer)?.UpdateWithValueChange();
        }

        private void Tank_Loaded(object sender, RoutedEventArgs e)
        {
            (sender as Thermometer)?.UpdateWithValueChange();
        }

        private static void OnPercentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as Thermometer)?.UpdateWithValueChange();
        }

        private void UpdateWithValueChange()
        {       
            if (!(this.GetTemplateChild(PART_Path) is Path path)) return;

            var full = this.ActualWidth < this.ActualHeight ? this.ActualWidth : this.ActualHeight;        
            double percent = (100 - Value) * full / 100;
            if (percent < 0) percent=0;
            if (percent > 100) percent = 100;
            path.Clip = new RectangleGeometry(new Rect(0, percent, full, full));
        }
    }
}
