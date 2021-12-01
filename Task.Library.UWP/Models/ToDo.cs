using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace Task.Library.UWP.Models
{
    public class ToDo : ItemBase
    {

        public ToDo()
        {
            Priority = 0;
            Name = "";
            Description = "";
            Deadline = DateTime.Today;
            BoundDeadline = DateTime.Today;
            BoundDeadlineTime = TimeSpan.Zero;

        }


        [BsonElement("deadline")]
        private DateTime deadline;

        [BsonIgnore]
        public DateTime Deadline
        {
            get
            {
                return deadline;
            }
            set
            {
                boundDeadline = value;

                NotifyPropertyChanged();
            }
        }


        [BsonIgnore]
        public DateTimeOffset boundDeadline;

        [BsonIgnore]
        TimeSpan BoundDeadlineTime
        {
            get => Deadline.TimeOfDay;
            set
            {
                Deadline = Deadline.AddTicks(value.Ticks);
            }
        }

        [BsonIgnore]
        public DateTimeOffset BoundDeadline
        {
            get
            {
                return boundDeadline;
            }
            set
            {
                boundDeadline = value;
                Deadline = boundDeadline.Date;

                NotifyPropertyChanged("Deadline");
            }
        }


        public override string ToString()
        {
            return $"[{ Priority}] {Name} || {Description} Due: {Deadline}";
        }
    }
}

