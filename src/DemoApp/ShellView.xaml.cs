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
    public partial class ShellView : Window
    {
        private ShellViewModel window1ViewModel;

        public ShellView()
        {
            InitializeComponent();

            window1ViewModel = new ShellViewModel();
            this.DataContext = window1ViewModel;
            
        }
 
    }
}
