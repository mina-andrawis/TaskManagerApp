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

        // { set; get;} automatically creates private fields , no need for declaring _deadline or _isCompleted
        public DateTime Deadline { get; set; }


        public DateTimeOffset boundDeadline;

        TimeSpan BoundDeadlineTime
        {
            get => Deadline.TimeOfDay;
            set
            {
                Deadline = Deadline.AddTicks(value.Ticks);
            }
        }

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
            return $"[{Priority}] {Name} || {Description} Due: {Deadline}";
        }
    }
}

