using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NOTAM.Behavior
{
    public class PasswordValidator : FrameworkElement
    {
        static IDictionary<PasswordBox, Brush> _passwordBoxes = new Dictionary<PasswordBox, Brush>();

        public static readonly DependencyProperty Box1Property = DependencyProperty.Register("Box1", typeof(PasswordBox), typeof(PasswordValidator), new PropertyMetadata(Box1Changed));
        public static readonly DependencyProperty Box2Property = DependencyProperty.Register("Box2", typeof(PasswordBox), typeof(PasswordValidator), new PropertyMetadata(Box2Changed));
        public static readonly DependencyProperty ErrorMsgProperty = DependencyProperty.Register("ErrorMsg", typeof(Label), typeof(PasswordValidator), new PropertyMetadata(ErrorMsgChanged));

        public PasswordBox Box1
        {
            get { return (PasswordBox)GetValue(Box1Property); }
            set { SetValue(Box1Property, value); }
        }
        public PasswordBox Box2
        {
            get { return (PasswordBox)GetValue(Box2Property); }
            set { SetValue(Box2Property, value); }
        }
        public Label ErrorMsg
        {
            get { return (Label)GetValue(ErrorMsgProperty); }
            set { SetValue(ErrorMsgProperty, value); }
        }
        private static void Box1Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pv = (PasswordValidator)d;
            pv.Box1.LostFocus += (obj, evt) =>
            {
                CheckErrorMessage(d);
               
            };
        }

        private static void ErrorMsgChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }
        private static void Box2Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pv = (PasswordValidator)d;
            _passwordBoxes[pv.Box2] = pv.Box2.BorderBrush;
            pv.Box2.LostFocus += (obj, evt) =>
            {
                CheckErrorMessage(d);
                if (pv.Box1.Password != pv.Box2.Password)
                {
                    pv.Box2.BorderBrush = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    pv.Box2.BorderBrush = _passwordBoxes[pv.Box2];
                }
            };
        }

        private static void CheckErrorMessage(DependencyObject d)
        {
            var pv = (PasswordValidator)d;
            if (pv.Box1.Password != pv.Box2.Password)
                pv.ErrorMsg.Content = "Passwords are not equal";
            else
                pv.ErrorMsg.Content = string.Empty ;


        }
    }
}
