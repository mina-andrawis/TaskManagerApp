using System;
using System.Collections.Generic;

namespace Task.Library
{
    public class Task : ItemBase
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

        // { set; get;} automatically creates private fields , no need for declaring _deadline, _isCompleted
        public DateTime Deadline { get; set; }
        public bool IsCompleted { get; set; }
         
        public Task()
        {

        }

        public Task AddTask(string name, string desc, DateTime deadline, bool completed)
        {
            //Id = currentId++;       //id is updated every time constructor is called
            //Console.WriteLine($" ID: {currentId}");


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

        public void DeleteTask(List<Task> taskList, int position)
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

        public void EditTitle(List<Task> taskList, int position, string replacement)
        {
            taskList[position].Name = replacement;
        }

        public void EditDescription(List<Task> taskList, int position, string replacement)
        {
            taskList[position].Description = replacement;
        }

        public override string ToString()
        {
            return $"{Id}. {Name} // {Description} Due: {Deadline}";
        }
    }
}

