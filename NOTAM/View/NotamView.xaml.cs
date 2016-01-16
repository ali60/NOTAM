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
using System.Collections;

namespace NOTAM.View
{
    /// <summary>
    /// Interaction logic for NotamView.xaml
    /// </summary>
    public partial class NotamView : UserControl
    {
        public NotamView()
        {
            InitializeComponent();

            
        }


        private void refNumTxt_LostFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).GetBindingExpression(TextBox.TextProperty).UpdateSource();

        }

        private void refNumTxt_TouchEnter(object sender, TouchEventArgs e)
        {

        }

        private void eFreeTxt_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
//                IList dictionaries = SpellCheck.GetCustomDictionaries(eFreeTxt);
//                dictionaries.Add(new Uri(@"pack://application:,,,/WPFCustomDictionary;component/DictionaryTest.lex"));
            if (string.IsNullOrEmpty(eFreeTxt.Text))
                return;
            int CurrentLine = eFreeTxt.GetLineIndexFromCharacterIndex(eFreeTxt.Text.Length); //Initialize a new int of name CurrentLine to get the current line the user is on
            if (eFreeTxt.GetLineLength(CurrentLine) >= 67) //Continue if the length of the current line is more or equal to 59
            {
                e.Handled = true; //Don't insert the character
            }//             int textLength = eFreeTxt.Text.Length;
            }
            catch (System.Exception ex)
            {
            //    MessageBox.Show(ex.Message);
            }
            //             string str= eFreeTxt.Text.Replace("\r\n","\n");
//             int iBeg= str.IndexOf("\n");
//             int iEnd=textLength;
//             if(iBeg<0 && textLength<67)
//                 return;
//             while(iBeg < iEnd)
//             {
//                 iEnd = str.IndexOf("\n",iBeg+1);
//                 if(iEnd<0)
//                     iEnd = textLength;
//                 if ((iEnd-iBeg)>= 67 )
//                 {
//                     str= str.Insert(iEnd, "\n");
//                 }
//                 iBeg = iEnd;
//             }
//             eFreeTxt.Text = str.Replace("\n","\r\n");
//            eFreeTxt.SelectionStart = eFreeTxt.Text.Length ;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            int CurrentLine = eFreeTxt.GetLineIndexFromCharacterIndex(eFreeTxt.Text.Length); //Initialize a new int of name CurrentLine to get the current line the user is on
            if (eFreeTxt.GetLineLength(CurrentLine) >= 67) //Continue if the length of the current line is more or equal to 59
            {
                e.Handled = true; //Don't insert the character
            }

        }

        private void dFreeTxt_KeyDown(object sender, KeyEventArgs e)
        {
            int CurrentLine = dFreeTxt.GetLineIndexFromCharacterIndex(dFreeTxt.Text.Length); //Initialize a new int of name CurrentLine to get the current line the user is on
            if (dFreeTxt.GetLineLength(CurrentLine) >= 67) //Continue if the length of the current line is more or equal to 59
            {
                e.Handled = true; //Don't insert the character
            }

        }

        private void fFreeTxt_KeyDown(object sender, KeyEventArgs e)
        {
            int CurrentLine = fFreeTxt.GetLineIndexFromCharacterIndex(fFreeTxt.Text.Length); //Initialize a new int of name CurrentLine to get the current line the user is on
            if (fFreeTxt.GetLineLength(CurrentLine) >= 67) //Continue if the length of the current line is more or equal to 59
            {
                e.Handled = true; //Don't insert the character
            }

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var isChecked = (sender as CheckBox).IsChecked ?? false;
            if (isChecked)
                MagnifiyingGlass.Visibility = Visibility.Visible;
            else
                MagnifiyingGlass.Visibility = Visibility.Hidden;
        }      
    }
}
