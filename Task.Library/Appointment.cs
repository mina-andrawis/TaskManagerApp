using System;
using System.Collections.Generic;
using System.Text;

namespace Task.Library
{
    public class Appointment : ItemBase
    {
        public DateTime Start { set; get; }
        public DateTime Stop { set; get; }
        public List<String> Attendees { set; get; }

        public Appointment AddAppointment(string name, string desc, DateTime start, DateTime stop, List<String> attendees)
        {

            Appointment newAppt = new Appointment();

            newAppt.Name = name;
            newAppt.Description = desc;
            newAppt.Start = start;
            newAppt.Stop = stop;
            newAppt.Attendees = attendees;

            return newAppt;

        }



        public override string ToString()
        {
            return $"Appointment {Id}. {Name} - {Description} \n" +
                $"Start Time: {Start}\n" +
                $"End Time: {Stop}\n" +
                $"Attendees: { string.Join(", ", Attendees)}";
        }
    }
}
