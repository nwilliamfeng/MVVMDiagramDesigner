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
            , typeof(DesignerItemControl), new  FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        public static readonly DependencyProperty TopConnectorProperty = DependencyProperty.Register(nameof(TopConnector)
            , typeof(FullyCreatedConnectorInfo)
            , typeof(DesignerItemControl));

        public static readonly DependencyProperty LeftConnectorProperty = DependencyProperty.Register(nameof(LeftConnector)
            , typeof(FullyCreatedConnectorInfo)
            , typeof(DesignerItemControl));

        public static readonly DependencyProperty RightConnectorProperty = DependencyProperty.Register(nameof(RightConnector)
          , typeof(FullyCreatedConnectorInfo)
          , typeof(DesignerItemControl));

        public static readonly DependencyProperty BottomConnectorProperty = DependencyProperty.Register(nameof(BottomConnector)
          , typeof(FullyCreatedConnectorInfo)
          , typeof(DesignerItemControl));


        /// <summary>
        /// 是否显示连接器
        /// </summary>
        public bool ShowConnectors
        {
            get => (bool)this.GetValue(ShowConnectorsProperty);
            set => this.SetValue(ShowConnectorsProperty, value);
        }


        public FullyCreatedConnectorInfo TopConnector
        {
            get => (FullyCreatedConnectorInfo)this.GetValue(TopConnectorProperty);
            set => this.SetValue(TopConnectorProperty, value);
        }

        public FullyCreatedConnectorInfo LeftConnector
        {
            get => (FullyCreatedConnectorInfo)this.GetValue(LeftConnectorProperty);
            set => this.SetValue(LeftConnectorProperty, value);
        }

        public FullyCreatedConnectorInfo RightConnector
        {
            get => (FullyCreatedConnectorInfo)this.GetValue(RightConnectorProperty);
            set => this.SetValue(RightConnectorProperty, value);
        }

        public FullyCreatedConnectorInfo BottomConnector
        {
            get => (FullyCreatedConnectorInfo)this.GetValue(BottomConnectorProperty);
            set => this.SetValue(BottomConnectorProperty, value);
        }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.MouseEnter -= DesignerItemControl_MouseEnter;
            this.MouseEnter += DesignerItemControl_MouseEnter;
        }
 

        private void DesignerItemControl_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.ShowConnectors = true;
        }
    }
}
