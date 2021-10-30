using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Task.Library.UWP.Models;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace Task.Library.UWP.ViewModels
{
    public class MainViewModel : Page, INotifyPropertyChanged
    {



        public static string PersistancePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\SaveData.json";
        public static JsonSerializerSettings Settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

        public ItemBase SelectedItem { get; set; }

        public ObservableCollection<ItemBase> taskList { get; set; }

        private ObservableCollection<ItemBase> filteredItems { get; set; }

        public ObservableCollection<ItemBase> FilteredItems { 
        
            get
            {
                if (string.IsNullOrWhiteSpace(Query))
                {
                    return taskList;
                }
                else
                {
                    filteredItems = new ObservableCollection<ItemBase>(taskList
                        .Where(s => s.Description.ToUpper().Contains(Query.ToUpper())
                        || s.Name.ToUpper().Contains(Query.ToUpper())).ToList());
                    return filteredItems;
                }
            }
        
        }

        public string Query { get; set; }


        public MainViewModel ()
        {
            // make sure the taskList is never null since binding can not
            // bind to null reference
            taskList = new ObservableCollection<ItemBase>(); 
        }

        public async System.Threading.Tasks.Task Search()
        {
            Console.WriteLine(Query);
        }

        public void RefreshList()
        {
            NotifyPropertyChanged("FilteredItems");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SaveState()
        {
            var viewModelJson = JsonConvert.SerializeObject(this, Settings);
            var hi = 1;
            File.WriteAllText(PersistancePath, viewModelJson);      // inserts reference of MainViewModel 

        }
    }
}
