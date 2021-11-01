using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Task.Library.UWP.Models
{
    public class Appointment : ItemBase
    {

        public Appointment()
        {
            Priority = 0;
            Name = "";
            Description = "";
            Start = DateTime.Today;
            Stop = DateTime.Today.AddHours(1);
            Attendees = new ObservableCollection<String>();
        }
        public DateTime Start { set; get; }
        public DateTime Stop { set; get; }
        public ObservableCollection<String> Attendees { set; get; }



        public override string ToString()
        {
            return $"[{Priority}] {Name}, {Description} \n"; // +
              /*  $"  Start Time: {Start}\n" +
                $"  End Time: {Stop}\n" +
                $"  Attendees: { string.Join(", ", Attendees)}";*/
        }
    }
}
