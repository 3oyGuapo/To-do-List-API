using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using To_do_List;
using To_do_List_API.Data;
using Todo.Shared;

namespace To_do_List_API.Controllers
{
    [Route("api/[controller]")]//The route of the url
    [ApiController]//Attribute that tells this class is an api controller
    public class TasksController : ControllerBase
    {
        private readonly TodoDbContext _context;

        public TasksController(TodoDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// A method that deals with the http get request
        /// </summary>
        /// <returns>Return a collection of ienumerable lists from todo list file</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTasks()
        {
            //Return the Lists
            var tasks = await _context.Tasks.ToListAsync();
            return Ok(tasks);
        }

        /// <summary>
        /// Get a task from the list by their id number
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A list by id given</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetTask(int id)
        {
            //Find the element from the lists that has the same id
            var task = await _context.Tasks.FindAsync(id);

            //Security check ensure it is not null
            if (task == null)
            {
                //Return 404 not found code
                return NotFound();
            }

            //Return the signle list
            return Ok(task);
        }

        /// <summary>
        /// Create a task
        /// </summary>
        /// <param name="newContent"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<TaskItem>> CreateTask(CreateTaskDto taskDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newTask = new TaskItem
            {
                Description = taskDto.Description,
                IsCompleted = taskDto.IsCompleted,
                Priority = taskDto.Priority
            };

            //Use addList function to add new content as new list
            _context.Tasks.Add(newTask);

            await _context.SaveChangesAsync();

            //Return it with the name of the list, the id of new list and the content
            return CreatedAtAction(nameof(GetTask), new { id = newTask.Id }, newTask);
        }

        /// <summary>
        /// Update a task by given id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedList"></param>
        /// <returns>Return a success code 204 that tells success or a 404 NotFound code</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, TaskItem task)
        {
            if (id != task.Id)
            {
                return BadRequest(); //If not match, send response of 400 bad request
            }

            _context.Entry(task).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Tasks.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();//Return 204 code meaning success but no content response
        }

        /// <summary>
        /// Delete a list by given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return a success code 204 that tells success or a 404 NotFound code</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
