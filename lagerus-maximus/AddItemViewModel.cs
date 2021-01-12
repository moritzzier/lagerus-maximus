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
    public class AddItemViewModel : INotifyPropertyChanged
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

        public ICommand OKMACH { get; }

        public AddItemViewModel()
        {           
        OKMACH = new DelegateCommand(MACH);
        }

        public void MACH()
        {

        }


    }
}
