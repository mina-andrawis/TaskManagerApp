﻿using System;
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

        public static string PersistencePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\SaveData.json";
        public static JsonSerializerSettings Settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

        public ItemBase SelectedItem { get; set; }

        public ObservableCollection<ItemBase> taskList { get; set; }

        private ObservableCollection<ItemBase> filteredItems { get; set; }

        private bool isSortedAsc;
        private bool isSortedComplete;

        public ObservableCollection<ItemBase> FilteredItems { 
        
            get
            {
                if (isSortedComplete)
                {
                     return isSortedComplete
                        ? new ObservableCollection<ItemBase>(taskList.Where(t => t.IsCompleted))
                        : new ObservableCollection<ItemBase>(taskList.OrderByDescending(t => t.Name));
                }


                if (string.IsNullOrWhiteSpace(Query))
                {
                    return isSortedAsc
                        ? new ObservableCollection<ItemBase>(taskList.OrderBy(t => t.Priority))
                        : new ObservableCollection<ItemBase>(taskList.OrderByDescending(t => t.Priority));
                }

                return isSortedAsc
                   ? new ObservableCollection<ItemBase>(
                   taskList.Where(t => t.Name.ToUpper().Contains(Query.ToUpper())
                   || t.Description.ToUpper().Contains(Query.ToUpper())
                   || ((t is Appointment) && (t as Appointment).Attendees.Any(s => s.Contains(Query)))
                   ).OrderBy(t => t.Priority))
                   : new ObservableCollection<ItemBase>(
                   taskList.Where(t => t.Name.ToUpper().Contains(Query.ToUpper())
                   || t.Description.ToUpper().Contains(Query.ToUpper())
                   || ((t is Appointment) && (t as Appointment).Attendees.Any(s => s.Contains(Query)))
                   ).OrderByDescending(t => t.Priority));
            }
        
        }

        public ObservableCollection<ItemBase> CompletedItems
        {

            get
            {
                return isSortedComplete
                    ? new ObservableCollection<ItemBase>(taskList.Where(t => t.IsCompleted))
                    : new ObservableCollection<ItemBase>(taskList.OrderByDescending(t => t.Name));
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
            NotifyPropertyChanged("CompletedItems");

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public void RemoveItem()
        {
            if (SelectedItem != null)
            {
                //make a web call to delete this same item on the server
                taskList.Remove(SelectedItem);
                RefreshList();
            }
        }

        public void Sort()
        {
            isSortedAsc = !isSortedAsc;
            RefreshList();
        }

        public void SortCompleted()
        {
            isSortedComplete = !isSortedComplete;
            RefreshList();
        }



/*        public void SaveState()
        {
            File.WriteAllText(PersistencePath, JsonConvert.SerializeObject(this, Settings));
        }*/
    }
}
