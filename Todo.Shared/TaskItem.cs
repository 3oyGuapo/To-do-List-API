using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace Todo.Shared//A project that shares the classes between different projects
{
    /// <summary>
    /// Class that define the data structures stored in the todo list
    /// </summary>
    public class TaskItem : INotifyPropertyChanged
    {
        //Declare a propertychanged event to notify the changes of property
        public event PropertyChangedEventHandler? PropertyChanged;//? define it is a nullable type
        //Declare private fields to store the value
        private string _description = string.Empty;//Assigning default value to avoid the string being null
        private bool _isCompleted;
        private string _priority = "Medium";//Default value to be medium incase did not set priority when creating new task

        //Constructor to initialize the fields
        public TaskItem(string description)
        {
            _description = description;
            _isCompleted = false;
        }

        public TaskItem() { }

        //Description property to encapsulate the field
        public string Description
        {
            get { return _description; }
            set
            {
                //Check does the new value equal to current field's value or not
                if (_description != value)
                {
                    //If not the same then update field's value with current value and call the function to inform subscribers to update
                    _description = value;
                    OnPropertyChanged(nameof(Description));//When property's value changed, notify by calling the method. Using nameof to convert the name into string and send as parameter
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

        public string Priority
        {
            get { return _priority; }
            set
            {
                if (value != _priority)
                {
                    _priority = value;
                    OnPropertyChanged(nameof(Priority));
                }
            }
        }

        public int Id { get; set; }

        //Method to invoke the event with a safety check
        protected void OnPropertyChanged(string propertyName)
        {
            //Invoke to send message to subscribers which is the wpf app interface that subscribes/bind to the properties
            //Sender is this which is this class TaskItem, and the information is propertyName that is changed
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));//? for safety check, if is null then not execute invoke
        }

        //Override the ToString method that return Description when directly used to represent the content
        public override string ToString()
        {
            return Description;
        }
    }
}
