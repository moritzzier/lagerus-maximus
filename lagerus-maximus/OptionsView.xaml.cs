﻿using System;
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
    /// Interaktionslogik für OptionsView.xaml
    /// </summary>
    public partial class OptionsView : Window
    {
        public OptionsViewModel ViewModel => Resources["ViewModel"] as OptionsViewModel;
        
        public OptionsView()        
        {
            InitializeComponent();

            if (DataContext is ICloseWindow vm)
            {
                vm.Close += () =>
                {
                    this.Close();
                };
            }

            ViewModel.Initialize();
        }
    }
}
