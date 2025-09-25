using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using To_do_List;

namespace To_do_List_API.Controllers
{
    [Route("api/[controller]")]//The route of the url
    [ApiController]//Attribute that tells this class is an api controller
    public class ListsController : ControllerBase
    {
        private static readonly TodoListController _controller = new TodoListController();

        /// <summary>
        /// A method that deals with the http get request
        /// </summary>
        /// <returns>Return a collection of ienumerable lists from todo list file</returns>
        [HttpGet]
        public ActionResult<IEnumerable<ListContent>> GetLists()
        {
            //Return the Lists
            return Ok(_controller.Lists);
        }

        /// <summary>
        /// Get a task from the list by their id number
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A list by id given</returns>
        [HttpGet("{id}")]
        public ActionResult<ListContent> GetList(int id)
        {
            //Find the element from the lists that has the same id
            var list = _controller.Lists.FirstOrDefault(list => list.Id == id);

            //Security check ensure it is not null
            if (list == null)
            {
                //Return 404 not found code
                return NotFound();
            }

            //Return the signle list
            return Ok(list);
        }

        /// <summary>
        /// Create a list
        /// </summary>
        /// <param name="newContent"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<ListContent> CreateList (ListContent newContent)
        {
            //Use addList function to add new content as new list
            _controller.AddList(newContent);

            //Return it with the name of the list, the id of new list and the content
            return CreatedAtAction(nameof(GetList), new { id = newContent.Id }, newContent);
        }

        /// <summary>
        /// Update a list by given id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedList"></param>
        /// <returns>Return a success code 204 that tells success or a 404 NotFound code</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateList(int id, ListContent updatedList)
        {
            var existingList = _controller.Lists.FirstOrDefault(list => list.Id == id);

            if (existingList == null)
            {
                return NotFound();
            }

            _controller.SaveListsToFile();
            return NoContent();//Return 204 code meaning success but no content response
        }

        /// <summary>
        /// Delete a list by given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return a success code 204 that tells success or a 404 NotFound code</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteList(int id)
        {
            var listToDelete = _controller.Lists.FirstOrDefault(list => list.Id == id);

            if (listToDelete == null)
            {
                return NotFound();
            }

            _controller.DeleteList(listToDelete);
            return NoContent();
        }
    }
}
