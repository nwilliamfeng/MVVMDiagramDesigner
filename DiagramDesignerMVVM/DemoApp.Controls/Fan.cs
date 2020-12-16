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
    
    public class Fan : Control
    {
      
        public static readonly DependencyProperty IsRunningProperty = DependencyProperty.Register(nameof(IsRunning),typeof(bool),typeof(Fan));


        public bool IsRunning
        {
            get => (bool)this.GetValue(IsRunningProperty);
            set => this.SetValue(IsRunningProperty, value);
        } 
    }
}
