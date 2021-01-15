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
    public class AboutViewModel : ICloseWindow
    {
        public bool WindowClose = false;

        public Action Close { get; set; }

        public bool CanClose()
        {
            return false;
        }

        public string Title
        {
            get
            {
                return $"About {Application.ResourceAssembly.GetName().Name.ToString()}";
            }
        }

        public string CopyrightText
        {
            get
            {
                return $"IT218 Schulprojekt von Melvin Scherer und Moritz Zier.";
            }
            set { }
        }

        public string MadeBy
        {
            get
            {
                return $"Melvin Scherer and Moritz Zier";
            }
        }

        public string Version
        {
            get
            {
                return Application.ResourceAssembly.GetName().Version.ToString();
            }
        }

        public string ApplicationName
        {
            get
            {
                return Application.ResourceAssembly.GetName().Name;
            }
        }

        public ICommand CloseCommand { get; }

        public AboutViewModel()
        {
            CloseCommand = new DelegateCommand(OnClose);
        }

        public void OnClose()
        {
            Close?.Invoke();
        }
    }
}
