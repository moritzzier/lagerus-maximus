using Microsoft.VisualStudio.PlatformUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
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


        public string ExitIcon
        {
            get
            {
                string projectImageFolder = System.IO.Directory.GetCurrentDirectory();
                projectImageFolder = Directory.GetParent(m_projectImageFolder).Parent.Parent.FullName;
                return Path.Combine(m_projectImageFolder, Path.Combine("Images", "Exit.png"));
            }
        }

        public string OptionsIcon
        {
            get
            {
                return Path.Combine(m_projectImageFolder, Path.Combine("Images", "Options.png"));
            }
        }

        public string Icon
        {
            get
            {
                return Path.Combine(m_projectImageFolder, Path.Combine("Images", "LagerusIcon.png"));
            }
        }

        public string AboutIcon
        {
            get
            {
                return Path.Combine(m_projectImageFolder, Path.Combine("Images", "About.png"));
            }
        }


        private OptionsView m_optionsView;
        private AddItemView m_addItemView;
        private EditItemView m_editItemView;
        private AboutView m_aboutView;
        private XmlReaderWriter m_xmlReaderWriter;
        private string m_baseDirectory;
        private string m_projectImageFolder;
        private string m_missingImagePath;
        private string m_clickToSelectPath;
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand CloseCommand { get; }
        public ICommand AboutCommand { get; }
        public ICommand OptionsCommand { get; }       
        public ICommand SelectedFilterChangedCommand { get; }

        public MainWindowViewModel()
        {
            //AddCommand = new DelegateCommand<Item>(OnAddItem);
            AddCommand = new DelegateCommand(OnAddItem);
            EditCommand = new DelegateCommand<Item>(OnEditItem);
            RemoveCommand = new DelegateCommand<Item>(OnRemoveItem);
            CloseCommand = new DelegateCommand(OnClose);
            AboutCommand = new DelegateCommand(OnAbout);
            OptionsCommand = new DelegateCommand(OnOptions);

            SelectedFilterChangedCommand = new DelegateCommand<string>(OnSelectedFilterChanged);
            CategoryCollection =  new ObservableCollection<string>() { "Salben","Tabletten","Pillen", "Zäpfchen","Tee","Sonstige" };

            m_projectImageFolder = System.IO.Directory.GetCurrentDirectory();
            m_projectImageFolder = Directory.GetParent(m_projectImageFolder).Parent.Parent.FullName;
            m_missingImagePath = Path.Combine(m_projectImageFolder, Path.Combine("Images", "MissingImage.png"));
            m_clickToSelectPath = Path.Combine(m_projectImageFolder, Path.Combine("Images", "ClickToSelectImage.png"));
            
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            m_baseDirectory =  Path.Combine(path,"Lagerus Maximus");


            if (!Directory.Exists(Path.Combine(m_baseDirectory)))
            {
                Directory.CreateDirectory(Path.Combine(m_baseDirectory));
            }

            if (!Directory.Exists(Path.Combine(m_baseDirectory, "Images")))
            {
                Directory.CreateDirectory(Path.Combine(m_baseDirectory, "Images"));
            }

            if (!File.Exists(Path.Combine(m_baseDirectory, "Save.xml")))
            {
                using (File.Create(Path.Combine(m_baseDirectory, "Save.xml")))
                {

                }
            }

            m_xmlReaderWriter = new XmlReaderWriter(m_baseDirectory, "Save.xml", "Images");

            foreach (string s in CategoryCollection)
            {
                FilterCollection.Add(s);
            }            
            FilterCollection.Insert(0, "All");
            OnSelectedFilterChanged("All");
        }

        public void Initialize()
        {
            CompleteCollection = m_xmlReaderWriter.LoadData();
                       
            foreach (Item item in CompleteCollection)
            {
                if (!File.Exists(item.ImagePath))
                {
                    item.ImagePath = m_missingImagePath;
                }
            }
        }

        public void OnOptions()
        {
            m_optionsView = new OptionsView();
            m_optionsView.ShowDialog();

            if (m_optionsView.ViewModel.SaveOptions == true)
            {
                //Save
                //Initialize();
            }

            m_optionsView.Close();
        }

        public void OnAddItem()
        {
            bool done = false;

            Item item = new Item();
            item.ImagePath = m_clickToSelectPath;
            while (!done)
            {
                m_addItemView = new AddItemView();
                m_addItemView.ViewModel.Item = item;
                m_addItemView.ViewModel.CategoryCollection = CategoryCollection;
                m_addItemView.ShowDialog();

                item = m_addItemView.ViewModel.Item;

                if (m_addItemView.ViewModel.WindowClose == true)
                {
                    return;
                }

                if (m_addItemView.ViewModel.Item.ImagePath == m_clickToSelectPath)
                {
                    m_addItemView.ViewModel.Item.ImagePath = m_missingImagePath;
                }
                else
                {
                    if(!File.Exists(Path.Combine(m_baseDirectory, Path.Combine("Images", item.Name + ".png"))))
                    {
                        File.Copy(m_addItemView.ViewModel.Item.ImagePath, Path.Combine(m_baseDirectory, Path.Combine("Images", item.Name + ".png")));
                    }
                }

                List<Item> items = new List<Item>();
                foreach (var i in CompleteCollection)
                {
                    items.Add(i);
                }


                done = true;
                string output = "Following issue(s) prevent the item from being added:\n\n\n";

                if (items.Exists(x => x.Name == item.Name))
                {
                    output += $"The name '{item.Name}' is already taken.\n\n";
                }

                if (items.Exists(x => x.ItemNumber == item.ItemNumber))
                {
                    output += $"The item number '{item.ItemNumber}' is already taken.\n\n";
                }
                if(output != "Following issue(s) prevent the item from being added:\n\n\n")
                {
                    if (MessageBoxResult.Yes == MessageBox.Show($"{output}Do you want to edit the item?", "Could not add new item", MessageBoxButton.YesNo, MessageBoxImage.Error))
                    {
                        done = false;
                    }
                    else
                    {
                        return;
                    }
                }
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
