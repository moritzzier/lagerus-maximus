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
    public class MainWindowViewModel : INotifyPropertyChanged
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

        private ObservableCollection<Item> m_selectedCollection = new ObservableCollection<Item>();
        /// <summary>
        /// Die Collection an Items die Angezeigt werden soll, die einzige Collection die Sichbar ist. Items die gezéit werden soll hinzufügen, die die nicht entfernen
        /// </summary>
        public ObservableCollection<Item> SelectedCollection
        {
            get => m_selectedCollection;

            set
            {
                m_selectedCollection = value;
                OnPropertyChanged();
            }
        }

        private AddItemView m_addItemView = new AddItemView();


        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand RemoveCommand { get; }

        public MainWindowViewModel()
        {
            AddCommand = new DelegateCommand<Item>(OnAddItem);
            EditCommand = new DelegateCommand<Item>(OnEditItem);
            RemoveCommand = new DelegateCommand<Item>(OnRemoveItem);
        }

        public void Initialize()
        {

        }

        public void OnAddItem(Item item)
        {
            m_addItemView = new AddItemView();
            CategoryCollection.Add("TEST");
            m_addItemView.ViewModel.CategoryCollection = CategoryCollection;
            m_addItemView.Show();
        }

        public void OnEditItem(Item item)
        {
            if (item != null)
            {
                   // Warehouse.EditItem(Item);
                
            }
            else
            {
                MessageBox.Show($"Please select an item to edit.", "No item found", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void OnRemoveItem(Item item)
        {
            if(item != null)
            {
                if(MessageBoxResult.Yes == MessageBox.Show($"Are you sure you want to remove the item(s) {item.Name} permanently?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Warning))
                {
                    //Warehouse.RemoveItem(Item);
                }
            }
            else
            {
                MessageBox.Show($"Please select an item to remove.", "No item found", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
