using System;
using System.Collections.Generic;
using System.Text;
using Task.Library;

namespace Task.Library.UWP.Models
{
    public class ItemBase
    {
        private static int currentId = 1;       //keep track of the amount of tasks
        private int _id = 0;       //check if id is new

        // properties
        public int Id { get; set; }

        public int Priority { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; }

        public void SetPriority (ItemBase task, int priority)
        {
            task.Priority = priority;
        }


        public void Complete(ItemBase task)
        {

            task.IsCompleted = true;
        }

        public void DeleteItem(List<ItemBase> taskList, int position)
        {
            try
            {
                taskList.RemoveAt(position);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("The selection was not found. Returning to menu...\n");
            }
        }

        public void SetId()
        {
            if (Id > 0)     //don't increment Id if it has already been set
            {
                return;
            }

            Id = ++FakeDatabase.lastItemId;
        }

    }
}
