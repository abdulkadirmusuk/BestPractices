using Refit;
using RefitRestfulAPITutorial.Model;
using RefitRestfulAPITutorial.Model.Concrete;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RefitRestfulAPITutorial.Business.Abstract
{
    /* Interface seviyesinde header ekleme
        [Headers("name:gencay yildiz")]
        public interface ITodoAPI
        {
            [Get("/todos")]
            Task<List<Todo>> GetTodos();
        }
     */

    public interface ITodoAPI
    {
        //Hedef API nin base url (örn : http://jsonplaceholder.typicode.com)
        [Get("/todos")] //Örn : http://jsonplaceholder.typicode.com/todos
        public Task<List<Todo>> GetTodos();

        [Get("/todos/{id}")]
        public Task<Todo> GetTodo(int id);

        [Get("/todos?id={id}")]
        public Task<List<Todo>> GetTodosId(int id);

        [Get("/todos?id={request.Id}&UserId={request.UserId}")]
        public Task<List<Todo>> GetByIdAndUserId(TodoRequest request);

        [Get("/{**page}")]
        public Task<Todo> Get(string page);

        [Get("/todos")]
        public Task<List<Todo>> GetTodos(TodoQueryParam todoQueryParam); // “/todos/?order=desc&Limit=5”

        [Get("/todos")]
        public Task<List<Todo>> GetTodosQuery([Query("search")] TodoQueryParam todoQueryParam); // “/todos/?search.order=desc&search.Limit=10”

        [Get("/todos")]
        public Task<List<Todo>> GetTodosMulti([Query(CollectionFormat.Multi)] int[] ids); // "/todos?ids=3&ids=5&ids=7"
        
        [Get("/todos")]
        public Task<List<Todo>> GetTodosCsv([Query(CollectionFormat.Csv)] int[] ids);// "/todos?ids=3%2C5%2C7"

        [Get("/todos")]
        [QueryUriFormat(UriFormat.Unescaped)]
        public Task<List<Todo>> GetTodos(string q); // /todos?q=sebepsiz%2Bbo%C5%9F%2Byere.ayr%C4%B1lacaksan%21 şeklinde query sttring oluşmasını engeller orjinal halini alır. (orjinal : sebepsiz+boş+yere.ayrılacaksan!). Unescaped sonrası (/todos?q=sebepsiz%2Bbo%C5%9F%2Byere.ayr%C4%B1lacaksan%21)

        [Headers("name:gencay yildiz")] //metot seviyesinde header ekleme
        [Get("/todos")]
        public Task<List<Todo>> GetTodosWihHeader();

        [Get("/todos")] //dynamic header
        public Task<Todo> GetTodosWithDynamicHeader([Header("authorization")] string authorization);

        [Get("/todos/login")]
        [Headers("Authorization: Bearer")]
        public Task<string> Login();//login with bearer token
    }

}
