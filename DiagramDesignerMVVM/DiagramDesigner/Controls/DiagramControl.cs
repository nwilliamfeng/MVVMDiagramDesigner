using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DiagramDesigner.Controls
{
    /// <summary>
    /// 图控件
    /// </summary>
    public class DiagramControl:ItemsControl
    {
        public static readonly DependencyProperty ShowGridLinesProperty =
           DependencyProperty.Register(nameof(ShowGridLines), typeof(bool), typeof(DiagramControl), new PropertyMetadata(false));

        /// <summary>
        /// 是否显示网格线
        /// </summary>
        public bool ShowGridLines
        {
            get { return (bool)GetValue(ShowGridLinesProperty); }
            set { SetValue(ShowGridLinesProperty, value); }
        }

        
       



    }
}
