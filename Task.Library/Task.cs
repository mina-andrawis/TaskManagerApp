using System;

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

            //Console.WriteLine(Name.ToString() + " " + Description.ToString()+ " " + Deadline + " " + isCompleted);
        }


    }
}
