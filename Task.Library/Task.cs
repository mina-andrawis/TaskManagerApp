using System;
using System.Collections.Generic;

namespace Task.Library
{
    public class Task : ItemBase
    {



        // { set; get;} automatically creates private fields , no need for declaring _deadline or _isCompleted
        public DateTime Deadline { get; set; }
        public bool IsCompleted { get; set; }
        

        public Task AddTask(string name, string desc, DateTime deadline, bool completed)
        {
            Task newTask = new Task();

            newTask.Name = name;
            newTask.Description = desc;
            newTask.Deadline = deadline;
            newTask.IsCompleted = completed;

            return newTask;

        }

        public void Complete(Task task)
        {

            task.IsCompleted = true;
        }


        public override string ToString()
        {
            return $"{Id}. {Name} // {Description} Due: {Deadline}";
        }
    }
}

