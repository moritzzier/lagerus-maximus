using Microsoft.VisualStudio.PlatformUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace lagerus_maximus
{
    public class MainWindowViewModel : INotifyPropertyChanged , ICloseWindow
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        

        string m_missingImagePath = "MissingImage.png";
        string m_imageDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Images");

        public string m_selectedFilter = "All";

        public string SelectedFilter
        {
            get => m_selectedFilter;

            set
            {
                m_selectedFilter = value;
                OnPropertyChanged();                
            }
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
                OnSelectedFilterChanged(SelectedFilter);
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

        private AddItemView m_addItemView;
        private EditItemView m_editItemView;
        private AboutView m_aboutView;
        private XmlReaderWriter m_xmlReaderWriter = new XmlReaderWriter();


        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand CloseCommand { get; }
        public ICommand AboutCommand { get; }


        public ICommand SelectedFilterChangedCommand { get; }

        public MainWindowViewModel()
        {
            AddCommand = new DelegateCommand<Item>(OnAddItem);
            EditCommand = new DelegateCommand<Item>(OnEditItem);
            RemoveCommand = new DelegateCommand<Item>(OnRemoveItem);
            CloseCommand = new DelegateCommand(OnClose);
            AboutCommand = new DelegateCommand(OnAbout);

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
            CompleteCollection = m_xmlReaderWriter.LoadData();

            var path = System.IO.Path.GetDirectoryName(
      System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

            path = path.Substring(6);

            foreach (Item item in CompleteCollection)
            {
                if (!File.Exists(item.ImagePath))
                {
                    item.ImagePath = Path.Combine(m_imageDirectory,m_missingImagePath);
                }
            }
        }

        public void OnAddItem(Item item)
        {
            m_addItemView = new AddItemView();
            m_addItemView.ViewModel.Item = new Item();
            m_addItemView.ViewModel.CategoryCollection = CategoryCollection;
            m_addItemView.ShowDialog();

            if (m_addItemView.ViewModel.WindowClose == true)
            {
                return;
            }

            CompleteCollection.Add(m_addItemView.ViewModel.Item);
            m_addItemView.Close();
            CompleteCollection = CompleteCollection;
            OnSelectedFilterChanged(SelectedFilter);
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

        

        public void OnAbout()
        {
                m_aboutView = new AboutView();
                
                
                m_aboutView.ShowDialog();


                m_aboutView.Close();
        }

        public void OnEditItem(Item item)
        {
            if (item != null)
            {

                Item unEditedItem = item;

                m_editItemView = new EditItemView();
                m_editItemView.ViewModel.CategoryCollection = CategoryCollection;
                m_editItemView.ViewModel.Item = new Item(item);
                m_editItemView.ShowDialog();

                if (m_editItemView.ViewModel.WindowClose == true)
                {
                    return;
                }

                CompleteCollection[CompleteCollection.IndexOf(unEditedItem)]=m_editItemView.ViewModel.Item;
                CompleteCollection = CompleteCollection;
                m_editItemView.Close();                                
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
                    CompleteCollection.Remove(item);
                    CompleteCollection = CompleteCollection;
                }
            }
            else
            {
                MessageBox.Show($"Please select an item to remove.", "No item found", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void OnClose()
        {
            m_xmlReaderWriter.SaveData(CompleteCollection);
            Close?.Invoke();
        }


        public Action Close { get; set; }

        public bool CanClose()
        {
            return false;
        }
    }
}
