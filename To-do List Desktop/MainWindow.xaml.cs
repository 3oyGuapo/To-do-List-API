using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Todo.Shared;

namespace To_do_List_Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly TodoApiService _apiService;
        public ObservableCollection<TaskItem> Tasks { get; set; }//Define a ObservableCollection list to store the values

        public MainWindow()
        {
            InitializeComponent();

            _apiService = new TodoApiService();
            Tasks = new ObservableCollection<TaskItem>();

            todoListBox.ItemsSource = Tasks;//Binding todoListBox with Tasks

            //Loading event
            this.Loaded += Window_Loaded;
        }

        private async Task LoadTasks()
        {
            var tasksFromServer = await _apiService.GetAllTasksAsync();
            Tasks.Clear();

            //When loading, order tasks in desending order by their priority value
            var sortedTasks = tasksFromServer.OrderByDescending(tasks => tasks.PriorityValue);

            foreach (var task in sortedTasks)
            {
                Tasks.Add(task);
            }
        }

        /// <summary>
        /// Add task button where a new task will be added to the database and show on the WPF app
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //Check the description is not null or whitespace
            if (!string.IsNullOrWhiteSpace(inputTextBox.Text))
            {
                //New variable based on the input
                var newTaskDto = new CreateTaskDto 
                { 
                    Description = inputTextBox.Text,//Assign value based on the textbox's text
                    Priority = priorityComboBox.Text//Assign value based on the text of selected index of combo box
                };

                //Send to api and get a new data back
                var createdTask = await _apiService.CreateTaskAsync(newTaskDto);

                //Adding new task in order of priority
                //Get value of newly created task
                int createdTaskPriorityValue = createdTask.PriorityValue;
                //Set default to false for not inserted yet
                bool inserted = false;

                //Iterate over the Tasks list to find any preivous values is smaller than the new one
                for (int i = 0; i < Tasks.Count; i++)
                {
                    //Find and insert in that index and mark as inserted true
                    if (Tasks[i].PriorityValue < createdTaskPriorityValue)
                    {
                        Tasks.Insert(i, createdTask);
                        inserted = true;
                        break;
                    }
                }

                //If not been inserted then add to the back of list
                if (!inserted)
                {
                    Tasks.Add(createdTask);
                }

                //inputTextBox.Text = "";
            }
            
        }

        //Button event that delete a selected list from the todo list
        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            //Check if the selected item is a ListContent type
            if (todoListBox.SelectedItem is TaskItem selectedTask)
            {
                await _apiService.DeleteTaskAsync(selectedTask.Id);

                Tasks.Remove(selectedTask);
            }
            else
            {
                MessageBox.Show("Delete fail, selete a valid task to delete,");
            }
        }

        //When window closing event
        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadTasks();
        }

        /// <summary>
        /// CheckBox click event which informs api to update the database when the IsCompleted status of a task on the WPF app changes by user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            //Check that the sender is TaskItem
            if (((FrameworkElement)sender).DataContext is TaskItem taskToUpdate)
            {
                await _apiService.UpdateTaskAsync(taskToUpdate);//Calls method to update
            }
        }

        
    }
}