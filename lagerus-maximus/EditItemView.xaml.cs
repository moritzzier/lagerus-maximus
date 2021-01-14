using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace lagerus_maximus
{
    /// <summary>
    /// Interaction logic for EditItemView.xaml
    /// </summary>
    public partial class EditItemView : Window
    {
        public EditItemViewModel ViewModel => Resources["ViewModel"] as EditItemViewModel;

        public EditItemView()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is ICloseWindow vm)
            {
                vm.Close += () =>
                {
                    this.Close();
                };
            }
        }
    }
}
