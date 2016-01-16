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
using System.Windows.Shapes;
using NOTAM.ViewModel;
using NOTAM.Service;

namespace NOTAM.View
{
    /// <summary>
    /// Interaction logic for SnowtamDetailView.xaml
    /// </summary>
    public partial class SnowtamDetailView : UserControl
    {
        public SnowtamDetailView()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as SnowtamDetailViewModel;
            if (vm != null && aftnListView.SelectedItem != null)
            {
                vm.SelectedAftn = aftnListView.SelectedItem as Aftn;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            var vm = DataContext as SnowtamDetailViewModel;
            if (vm != null && aftnSelectedListView.SelectedItem != null)
            {
//                vm.RemovedAftn = aftnSelectedListView.SelectedItem as Aftn;
            }

        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as NotamDetailViewModel;
            if (vm != null && aftnManualListView.SelectedItems != null && aftnManualListView.SelectedItems.Count > 0)
            {
                if (vm.RemovedManualAftns != null)
                    vm.RemovedManualAftns.Clear();
                else
                    vm.RemovedManualAftns = new List<string>();
                foreach (var item in aftnManualListView.SelectedItems)
                    vm.RemovedManualAftns.Add(item as string);
            }

        }

    }
}
