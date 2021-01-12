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

        private ObservableCollection<Item> m_completeCollection = new ObservableCollection<Item>();
        /// <summary>
        /// hat alle items, zu jeder zeit, nie entfernen auser remove befeh. danke
        /// </summary>
        public ObservableCollection<Item> CompleteCollection
        {
            get => m_completeCollection;

            set
            {
                m_completeCollection = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> m_filterCollection = new ObservableCollection<string>();
        /// <summary>
        /// beinhaltet die filter nachdenen ...naja...gefilter wird
        /// </summary>
        public ObservableCollection<string> FilterCollection
        {
            get => m_filterCollection;

            set
            {
                m_filterCollection = value;
                OnPropertyChanged();
            }
        }

        private AddItemView m_addItemView = new AddItemView();


        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand RemoveCommand { get; }

        public ICommand SelectedFilterChangedCommand { get; }

        public MainWindowViewModel()
        {
            AddCommand = new DelegateCommand<Item>(OnAddItem);
            EditCommand = new DelegateCommand<Item>(OnEditItem);
            RemoveCommand = new DelegateCommand<Item>(OnRemoveItem);
            SelectedFilterChangedCommand = new DelegateCommand<string>(OnSelectedFilterChanged);
            CategoryCollection =  new ObservableCollection<string>() { "TEST", "Salben","Tabletten","Pillen", "Zäpfchen","Tee","Sonstige" };

            foreach(string s in CategoryCollection)
            {
                FilterCollection.Add(s);
            }            
            FilterCollection.Insert(0, "All");
            OnSelectedFilterChanged("All");
        }

        public void Initialize()
        {

        }

        public void OnAddItem(Item item)
        {
            m_addItemView = new AddItemView();            
            m_addItemView.ViewModel.CategoryCollection = CategoryCollection;
            m_addItemView.ShowDialog();

            if(m_addItemView.ViewModel.WindowClose == true)
            {
                return;
            }

            CompleteCollection.Add(m_addItemView.ViewModel.Item);
            CompleteCollection = CompleteCollection;
        }

        public void OnSelectedFilterChanged(string filter)
        {
            SelectedCollection.Clear();

            foreach (Item item in CompleteCollection)
            {
                if (item.Category == filter)
                {
                    SelectedCollection.Add(item);
                }
            }


            if (filter == "All")
            {
                foreach (Item item in CompleteCollection)
                {
                    SelectedCollection.Add(item);
                }
            }
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
