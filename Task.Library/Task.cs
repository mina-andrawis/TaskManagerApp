using System;

namespace Task.Library
{
    public class Task
    {

        public string Name = null;
        public string Description = null;
        public DateTime Deadline = new DateTime();
        public bool isCompleted;

        public void addTask(string name, string desc, DateTime date, bool completed)
        {
            Name = name;
            Description = desc;
            Deadline = date;
            isCompleted = completed;

            Console.WriteLine(Name.ToString() + " " + Description.ToString()+ " " + Deadline + " " + isCompleted);
        }
    }
}
