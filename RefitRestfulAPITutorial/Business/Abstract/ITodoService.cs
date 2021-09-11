using RefitRestfulAPITutorial.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RefitRestfulAPITutorial.Business.Abstract
{
    public interface ITodoService
    {
        public Task<List<Todo>> GetMyToDoList();
        public Task<Todo> GetTodoById(int id);
    }
}
