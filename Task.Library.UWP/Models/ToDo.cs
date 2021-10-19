using System;
using System.Collections.Generic;

namespace Task.Library.UWP.Models
{
    public class ToDo : ItemBase
    {


        // { set; get;} automatically creates private fields , no need for declaring _deadline or _isCompleted
        public DateTime Deadline { get; set; }
        

        public ToDo AddTask(string name, string desc, DateTime deadline, bool completed)
        {
            ToDo newTask = new ToDo();

            newTask.Name = name;
            newTask.Description = desc;
            newTask.Deadline = deadline;
            newTask.IsCompleted = completed;

            return newTask; 

        }



        public override string ToString()
        {
            return $"{Id}. {Name} // {Description} Due: {Deadline}";
        }
    }
}

