using System;
using System.Globalization;
using System.Collections.Generic;

using TaskLibrary = Task.Library;

namespace TaskManagerApp
{


    class Program
    {
        //list to organize the tasks
        public static List<TaskLibrary.Task> taskList= new List<TaskLibrary.Task>();

        static void Main(string[] args)
        {

            var selection = " ";

            //present user with menu options
            Console.WriteLine("Please select an option from the following menu:\n" +
                "1) create task\n" +
                "2) delete task\n" +
                "3) edit existing task\n" +
                "4) complete task\n" +
                "5) list all outstanding tasks\n" +
                "6) list all tasks\n");

            selection = Console.ReadLine();

            //depending on the input of the user, do the thing
            switch (selection)
            {
                //create task
                case "1":

                    var name = " ";
                    var desc = " ";

                    var deadline = new DateTime(2000,01,01);    //placeholder date
                    CultureInfo us = new CultureInfo("en-US");  //used to error check for USA date format
                    string format = "d"; //error checking for deadline format (mm/dd/yyyy)

                    var completed = false;

                    Console.WriteLine("What is the name of your task? Click 'Enter' when complete.");
                    name = Console.ReadLine();

                    Console.WriteLine("Provide a description if you would like. Click 'Enter' when complete.");
                    desc = Console.ReadLine();

                    Console.WriteLine("Provide the deadline date of this task (e.g. 04/12/2021). Click 'Enter' when complete.");
                    string deadlineString = Console.ReadLine();


                    //parse user inputted date to check formatting
                    try
                    {
                        deadline = DateTime.ParseExact(deadlineString, format, us);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("{0} is not in the correct format.", deadlineString);
                    }


                    // use method found in TaskLibrary in order to create a 
                    // new Task object and insert it into the taskList
                    taskList.Add(new TaskLibrary.Task().addTask(name, desc, deadline, completed));

                    new TaskLibrary.Task().listAllTasks(taskList);

                    break;

            }





        }

    }
}
