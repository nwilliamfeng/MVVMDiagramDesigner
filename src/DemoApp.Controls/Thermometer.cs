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
    [TemplatePart(Name = PART_Background_Path, Type = typeof(Path))]
    [TemplatePart(Name = PART_Outline_Path, Type = typeof(Path))]
    public class Thermometer : Control
    {
        public const string PART_Path = "PART_Path";
        public const string PART_Background_Path = "PART_Background_Path";
        public const string PART_Outline_Path = "PART_Outline_Path";

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof(Value), typeof(double), typeof(Thermometer),new PropertyMetadata(0.0,new PropertyChangedCallback(OnPercentPropertyChanged)));

        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register(nameof(Maximum), typeof(double), typeof(Thermometer), new PropertyMetadata(100.0 ));

        public double Maximum
        {
            get => (double)this.GetValue(MaximumProperty);
            set => this.SetValue(MaximumProperty, value);
        }

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

            UpdatePathSize();
            this.UpdateWithValueChange();

        }

        private void UpdatePathSize()
        {
            if (!(this.GetTemplateChild(PART_Path) is Path path)) return;
            if (!(this.GetTemplateChild(PART_Background_Path) is Path path2)) return;
            if (!(this.GetTemplateChild(PART_Outline_Path) is Path path3)) return;
            path.Height = path3.ActualHeight * 0.8;
            path2.Height = path3.ActualHeight * 0.8;
        }

        private void Tank_Loaded(object sender, RoutedEventArgs e)
        {
            UpdatePathSize();
            this.UpdateWithValueChange();
        }

        private static void OnPercentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as Thermometer)?.UpdateWithValueChange();
        }

        private void UpdateWithValueChange()
        {                
            if (!(this.GetTemplateChild(PART_Path) is Path path)) return;
            var maximum = this.Maximum;
            var height =  path.ActualHeight;        
            double value = (maximum - Value) * height / maximum  ;
            if (value < 0) value=0;
            if (value > maximum) value = maximum;
            var width = this.ActualHeight > this.ActualWidth ? ActualHeight : ActualWidth;
            path.Clip = new RectangleGeometry(new Rect(0, value , height, width));
        }
    }
}
