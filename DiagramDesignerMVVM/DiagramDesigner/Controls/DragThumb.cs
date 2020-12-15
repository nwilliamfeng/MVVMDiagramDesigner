using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace DiagramDesigner.Controls
{
    public class DragThumb : Thumb
    {
        public DragThumb()
        {
            base.DragDelta += new DragDeltaEventHandler(DragThumb_DragDelta);
        }

        void DragThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            DesignerElement designerItem = this.DataContext as DesignerElement;

            if (designerItem != null && designerItem.IsSelected)
            {
                double minLeft = double.MaxValue;
                double minTop = double.MaxValue;

                // we only move DesignerItems
                var designerItems = designerItem.SelectedItems;

                foreach (DesignerElement item in designerItems.OfType<DesignerElement>())
                {
                    double left = item.Left;
                    double top = item.Top;
                    minLeft = double.IsNaN(left) ? 0 : Math.Min(left, minLeft);
                    minTop = double.IsNaN(top) ? 0 : Math.Min(top, minTop);

                    double deltaHorizontal = Math.Max(-minLeft, e.HorizontalChange);
                    double deltaVertical = Math.Max(-minTop, e.VerticalChange);
                    item.Left += deltaHorizontal;
                    item.Top += deltaVertical;

                    // prevent dragging items out of groupitem
                    if (item.Parent is IDiagram && item.Parent is DesignerElement)
                    {
                        DesignerElement groupItem = (DesignerElement)item.Parent;
                        if (item.Left + item.Width >= groupItem.Width) item.Left = groupItem.Width - item.Width;
                        if (item.Top + item.Height >= groupItem.Height) item.Top = groupItem.Height - item.Height;
                    }

                }
                e.Handled = true;
            }
        }
    }
}
