using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NOTAM.View
{
    /// <summary>
    /// Interaction logic for NotamView.xaml
    /// </summary>
    public partial class SnowtamView : UserControl
    {
        public SnowtamView()
        {
            InitializeComponent();
        }
        private void ItemLostFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }
        private void FreeTexttTxt_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.FreeTexttTxt.Text))
                {
                    int lineIndexFromCharacterIndex = this.FreeTexttTxt.GetLineIndexFromCharacterIndex(this.FreeTexttTxt.Text.Length);
                    if (this.FreeTexttTxt.GetLineLength(lineIndexFromCharacterIndex) >= 67)
                    {
                        e.Handled = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }


    }
}
