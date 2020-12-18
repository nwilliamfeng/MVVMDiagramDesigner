using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiagramDesigner
{
    /// <summary>
    /// 连接器线条类型
    /// </summary>
    public enum ConnectorLineType
    {
        /// <summary>
        /// 实线
        /// </summary>
        Solid=0,

        /// <summary>
        /// 虚线
        /// </summary>
        Dash=1,

        /// <summary>
        /// 支持动画的虚线
        /// </summary>
        DynamicDash=2,
    }
}
