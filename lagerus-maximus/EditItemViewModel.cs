using Microsoft.VisualStudio.PlatformUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace lagerus_maximus
{
    public class EditItemViewModel : INotifyPropertyChanged, ICloseWindow
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<string> m_categoryCollection = new ObservableCollection<string>();
        /// <summary>
        /// All available Categorys
        /// </summary>
        public ObservableCollection<string> CategoryCollection
        {
            get => m_categoryCollection;

            set
            {
                m_categoryCollection = value;
                OnPropertyChanged();
            }
        }

        public bool WindowClose = false;

        private Item m_Item = new Item();

        public Item Item
        {
            get => m_Item;

            set
            {
                m_Item = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveChangesCommand { get; }
        public ICommand CancelCommand { get; }

        public EditItemViewModel()
        {
            SaveChangesCommand = new DelegateCommand(OnSaveChanges);
            CancelCommand = new DelegateCommand(OnCancelItem);
        }

        public void OnSaveChanges()
        {
            if (CheckItemInputValid())
            {
                WindowClose = false;
                Close?.Invoke();
            }
        }

        public void OnCancelItem()
        {
            WindowClose = true;
            Close?.Invoke();
        }


        private bool CheckItemInputValid()
        {
            string output = "Following issues prevent the item from being added:\n\n\n";
            bool checkResult = true;


            if (string.IsNullOrEmpty(Item.Name))
            {
                output += $"Please enter the name of the item.\n\n";
                checkResult = false;
            }

            if (0 >= Item.Quantity)
            {
                output += $"Please enter the quantity of the item.\n\n";
                checkResult = false;
            }

            if (string.IsNullOrEmpty(Item.Category))
            {
                output += $"Please select the category of the item.\n\n";
                checkResult = false;
            }

            if (0 >= Item.ItemNumber)
            {
                output += $"Please enter the item number of the item.\n\n";
                checkResult = false;
            }

            if (!checkResult)
            {
                MessageBox.Show(output, "Item could not be added.", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return checkResult;
        }

        public Action Close { get; set; }

        public bool CanClose()
        {
            return false;
        }
    }
}
