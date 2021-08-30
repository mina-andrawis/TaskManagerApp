using System;
using System.Collections.Generic;

namespace Task.Library
{
    public class Task
    {

        public string Name = null;
        public string Description = null;
        public DateTime Deadline = new DateTime();
        public bool isCompleted;

        public Task addTask(string name, string desc, DateTime deadline, bool completed)
        {

            Task newTask = new Task();

            newTask.Name = name;
            newTask.Description = desc;
            newTask.Deadline = deadline;
            newTask.isCompleted = completed;

            return newTask;

        }

        public void listAllTasks(List<Task> taskList)
        {
            int i = 1;

            foreach (var Task in taskList)
            {
                Console.WriteLine("{0}) {1}", i++, Task.Name);
            }
        }

        public void listOutstanding(List<Task> taskList)
        {
            int i = 1;

            foreach (var Task in taskList)
            {
                if (!Task.isCompleted)
                {
                    Console.WriteLine("{0}) {1}", i++, Task.Name);
                }
            }
        }

        public void deleteTask()
        {

        }
    }
}
