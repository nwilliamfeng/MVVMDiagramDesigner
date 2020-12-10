using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Documents;
using System.Linq;
using System.Collections.Generic;
using DiagramDesigner.Helpers;
using DiagramDesigner;

namespace DemoApp
{
    public partial class Window1 : Window
    {
        private Window1ViewModel window1ViewModel;

        public Window1()
        {
            InitializeComponent();

            window1ViewModel = new Window1ViewModel();
            this.DataContext = window1ViewModel;
            
        }
 
    }
}
