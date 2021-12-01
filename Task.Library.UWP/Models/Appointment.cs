using MongoDB.Bson.Serialization.Attributes;
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

        [BsonElement("Start")]
        private DateTime start;

        [BsonIgnore]
        public DateTime Start {
            get
            {
                return start;
            }
            set
            {
                start = value;
                NotifyPropertyChanged();
            }
        }

        [BsonElement("Stop")]
        private DateTime stop;


        [BsonIgnore]
        public DateTime Stop {
            get
            {
                return stop;
            }
            set
            {
                stop = value;
                NotifyPropertyChanged();
            }
        }


        [BsonElement("Attendees")]
        private ObservableCollection<String> attendees;

        [BsonIgnore]
        public ObservableCollection<String> Attendees
        {
            get
            {
                return attendees;
            }
            set
            {
                attendees = value;
                NotifyPropertyChanged();
            }
        }

        [BsonIgnore]
        public string boundAttendees;

        [BsonIgnore]
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

        [BsonIgnore]
        private DateTimeOffset boundStart;
        
        [BsonIgnore]
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



        [BsonIgnore]
        private DateTimeOffset boundStop;

        [BsonIgnore]
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
