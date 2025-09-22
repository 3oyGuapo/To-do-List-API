using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using To_do_List;

namespace To_do_List_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListsController : ControllerBase
    {
        public static readonly TodoListController _controller = new TodoListController();
        [HttpGet]
        public ActionResult<IEnumerable<ListContent>> GetLists()
        {
            return Ok(_controller.Lists);
        }
    }
}
