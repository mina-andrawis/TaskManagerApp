using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Task.Library.UWP.Models;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Task.Library.UWP.ViewModels
{
    public class MainViewModel : Page, INotifyPropertyChanged
    {
        public ItemBase SelectedItem { get; set; }

        public ObservableCollection<ItemBase> taskList { get; set; }

        public string Query { get; set; }


        public MainViewModel ()
        {
            // make sure the taskList is never null since binding can not
            // bind to null reference
            taskList = new ObservableCollection<ItemBase>(); 
        }

        public void AddItem()
        {
            if (SelectedItem == null)
            {
                taskList.Add(new ItemBase());
            }
        }

        public void DeleteItem()
        {
            if (SelectedItem != null)
            {
                taskList.Remove(SelectedItem);
            }
        }

        public async System.Threading.Tasks.Task Search()
        {
            Console.WriteLine(Query);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
