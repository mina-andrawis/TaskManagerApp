using System;
using System.Collections.Generic;
using System.Text;

namespace Task.Library
{
    class Appointment : ItemBase
    {
        public DateTime Start { set; get; }
        public DateTime Stop { set; get; }
        public List<String> Attendees { set; get; }
        


        
    }
}
