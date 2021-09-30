using System;
using System.Collections.Generic;
using System.Text;

namespace Task.Library
{
    public class ItemBase
    {
        private static int currentId = 1;       //keep track of the amount of tasks
        private int _id = -1;       //check if id is new

        // properties
        public int Id
        {
            get
            {
                if (_id <= 0)   // if the id is new, set the id to currentId
                {
                    _id = currentId++;
                }
                return _id;
            }
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; }


        public void EditTitle(List<ItemBase> taskList, int position, string replacement)
        {
            taskList[position].Name = replacement;
        }

        public void EditDescription(List<ItemBase> taskList, int position, string replacement)
        {
            taskList[position].Description = replacement;
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

    }
}
