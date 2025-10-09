using Xunit;
using Microsoft.EntityFrameworkCore;
using To_do_List_API.Controllers;
using To_do_List_API.Data;
using Todo.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Tests
{
    public class TasksControllerTests
    {
        private readonly DbContextOptions<TodoDbContext> _dbOptions;

        public TasksControllerTests()
        {
            _dbOptions = new DbContextOptionsBuilder<TodoDbContext>().UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString()).Options;
        }

        [Fact]
        public async Task GetTasks_ReturnsAllTasks()
        {
            using (var context = new TodoDbContext(_dbOptions))
            {
                context.Tasks.Add(new TaskItem { Description = "Task 1" });
                context.Tasks.Add(new TaskItem { Description = "Task 2" });
                await context.SaveChangesAsync();
            }

            using (var context = new TodoDbContext(_dbOptions))
            {
                var controller = new TasksController(context);

                var result = await controller.GetTasks();

                var actionResult = Assert.IsType<ActionResult<IEnumerable<TaskItem>>>(result);
                var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
                var tasks = Assert.IsAssignableFrom<IEnumerable<TaskItem>>(okResult.Value);
                Assert.Equal(2, tasks.Count());
            }
        }

        [Fact]
        public async Task CreateTask_AddNewTaskToDatabase()
        {
            
            using (var context = new TodoDbContext(_dbOptions))
            {
                var controller = new TasksController(context);
                var newTaskDto = new CreateTaskDto { Description = "New Task from Test" };

                var result = await controller.CreateTask(newTaskDto);

                Assert.Equal(1, await context.Tasks.CountAsync());
                var addedTask = await context.Tasks.FirstAsync();
                Assert.Equal("New Task from Test", addedTask.Description);

                var actionResult = Assert.IsType<ActionResult<TaskItem>>(result);
                var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
                Assert.Equal("GetTask", createdAtActionResult.ActionName);
            }
        }

        [Fact]
        public async Task GetTask_WithNonExistentId()
        {
            using (var context = new TodoDbContext(_dbOptions))
            {
                var controller = new TasksController(context);

                var result = await controller.GetTask(999);

                var actionResult = Assert.IsType<ActionResult<TaskItem>>(result);
                Assert.IsType<NotFoundResult>(actionResult.Result);
            }
        }
    }
}