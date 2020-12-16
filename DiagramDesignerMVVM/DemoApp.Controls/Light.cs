using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DemoApp.Controls
{
    
    public class Light : Control
    {
      
        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register(nameof(IsOpen),typeof(bool),typeof(Light));


        public bool IsOpen
        {
            get => (bool)this.GetValue(IsOpenProperty);
            set => this.SetValue(IsOpenProperty, value);
        } 
    }
}
