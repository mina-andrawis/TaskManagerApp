using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Task.Library.UWP.ViewModels
{
    class DateViewModel
    {
        private DateTimeOffset boundDate;
        public DateTimeOffset BoundDate
        {
            get
            {
                return boundDate;
            }
            set
            {
                boundDate = value;
                Date = boundDate.Date;
                NotifyPropertyChanged("Date");
            }
        }

        public DateTime Date { get; set; }


        public DateViewModel()
        {

            BoundDate = DateTime.Now;
            //DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
