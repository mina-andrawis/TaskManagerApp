using System;
using System.Collections.Generic;
using System.Text;
using Task.Library.Models;

namespace Task.Library.ViewModels
{
    public class MainViewModel
    {
        public List<ItemBase> Items { get; set; }

        public ItemBase SelectedItem { get; set; }

        public MainViewModel ()
        {
            // make sure the taskList is never null since binding can not
            // bind to null reference
            Items = new List<ItemBase>(); 
        }

        public void AddItem()
        {
            if (SelectedItem == null)
            {
                Items.Add(new ItemBase());
            }
        }

        public void DeleteItem()
        {
            if (SelectedItem != null)
            {
                Items.Remove(SelectedItem);
            }
        }
    }
}
