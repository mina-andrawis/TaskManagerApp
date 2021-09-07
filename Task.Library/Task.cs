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

        private static int currentId;

        // properties

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsCompleted { get; set; }

        public Task AddTask(string name, string desc, DateTime deadline, bool completed)
        {
            Id = currentId++;       //id is updated every time constructor is called

            Task newTask = new Task();

            newTask._name = name;
            newTask._description = desc;
            newTask._deadline = deadline;
            newTask._isCompleted = completed;

            return newTask;

        }

        public void ListAllTasks(List<Task> taskList)
        {

            Console.WriteLine("\n");

            foreach (var Task in taskList)
            {
                Console.WriteLine($"{Id}) {Task._name} // {Task._description}");
            }
            Console.WriteLine("\n");
        }

        public void ListOutstanding(List<Task> taskList)
        {

            Console.WriteLine("\n");

            foreach (var Task in taskList)
            {
                if (!Task._isCompleted)
                {
                    Console.WriteLine($"{Id}) {Task._name} // {Task._description}");

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
            catch (ArgumentOutOfRangeException e)
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
    }
}

