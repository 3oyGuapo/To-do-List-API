using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Shared;

namespace To_do_List
{
    public class TodoListController
    {
        //A constant variable stores the file path, for this it will be store in the same folder with the project since did not specify the directory
        private const string filePath = "To-do List.txt";
        
        //Define a new List as ObservableCollection to store ListContent, private set to restrict only member in current class can assign new values to Lists
        public ObservableCollection<TaskItem> Lists { get; private set; }

        //Constructor that initialize the Lists by calling LoadListsFromFile to get a list
        public TodoListController()
        {
            Lists = LoadListsFromFile();
        }

        //Method that save all the lists in a given file path
        public void SaveListsToFile()
        {
            //Select Lists to iterate and get Description and IsCompleted property and return as a string lists.
            var lines = Lists.Select(list => $"{list.IsCompleted}|{list.Description}").ToList();
            File.WriteAllLines(filePath, lines);//Store lines in the given filepath
        }

        //Private method that load todo list from the file path given, only execute when the object is initialized
        private ObservableCollection<TaskItem> LoadListsFromFile()
        {
            //Object that stores the contents from the filepath given if exist
            var lists = new ObservableCollection<TaskItem>();//use var to shorten the length

            //Check the file exists
            if (File.Exists(filePath))
            {
                //Store all the contents from the file in an array
                string[] lines = File.ReadAllLines(filePath);

                //Loop each elements in the array
                foreach (string line in lines)
                {
                    //Split the element with | into an array with 2 different values, description and isCompleted
                    string[] parts = line.Split('|');

                    //Validity check of correct content, and convert string to bool
                    if (parts.Length == 2 && bool.TryParse(parts[0], out bool isCompleted))
                    {
                        //Initialize the listcontent by adding the second element in constructor to initialize and define IsCompleted property with the parsed bool value
                        var loadedList = new TaskItem(parts[1]) {IsCompleted = isCompleted};
                        lists.Add(loadedList);//Add to the list
                    }
                }
            }

            return lists;
        }

        //Add method that adds the new content into the Lists and perform a save
        public void AddList(TaskItem newList)
        {
            if (!string.IsNullOrEmpty(newList.Description) && !string.IsNullOrWhiteSpace(newList.Description))
            {
                Lists.Add(newList);
                SaveListsToFile();
            }
            
        }

        //Delete method that removes the new content into the Lists and perform a save
        public void DeleteList(TaskItem deleteList)
        {
            Lists.Remove(deleteList);
            SaveListsToFile();
        }
    }
}
