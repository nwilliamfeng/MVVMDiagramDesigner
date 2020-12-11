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
    /// 设计器控件
    /// </summary>
    public class DesignerItemControl:ContentControl
    {
        public static readonly DependencyProperty ShowConnectorsProperty = DependencyProperty.Register(nameof(ShowConnectors)
            , typeof(bool)
            , typeof(DesignerItemControl), new PropertyMetadata(false));

        /// <summary>
        /// 是否显示连接器
        /// </summary>
        public bool ShowConnectors
        {
            get => (bool)this.GetValue(ShowConnectorsProperty);
            set => this.SetValue(ShowConnectorsProperty, value);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.MouseMove -= DiagramControl_MouseMove;
            this.MouseMove += DiagramControl_MouseMove;
            this.mouseho
        }

        private void DiagramControl_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.ShowConnectors = true;
            
        }
    }
}
