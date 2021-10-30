using System;
using System.Collections.Generic;

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
        }

        // { set; get;} automatically creates private fields , no need for declaring _deadline or _isCompleted
        public DateTime Deadline { get; set; }
        




        public override string ToString()
        {
            return $"{Id}. {Name} || {Description} Due: {Deadline}";
        }
    }
}

