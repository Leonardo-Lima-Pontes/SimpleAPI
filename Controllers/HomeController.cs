using Microsoft.AspNetCore.Mvc;
using ToDoApp.Data;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Route("/listastodos/")]
        public IEnumerable<Todo> Get([FromServices] Context context)
        => context.Todos.ToList();

        [HttpPost]
        [Route("/gravartodo/")]
        public Todo Post([FromBody] Todo todo, [FromServices] Context context)
        {
            context.Todos.Add(todo);
            context.SaveChanges();
            return todo;
        }
    }
}