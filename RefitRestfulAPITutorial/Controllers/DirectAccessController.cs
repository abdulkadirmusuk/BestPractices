using Microsoft.AspNetCore.Mvc;
using Refit;
using RefitRestfulAPITutorial.Authorization;
using RefitRestfulAPITutorial.Business.Abstract;
using RefitRestfulAPITutorial.Model.Concrete;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace RefitRestfulAPITutorial.Controllers
{
    [Route("api/[controller]")]
    public class DirectAccessController : ControllerBase
    {
        //with refit base interface get all data
        readonly ITodoAPI _todoAPI;

        public DirectAccessController(ITodoAPI todoAPI)
        {
            _todoAPI = todoAPI;
        }

        [HttpGet("GetTodos")]
        public async Task<IActionResult> GetTodos()
        {
            return Ok(await _todoAPI.GetTodos());
        }

        [HttpGet("GetTodosById")]
        public async Task<IActionResult> GetTodosById(int id)
        {
            return Ok(await _todoAPI.GetTodo(id));
        }

        [HttpGet("GetTodosId")]
        public async Task<IActionResult> GetTodosId(int id)
        {
            return Ok(await _todoAPI.GetTodosId(id));
        }

        /// <summary>
        /// GetByIdAndUserId
        /// </summary>
        /// <param TodoRequest="request">incoming todo request</param>
        [HttpGet("GetByIdAndUserId")]
        public async Task<IActionResult> GetByIdAndUserId(TodoRequest request)
        {
            return Ok(await _todoAPI.GetByIdAndUserId(request));
        }

        [HttpGet("GetBearerToken")]
        public async Task<IActionResult> GetBearerToken()
        {
            var api = RestService.For<ITodoAPI>(new HttpClient(new AuthenticatedHttpClientHandler(() =>
            {
                //Sunucudan token talep edilebilir...
                return Task.FromResult("Xsjfa23fjfaSfXCzxgmodgmrmr");
            }))
            {
                BaseAddress = new Uri("https://localhost:44344")
            });

            await api.Login();
            HttpRequestMessage message = new HttpRequestMessage();
            var isAuth = message.Headers.Authorization.ToString();
            if (string.IsNullOrWhiteSpace(isAuth))
            {
                return Ok("Token var. Başarılı giriş yapıldı!");
            }
            else return Unauthorized();
        }
    }
}
