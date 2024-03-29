﻿using System;
using System.Globalization;
using System.Collections.Generic;

using TaskLibrary = Task.Library;
using Task.Library.Models;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using Windows.Storage;
using Task.Library;

namespace TaskManagerApp
{


    class Program
    {
        //list to organize the tasks
        public static List<ItemBase> taskList;

        private static string persistencePath;

        //allows json file to be type aware so appointments and tasks are differentiated
        private static JsonSerializerSettings serializationSettings; 

        static void Main(string[] args)
        {
            serializationSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

            persistencePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\SaveData.txt";

            if (File.Exists(persistencePath))
            {
                //deserialize the list (read from file and create new list from json)
                taskList = JsonConvert.DeserializeObject<List<ItemBase>>(File.ReadAllText(persistencePath),serializationSettings);

            }
            else
            {
                taskList = new List<ItemBase>();
            }
            string selection="";

            var itemNavigator = new ListNavigator<ItemBase>(taskList);

            //do while to loop menu after each choice
            do
            {

                //present user with menu options
                Console.WriteLine("Please select an option from the following menu:\n" +
                    "1) create task\n" +
                    " 2) create appointment\n" +
                    "  3) delete item\n" +
                    "   4) edit existing item\n" +
                    "    5) complete item\n" +
                    "     6) list all outstanding items\n" +
                    "      7) list all items\n" +
                    "       8) search for a string\n" +
                    "        9) save\n" +
                    "         10) close task manager\n");

                selection = Console.ReadLine();


                //depending on the input of the user, do the thing
                switch (selection)
                {
                    //create task
                    case "1":

                        //used for both appointment and task
                        var name = " ";
                        var desc = " ";

                        var deadline = new DateTime(2000, 01, 01);    //placeholder date


                        var completed = false;

                        Console.WriteLine("What is the name of your task? Click 'Enter' when complete.");
                        name = Console.ReadLine();
                        Console.WriteLine("Provide a description if you would like. Click 'Enter' when complete.");
                        desc = Console.ReadLine();
                        Console.WriteLine("Provide the deadline date of this task (e.g. 04/12/2021). Click 'Enter' when complete.");
                        string deadlineString = Console.ReadLine();

                        if (!DateTime.TryParse(deadlineString, out deadline))
                        {
                            Console.WriteLine("Unable to parse '{0}'. Defaulting to today at 12:00 pm", deadlineString);
                            deadline = DateTime.Today.AddHours(12);
                        }


                        // use method found in TaskLibrary in order to create a 
                        // new Task object and insert it into the taskList
                        taskList.Add(new TaskLibrary.Models.Task().AddTask(name, desc, deadline, completed));

                        PrintTaskList(itemNavigator);

                        break;

                    //create appointment
                    case "2":

                        //placeholder values        
                        var startTime = new DateTime(2000, 01, 01);
                        var endTime = new DateTime(2000, 01, 01);


                        Console.WriteLine("What is the name of your appointment? Click 'Enter' when complete.");
                        name = Console.ReadLine();
                        Console.WriteLine("Provide a description if you would like. Click 'Enter' when complete.");
                        desc = Console.ReadLine();
                        Console.WriteLine("Provide the date and start time of this appointment (e.g. 04/12/2021 12:00 PM ). Click 'Enter' when complete.");
                        var startDateString = Console.ReadLine();

                        if (!DateTime.TryParse(startDateString, out startTime))
                        {
                            Console.WriteLine("Unable to parse '{0}'. Defaulting to today at 12:00 pm", startDateString);
                            startTime = DateTime.Today.AddHours(12);
                        }

                        Console.WriteLine("Provide the date and end time of this appointment (e.g. 04/12/2021 12:30 PM). Click 'Enter' when complete.");
                        var endDateString = Console.ReadLine();

                        if (!DateTime.TryParse(endDateString, out endTime))
                        {
                            Console.WriteLine("Unable to parse '{0}'. Defaulting to today at 12:30 pm", endDateString);
                            endTime = DateTime.Today.AddHours(12).AddMinutes(30);

                        }

                        var attendeeList = new List<String>();

                        Console.WriteLine("Provide the names of the attendees followed by 'Enter'. Type in \"Done\" when completed.");
                        var attendee = Console.ReadLine();

                        //loop until user inputs "done" - ignore case
                        while (!string.Equals(attendee, "done", StringComparison.OrdinalIgnoreCase))
                        {
                            attendeeList.Add(attendee);
                            attendee = Console.ReadLine();
                        }


                        taskList.Add(new TaskLibrary.Models.Appointment().AddAppointment(name, desc, startTime, endTime, attendeeList));

                        PrintTaskList(itemNavigator);

                        break;

                    // delete item
                    case "3":

                        PrintTaskList(itemNavigator);

                        Console.WriteLine("Which task would you like to delete? Please provide a number from your outstanding tasks.");


                        string taskChoice = Console.ReadLine();


                        try
                        {
                            int numVal = Int32.Parse(taskChoice);

                            new TaskLibrary.Models.Task().DeleteItem(taskList, numVal - 1);
                            // -1 to account for the fact that Id starts at 1, not 0
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("You did not enter a numeric value or the selection was not found. Returning to menu...\n");
                            break;
                        }

                        break;

                    // edit item
                    case "4":

                        ItemBase taskToEdit;        //task instance that will be edited
                        string internalEditChoice;  //user hoice of title, description, deadline, ect..

                        PrintTaskList(itemNavigator);

                        Console.WriteLine("Which task would you like to edit? Please provide a number from your outstanding tasks.");

                        if (int.TryParse(Console.ReadLine(), out int editChoice))       //parse user option and match with ID if available
                        {
                            taskToEdit = taskList.FirstOrDefault(t => t.Id == editChoice);


                            if (taskToEdit is TaskLibrary.Models.Task)     //the selected item is a Task
                            {
                                Console.WriteLine("Would you like to edit the title (t), description (d), or deadline (D) of the task?");
                                internalEditChoice = Console.ReadLine();

                                //switch statement to take care of internal switch statement to improve readability
                                internalTaskEditSwitch(internalEditChoice, editChoice);


                            }
                            else if (taskToEdit is TaskLibrary.Models.Appointment)     //the selected item is an Appointment
                            {
                                Console.WriteLine("Would you like to edit the title (t), description (d), start date/time (s), end date/time (e), or attendees (a) of the appointment?");
                                internalEditChoice = Console.ReadLine();

                                //switch statement to take care of internal switch statement to improve readability
                                internalAppointmentEditSwitch(internalEditChoice, editChoice);


                            }
                        }
                        break;

                    // complete item
                    case "5":

                        PrintTaskList(itemNavigator, true);

                        Console.WriteLine("Which task would you like to complete? Please provide a number from your outstanding tasks.");


                        taskChoice = Console.ReadLine();

                        try
                        {
                            int numVal = Int32.Parse(taskChoice);

                            new TaskLibrary.Models.Task().Complete(taskList[numVal - 1]);
                            // -1 to account for the fact that the list shown to user skips 0


                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("You did not enter a numeric value or the selection was not found. Returning to menu...\n");
                            break;
                        }

                        break;

                    // print outstanding items
                    case "6":

                        PrintTaskList(itemNavigator, true);

                        break;

                    // print all tasks
                    case "7":

                        PrintTaskList(itemNavigator);

                        break;

                    //search
                    case "8":
                        Console.WriteLine("What string would you like to search for?\n");
                        var searchSelection = Console.ReadLine();

                        var matchedTasks =
                            from task in taskList
                            where task.Name.Contains(searchSelection) ||
                                task.Description.Contains(searchSelection)
                            select task;



                        foreach (var task in matchedTasks.ToList())
                        {

                            Console.WriteLine(task.ToString());
                        }

                        break;
                    //save
                    case "9":

                        // save jsonList (string) by converting taskList and settings (to allow type differentiation
                        // between appointment and task derived classes
                        var jsonList = JsonConvert.SerializeObject(taskList, serializationSettings);
                        File.WriteAllText($"{persistencePath}", jsonList);

                        break;

                    //quit
                    case "10":
                        break;

                    default:
                        Console.WriteLine("Your selection, {0}, was not found. Returning to menu...\n", selection);
                        
                        break;

                }
            } while (selection != "10");         //10 - save quit, 11 - exit application

            Console.WriteLine("Thank you for using the application. Shutting down.. \n");

        }

