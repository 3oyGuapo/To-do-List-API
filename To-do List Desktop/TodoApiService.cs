using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using Todo.Shared;

namespace To_do_List_Desktop
{
    internal class TodoApiService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:7089";//Need to replace with your url

        public TodoApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<TaskItem>> GetAllTasksAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<TaskItem>>($"{BaseUrl}/api/tasks");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error fetching tasks: {ex.Message}");
                return new List<TaskItem>();
            }
        }

        public async Task<TaskItem> CreateTaskAsync(CreateTaskDto newTaskDto)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/api/Tasks", newTaskDto);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<TaskItem>();
        }

        public async Task UpdateTaskAsync(TaskItem taskToUpdate)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/api/tasks/{taskToUpdate.Id}", taskToUpdate);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteTaskAsync(int taskId)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"{BaseUrl}/api/tasks/{taskId}");
            response.EnsureSuccessStatusCode();
        }
    }
}
