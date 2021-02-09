using Microsoft.VisualStudio.PlatformUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace lagerus_maximus
{
    //string m_saveFileName, string m_imageDirectory,
    class XmlReaderWriter
    {
        string m_saveFileName;
        string m_imageDirectory;
        string m_saveLocation;

        public XmlReaderWriter(string saveDirectory,string saveFileName, string imageDirectory)
        {
            m_saveFileName = saveFileName;
            m_imageDirectory=imageDirectory;
            m_saveLocation =saveDirectory;
        }


        public string SerializeObject(ObservableCollection<Item> items)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(items.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, items);
                return textWriter.ToString();
            }
        }

        public ObservableCollection<Item> XmlDeserialize(string toDeserialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObservableCollection<Item>));
            using (StringReader textReader = new StringReader(toDeserialize))
            {
                try
                {
                    return (ObservableCollection<Item>)xmlSerializer.Deserialize(textReader);
                }
                catch (Exception ex)
                {
                    return new ObservableCollection<Item>();
                }
                
            }
        }

        public void SaveData(ObservableCollection<Item> items)
        {
            if (!Directory.Exists(Path.Combine(m_saveLocation)))
            {
                Directory.CreateDirectory(Path.Combine(m_saveLocation));
            }

            if (!Directory.Exists(Path.Combine(m_saveLocation, m_imageDirectory)))
            {
                Directory.CreateDirectory(Path.Combine(m_saveLocation, m_imageDirectory));
            }

            if (!File.Exists(Path.Combine(m_saveLocation, m_saveFileName)))
            {
                using (File.Create(Path.Combine(m_saveLocation, m_saveFileName)))
                {

                }
            }

            if (!File.Exists(Path.Combine(m_imageDirectory, "MissingImage.png")))
            {
                

                //using (File.Create(Path.Combine(m_imageDirectory, "MissingImage.png")))
                //{

                //}
            }

            File.WriteAllText(Path.Combine(m_saveLocation, m_saveFileName), SerializeObject(items));
        }

        public ObservableCollection<Item> LoadData()
        {

            if (!Directory.Exists(Path.Combine(m_saveLocation)))
            {
                Directory.CreateDirectory(Path.Combine(m_saveLocation));
            }

            if (!Directory.Exists(Path.Combine(m_saveLocation, m_imageDirectory)))
            {
                Directory.CreateDirectory(Path.Combine(m_saveLocation, m_imageDirectory));
            }

            if (!File.Exists(Path.Combine(m_saveLocation, m_saveFileName)))
            {
                using (File.Create(Path.Combine(m_saveLocation, m_saveFileName)))
                {

                }       
            }

            return XmlDeserialize(File.ReadAllText(Path.Combine(m_saveLocation, m_saveFileName)));
        }
    }
}
