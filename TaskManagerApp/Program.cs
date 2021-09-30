using System;
using System.Globalization;
using System.Collections.Generic;

using TaskLibrary = Task.Library;
using Task.Library;

namespace TaskManagerApp
{


    class Program
    {
        //list to organize the tasks
        public static List<TaskLibrary.ItemBase> taskList= new List<TaskLibrary.ItemBase>();

        static void Main(string[] args)
        {

            string selection="";

            var itemNavigator = new ListNavigator<TaskLibrary.ItemBase>(taskList);

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
                    "       8) close task manager\n");

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
                        taskList.Add(new TaskLibrary.Task().AddTask(name, desc, deadline, completed));

                        PrintTaskList(itemNavigator);

                        break;

                    //create appointment
                    case "2":

                        //placeholder values        
                        var startTime = new DateTime(2000, 01, 01);    
                        var endTime = new DateTime(2000, 01, 01);    

                        List<String> attendeeList = new List<String>();

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

                        Console.WriteLine("Provide the names of the attendees followed by 'Enter'. Type in \"Done\" when completed.");
                        var attendee = Console.ReadLine();

                        //loop until user inputs "done" - ignore case
                        while (!string.Equals(attendee, "done", StringComparison.OrdinalIgnoreCase))
                        {
                            attendeeList.Add(attendee);
                            attendee = Console.ReadLine();
                        }


                        taskList.Add(new TaskLibrary.Appointment().AddAppointment(name, desc, startTime, endTime, attendeeList));


                        break;

                    // delete item
                    case "3":

                        PrintTaskList(itemNavigator);

                        Console.WriteLine("Which task would you like to delete? Please provide a number from your outstanding tasks.");


                        string taskChoice = Console.ReadLine(); 


                        try
                        {
                            int numVal = Int32.Parse(taskChoice);

                            new TaskLibrary.Task().DeleteItem(taskList, numVal - 1); 
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

                        PrintTaskList(itemNavigator);

                        Console.WriteLine("Which task would you like to edit? Please provide a number from your outstanding tasks.");

                        taskChoice = Console.ReadLine();


                        Console.WriteLine("Would you like to edit the title (t) or description (d) of the task?.");
                        string editChoice = Console.ReadLine();    
                         
                        try
                        {
                            int numVal = Int32.Parse(taskChoice);

                            string replacement;

                            if (editChoice == "t")
                            {
                                Console.WriteLine("Would you like to replace the title with?");
                                replacement = Console.ReadLine();    

                                new TaskLibrary.Task().EditTitle(taskList, numVal - 1, replacement);
                            }
                            else if (editChoice == "d")
                            {
                                Console.WriteLine("Would you like to replace the description with?");
                                replacement = Console.ReadLine();     

                                new TaskLibrary.Task().EditDescription(taskList, numVal - 1, replacement);
                            }
                            else
                            {
                                Console.WriteLine("{0} is not a valid choice. Returning to menu...", selection);
                                break;
                            }


                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("You did not enter a numeric value or the selection was not found. Returning to menu...\n");
                            break;
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
                            
                            new TaskLibrary.Task().Complete(taskList[numVal-1]);
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

                        PrintTaskList(itemNavigator,true);

                        break;

                    // print all tasks
                    case "7":

                        PrintTaskList(itemNavigator);

                        break;

                    case "8":
                        break;

                    default:
                        Console.WriteLine("Your selection, {0}, was not found. Returning to menu...\n", selection);
                        
                        break;

                }
            } while (selection != "8");         //8 means exit application

            Console.WriteLine("Thank you for using the application. Shutting down.. \n");

        }
        public static void PrintTaskList(ListNavigator<TaskLibrary.ItemBase> itemNavigator, bool onlyOutstanding = false)
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
    }
}
