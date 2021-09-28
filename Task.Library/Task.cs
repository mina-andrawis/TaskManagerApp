using System;
using System.Collections.Generic;

namespace Task.Library
{
    public class Task
    {

        // fields
        public string _name = null;
        public string _description = null;
        public DateTime _deadline = new DateTime();
        public bool _isCompleted;

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

        public void ListAllTasks(List<Task> taskList)
        {

            Console.WriteLine("\n");
            int id = 1;


            foreach (var Task in taskList)
            {
                Console.WriteLine($"{id++}) {Task._name} // {Task._description}");
            }
            Console.WriteLine("\n");
        }

        public void ListOutstanding(List<Task> taskList)
        {

            Console.WriteLine("\n");
            int id = 1;


            foreach (var Task in taskList)
            {
                if (!Task._isCompleted)
                {
                    Console.WriteLine($"{id++}) {Task._name} // {Task._description}");

                }
            }
            Console.WriteLine("\n");

        }

        public void Complete(Task task)
        {
            task._isCompleted = true;
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
            taskList[position]._name = replacement;
        }

        public void EditDescription(List<Task> taskList, int position, string replacement)
        {
            taskList[position]._description = replacement;
        }

        public override string ToString()
        {
            return $"{Id}. {Name} - {Description} Due: {Deadline}";
        }
    }
}

