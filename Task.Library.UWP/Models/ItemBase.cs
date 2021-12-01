using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Task.Library;

namespace Task.Library.UWP.Models
{
    public class ItemBase
    {

        public event PropertyChangedEventHandler PropertyChanged;

        // properties
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id
        {
            get; set;
        }

        [BsonElement("priority")]
        public int Priority { get; set; }

        [BsonElement("name")]
        private string name;

        [BsonIgnore]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                NotifyPropertyChanged();
            }
        }

        [BsonElement("description")]
        private string description;
        [BsonIgnore]
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
                NotifyPropertyChanged();
            }
        }

        [BsonElement("isCompleted"), BsonRequired]
        private bool isCompleted;

        public bool IsCompleted {
            get
            {
                return isCompleted;
            }
            set
            {
                isCompleted = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("Completed");
            }
        }

        public void SetPriority (ItemBase task, int priority)
        {
            task.Priority = priority;
        }


        public void Complete(ItemBase task)
        {

            task.IsCompleted = true;
        }


        internal void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
