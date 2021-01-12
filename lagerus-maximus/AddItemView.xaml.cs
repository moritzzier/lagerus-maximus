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
    /// Interaktionslogik für AddItem.xaml
    /// </summary>
    public partial class AddItemView : Window
    {
        public AddItemViewModel ViewModel => Resources["ViewModel"] as AddItemViewModel;

        public AddItemView()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e )
        {
            if(DataContext is ICloseWindow vm)
            {
                vm.Close += () =>
                {
                    this.Close();
                };
            }
        }
    }
}
