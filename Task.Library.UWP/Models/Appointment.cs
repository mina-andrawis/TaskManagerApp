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
            BoundStart = DateTime.Today;

            Stop = DateTime.Today.AddHours(1);
            BoundStop = DateTime.Today.AddHours(1);

            BoundAttendees = "";
            Attendees = new ObservableCollection<String>();
        }
        public DateTime Start { set; get; }
        public DateTime Stop { set; get; }

        public ObservableCollection<String> Attendees { set; get; }

        public string boundAttendees;
        public string BoundAttendees
        {
            get
            {
                return Attendees.ToString();
            }
            set
            {
                boundAttendees = value;
                string[] names = boundAttendees.Split(',');
                Attendees = new ObservableCollection<string>(names);
                
                NotifyPropertyChanged("Attendees");

            }
        }


        public DateTimeOffset boundStart;
        public DateTimeOffset BoundStart
        {
            get
            {
                return Start;
            }
            set
            {
                boundStart = value;
                Start = boundStart.Date;

                NotifyPropertyChanged("Start");
            }
        }

        public DateTimeOffset boundStop;
        public DateTimeOffset BoundStop
        {
            get
            {
                return Stop;
            }
            set
            {
                boundStop = value;
                Stop = boundStop.Date;

                NotifyPropertyChanged("Stop");
            }
        }





        public override string ToString()
        {
            return $"[{Priority}] {Name}, {Description} \n" +
                $"  Start Time: {Start}\n" +
                $"  End Time: {Stop}\n" +
                $"  Attendees: { string.Join(", ", Attendees)}";
        }
    }
}
