using System;
using TaskLibrary = Task.Library;

namespace TaskManagerApp
{


    class Program
    {


        static void Main(string[] args)
        {

            var name = "tom";
            var desc = "desfc";
            var now = DateTime.Today;
            var com = false;

            new TaskLibrary.Task().addTask(name,desc,now,com);
        }

    }
}
