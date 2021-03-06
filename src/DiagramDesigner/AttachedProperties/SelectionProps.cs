﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace DiagramDesigner
{
    public static class SelectionProps
    {
        #region EnabledForSelection

        public static readonly DependencyProperty EnabledForSelectionProperty =
            DependencyProperty.RegisterAttached("EnabledForSelection", typeof(bool), typeof(SelectionProps),
                new FrameworkPropertyMetadata((bool)false,
                    new PropertyChangedCallback(OnEnabledForSelectionChanged)));

        public static bool GetEnabledForSelection(DependencyObject d)
        {
            return (bool)d.GetValue(EnabledForSelectionProperty);
        }

        public static void SetEnabledForSelection(DependencyObject d, bool value)
        {
            d.SetValue(EnabledForSelectionProperty, value);
        }

        private static void OnEnabledForSelectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement fe = (FrameworkElement)d;
            if ((bool)e.NewValue)
            {
                fe.PreviewMouseDown += Fe_PreviewMouseDown;
            }
            else
            {
                fe.PreviewMouseDown -= Fe_PreviewMouseDown;
            }
        }

        #endregion

        static void Fe_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            VisualElement selectableDesignerItemViewModelBase =
                (VisualElement)((FrameworkElement)sender).DataContext;

            if(selectableDesignerItemViewModelBase != null)
            {
                if ((Keyboard.Modifiers & (ModifierKeys.Shift | ModifierKeys.Control)) != ModifierKeys.None)
                {
                    if ((Keyboard.Modifiers & (ModifierKeys.Shift)) != ModifierKeys.None)
                    {
                        selectableDesignerItemViewModelBase.IsSelected = !selectableDesignerItemViewModelBase.IsSelected;
                    }

                    if ((Keyboard.Modifiers & (ModifierKeys.Control)) != ModifierKeys.None)
                    {
                        selectableDesignerItemViewModelBase.IsSelected = !selectableDesignerItemViewModelBase.IsSelected;
                    }
                }
                else if (!selectableDesignerItemViewModelBase.IsSelected)
                {
                    foreach (VisualElement item in selectableDesignerItemViewModelBase.Parent.SelectedItems)
                    {

                        if (item is IDiagram)
                        {
                            IDiagram tmp = (IDiagram)item;
                            foreach (VisualElement gItem in tmp.Items)
                            {
                                gItem.IsSelected = false;
                            }

                        }
                        if (selectableDesignerItemViewModelBase.Parent is VisualElement)
                        {
                            VisualElement tmp = (VisualElement)selectableDesignerItemViewModelBase.Parent;
                            tmp.IsSelected = false;
                        }
                        item.IsSelected = false;
                    }
                    if (selectableDesignerItemViewModelBase is IDiagram)
                    {
                        IDiagram tmp = (IDiagram)selectableDesignerItemViewModelBase;
                        foreach (VisualElement gItem in tmp.Items)
                        {
                            gItem.IsSelected = false;
                        }

                    }
                    selectableDesignerItemViewModelBase.Parent.SelectedItems.Clear();
                    selectableDesignerItemViewModelBase.IsSelected = true;
                }
            }
        }
    }
}
