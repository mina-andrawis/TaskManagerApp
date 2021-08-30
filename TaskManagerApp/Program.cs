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

            string selection="";
            //int selectionInt = 0;  //transform into int, cases 3,4,5

            //do while to loop menu after each choice
            do
            {

                //present user with menu options
                Console.WriteLine("Please select an option from the following menu:\n" +
                    "1) create task\n" +
                    " 2) delete task\n" +
                    "  3) edit existing task\n" +
                    "   4) complete task\n" +
                    "    5) list all outstanding tasks\n" +
                    "     6) list all tasks\n" +
                    "      7) close task manager\n");

                selection = Console.ReadLine();


                //depending on the input of the user, do the thing
                switch (selection)
                {
                    //create task
                    case "1":

                        var name = " ";
                        var desc = " ";

                        var deadline = new DateTime(2000, 01, 01);    //placeholder date
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
                            Console.WriteLine("{0} is not in the correct format. Returning to menu...", deadlineString);
                            break;
                        }

                        // use method found in TaskLibrary in order to create a 
                        // new Task object and insert it into the taskList
                        taskList.Add(new TaskLibrary.Task().AddTask(name, desc, deadline, completed));

                        new TaskLibrary.Task().ListAllTasks(taskList);

                        break;

                    // delete task
                    case "2":

                        Console.WriteLine("Which task would you like to delete? Please provide a number from your outstanding tasks.");

                        new TaskLibrary.Task().ListAllTasks(taskList);

                        selection = Console.ReadLine();     //overwrite selection variable since we're alrady in case block


                        try
                        {
                            int numVal = Int32.Parse(selection);

                            new TaskLibrary.Task().DeleteTask(taskList, numVal - 1); 
                            // -1 to account for the fact that the list shown to user skips 0


                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine("You did not enter a numeric value or the selection was not found. Returning to menu...\n");
                            break;
                        }

                        // incomplete

                        break;

                    // edit task
                    case "3":

                        // incomplete

                        break;
                        
                    // complete task
                    case "4":
                        Console.WriteLine("Which task would you like to complete? Please provide a number from your outstanding tasks.");

                        new TaskLibrary.Task().ListOutstanding(taskList);

                        selection = Console.ReadLine();     //overwrite selection variable since we're alrady in case block

                        try
                        {
                            int numVal = Int32.Parse(selection);
                            
                            new TaskLibrary.Task().Complete(taskList[numVal-1]);
                            // -1 to account for the fact that the list shown to user skips 0


                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine("You did not enter a numeric value or the selection was not found. Returning to menu...\n");
                            break;
                        }

                        break;

                    // print outstanding tasks
                    case "5":

                        new TaskLibrary.Task().ListOutstanding(taskList);

                        break;
                    // print all tasks
                    case "6":

                        new TaskLibrary.Task().ListAllTasks(taskList);

                        break;

                }
            } while (selection != "7");         //7 means exit application

        }
    }
}
