using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NOTAM.View
{
    public static class Commands
    {
        public static readonly DependencyProperty ListViewDoubleClickProperty =
          DependencyProperty.RegisterAttached("ListViewDoubleClickCommand", typeof(ICommand), typeof(Commands),
                            new PropertyMetadata(new PropertyChangedCallback(AttachOrRemoveListViewDoubleClickEvent)));

        public static ICommand GetListViewDoubleClickCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(ListViewDoubleClickProperty);
        }

        public static void SetListViewDoubleClickCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(ListViewDoubleClickProperty, value);
        }

        public static void AttachOrRemoveListViewDoubleClickEvent(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ListView dataGrid = obj as ListView;
            if (dataGrid != null)
            {
                ICommand cmd = (ICommand)args.NewValue;

                if (args.OldValue == null && args.NewValue != null)
                {
                    dataGrid.MouseDoubleClick += ExecuteListViewDoubleClick;
                }
                else if (args.OldValue != null && args.NewValue == null)
                {
                    dataGrid.MouseDoubleClick -= ExecuteListViewDoubleClick;
                }
            }
        }

        private static void ExecuteListViewDoubleClick(object sender, MouseButtonEventArgs args)
        {
             DependencyObject obj = sender as DependencyObject;
            ICommand cmd = (ICommand)obj.GetValue(ListViewDoubleClickProperty);
            if (cmd != null)
            {
                if (cmd.CanExecute(obj))
                {
                    cmd.Execute((sender as ListBox).SelectedItems[0]);
                }
            }


            //DependencyObject obj = sender as DependencyObject;
            //ICommand cmd = (ICommand)obj.GetValue(ListViewDoubleClickProperty);
            //if (cmd != null)
            //{
            //    if (cmd.CanExecute(obj))
            //    {
            //        cmd.Execute(obj);
            //    }
            //}
        }

    }
}
