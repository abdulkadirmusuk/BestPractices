using Microsoft.AspNetCore.Mvc;
using RefitRestfulAPITutorial.Business.Abstract;
using System.Threading.Tasks;

namespace RefitRestfulAPITutorial.Controllers
{
    [Route("api/[controller]")]
    public class BusinessAccessController : ControllerBase
    {
        //n layer architecture use refit
        public ITodoService _todoService;
        public BusinessAccessController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetMyTodoList()
        {
            return Ok(await _todoService.GetMyToDoList());
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetMyTodoListById(int id)
        {
            return Ok(await _todoService.GetTodoById(id));
        }
    }
}