        public static void PrintTaskList(ListNavigator<TaskLibrary.Models.ItemBase> itemNavigator, bool onlyOutstanding = false)
        {

            bool isNavigating = true;
            while (isNavigating)
            {
                if (taskList.Count == 0)       //avoid PageFaultException if size is 0
                {
                    Console.WriteLine("\nThe list is empty.\n");
                    break;
                }

                var page = itemNavigator.GetCurrentPage();
                Console.WriteLine("\n");

                foreach (var item in page)
                {
                    if (onlyOutstanding == true)        //only print outstanding
                    {
                        if (!item.Value.IsCompleted)
                        {
                            Console.WriteLine($"{item.Value}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{item.Value}");
                    }

                }

                if (itemNavigator.HasPreviousPage)
                {
                    Console.WriteLine("P. Previous");
                }

                if (itemNavigator.HasNextPage)
                {
                    Console.WriteLine("N. Next");
                }

                var selection = Console.ReadLine();
                if (selection.Equals("P", StringComparison.InvariantCultureIgnoreCase))
                {
                    itemNavigator.GoBackward();
                }
                else if (selection.Equals("N", StringComparison.InvariantCultureIgnoreCase))
                {
                    itemNavigator.GoForward();
                }
                else
                {
                    isNavigating = false;
                }

            }

            Console.WriteLine();
        }

        /* SWITCH STATEMENTS FOR INTERNAL EDIT USE : increase readability*/
        /*************************************************************************************/

        public static void internalAppointmentEditSwitch(string internalEditChoice, int editChoice)
        {
            switch (internalEditChoice)
            {
                //title
                case "t":
                    Console.WriteLine("Would you like to replace the title with?");
                    var replacement = Console.ReadLine();

                    EditTitle(taskList, editChoice - 1, replacement);

                    break;
                //description
                case "d":
                    Console.WriteLine("Would you like to replace the description with?");
                    replacement = Console.ReadLine();

                    EditDescription(taskList, editChoice - 1, replacement);

                    break;

                //start
                case "s":
                    Console.WriteLine("Would you like to replace the start date and time with?");
                    replacement = Console.ReadLine();

                    if (!DateTime.TryParse(replacement,out var replacementDate))
                    {
                        Console.WriteLine("Unable to parse '{0}'. Defaulting to today at 12:00 pm", replacement);
                        replacementDate = DateTime.Today.AddHours(12);

                    }
                    EditStart(taskList, editChoice - 1, replacementDate);

                    break;

                //end
                case "e":
                    Console.WriteLine("Would you like to replace the end date and time with?");
                    replacement = Console.ReadLine();

                    if (!DateTime.TryParse(replacement, out replacementDate))
                    {
                        Console.WriteLine("Unable to parse '{0}'. Defaulting to today at 12:00 pm", replacement);
                        replacementDate = DateTime.Today.AddHours(12);

                    }
                    EditStop(taskList, editChoice - 1, replacementDate);

                    break;

                //attendees
                case "a":
                    Console.WriteLine("Would you like to replace attendee list with? Type \"done\" when completed.");
                    replacement = Console.ReadLine();


                    var newAttendeeList = new List<String>();

                    //loop until user inputs "done" - ignore case
                    while (!string.Equals(replacement, "done", StringComparison.OrdinalIgnoreCase))
                    {
                        newAttendeeList.Add(replacement);
                        replacement = Console.ReadLine();
                    }

                    EditAttendees(taskList, editChoice - 1, newAttendeeList);
                    break;

            }
        }

        public static void internalTaskEditSwitch(string internalEditChoice, int editChoice)
        {
            switch (internalEditChoice)
            {
                //title
                case "t":
                    Console.WriteLine("Would you like to replace the title with?");
                    var replacement = Console.ReadLine();

                    EditTitle(taskList, editChoice - 1, replacement);

                    break;

                //description
                case "d":
                    Console.WriteLine("Would you like to replace the description with?");
                    replacement = Console.ReadLine();

                    EditDescription(taskList, editChoice - 1, replacement);

                    break;

                //deadline
                case "D":
                    Console.WriteLine("Would you like to replace the deadline with with?");
                    replacement = Console.ReadLine();

                    if (!DateTime.TryParse(replacement, out var replacementDate))
                    {
                        Console.WriteLine("Unable to parse '{0}'. Defaulting to today at 12:00 pm", replacement);
                        replacementDate = DateTime.Today.AddHours(12);
                    }

                    EditDeadline(taskList, editChoice - 1, replacementDate);

                    break;

            }

        }

        /*************************************************************************************/



        /*EDIT TASKS METHODS*/
        /*************************************************************************************/
        public static void EditTitle(List<ItemBase> taskList, int position, string replacement)
        {
            (taskList[position]).Name = replacement;
        }

        public static void EditDescription(List<ItemBase> taskList, int position, string replacement)
        {
            (taskList[position]).Description = replacement;
        }

        
        public static void EditDeadline(List<ItemBase> taskList, int position, DateTime replacement)
        {
            (taskList[position] as TaskLibrary.Models.Task).Deadline = replacement;
        }

        public static void EditStart(List<ItemBase> taskList, int position, DateTime replacement)
        {
            (taskList[position] as TaskLibrary.Models.Appointment).Start = replacement;
        }

        public static void EditStop(List<ItemBase> taskList, int position, DateTime replacement)
        {
            (taskList[position] as TaskLibrary.Models.Appointment).Stop = replacement;
        }

        public static void EditAttendees(List<ItemBase> taskList, int position, List<String> replacement)
        {
            (taskList[position] as TaskLibrary.Models.Appointment).Attendees = replacement;
        }
        /*************************************************************************************/

    }
}
