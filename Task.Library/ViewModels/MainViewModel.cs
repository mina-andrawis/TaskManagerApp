using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Task.Library.Models;

namespace Task.Library.ViewModels
{
    public class MainViewModel
    {
        public ItemBase SelectedItem { get; set; }

        public ObservableCollection<ItemBase> taskList { get; set; }

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

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
