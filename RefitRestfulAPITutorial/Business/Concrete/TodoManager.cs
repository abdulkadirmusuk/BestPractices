using Microsoft.Extensions.Configuration;
using Refit;
using RefitRestfulAPITutorial.Business.Abstract;
using RefitRestfulAPITutorial.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RefitRestfulAPITutorial.Services
{
    public class TodoManager : ITodoService
    {
        private readonly IConfiguration _configuration;
        public TodoManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<List<Todo>> GetMyToDoList()
        {
            var request = RestService.For<ITodoAPI>(_configuration["BaseUrl"].ToString());
            List<Todo> todos = await request.GetTodos();
            return todos;
        }

        public async Task<Todo> GetTodoById(int id)
        {
            ITodoAPI todoAPI = RestService.For<ITodoAPI>(_configuration["BaseUrl"].ToString());
            Todo todo = await todoAPI.GetTodo(id);
            return todo;
        }
    }
}
