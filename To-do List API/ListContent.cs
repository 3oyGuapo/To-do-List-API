using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace To_do_List
{
    public class ListContent : INotifyPropertyChanged
    {
        //Declare a propertychanged event to notify the changes of property
        public event PropertyChangedEventHandler PropertyChanged;
        //Declare private fields to store the value
        private string _description;
        private bool _isCompleted;

        //Constructor to initialize the fields
        public ListContent(string description)
        {
            _description = description;
            _isCompleted = false;
        }

        //Description property to encapsulate the field
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged(nameof(Description));//When property's value changed, notify by calling the method
                }
            }
        }

        //IsCompleted property to encapsulate the field
        public bool IsCompleted
        {
            get { return _isCompleted; }
            set
            {
                if (_isCompleted != value)
                {
                    _isCompleted = value;
                    OnPropertyChanged(nameof(IsCompleted));
                }
            }
        }

        public int Id { get; set; }
        
        //Method to invoke the event with a safety check
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));//Invode when called and check is null or not before calling for safety check
        }

        //Override the ToString method that return Description when directly used to represent the content
        public override string ToString()
        {
            return Description;
        }
    }
}
