using Microsoft.VisualStudio.PlatformUI;
using Microsoft.Win32;
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
    public class OptionsViewModel : INotifyPropertyChanged //, ICloseWindow
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string m_saveFileName;
        public string SaveFileName
        {
            get => m_saveFileName;
            set
            {
                m_saveFileName = value;
                OnPropertyChanged();
            }
        }
        public string m_imageDirectory;
        public string ImageDirectory
        {
            get => m_imageDirectory;
            set
            {
                m_imageDirectory = value;
                OnPropertyChanged();
            }
        }
        public string m_saveDirectory;
        public string SaveDirectory
        {
            get => m_saveDirectory;
            set
            {
                m_saveDirectory = value;
                OnPropertyChanged();
            }
        }

        public bool SaveOptions { get; set; }

        public void Initialize()
        {

        }

        public void OnCancel()
        {
            SaveOptions = true;
            Close?.Invoke();
        }

        public Action Close { get; set; }

        public bool CanClose()
        {
            return false;
        }
    }
}
