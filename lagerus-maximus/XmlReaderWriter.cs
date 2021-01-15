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
    class XmlReaderWriter
    {
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
            if (!File.Exists("SaveTest.xml"))
            {
                using (File.Create("SaveTest.xml"))
                {


                }
            }

            File.WriteAllText("SaveTest.xml",SerializeObject(items));
        }

        public ObservableCollection<Item> LoadData()
        {
            if(!File.Exists("SaveTest.xml"))
            {
                using (File.Create("SaveTest.xml"))
                {

                }                
            }

            return XmlDeserialize(File.ReadAllText("SaveTest.xml"));
        }
    }
}
