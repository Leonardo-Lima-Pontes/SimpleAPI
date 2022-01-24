using Microsoft.AspNetCore.Mvc;
using ToDoApp.Data;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Route("/listtodos/")]
        public IEnumerable<Todo> Get([FromServices] Context context)
        => context.Todos.ToList();

        [HttpGet]
        [Route("/listtodo/{id:int}")]
        public Todo GetById([FromRoute] int id, [FromServices] Context context)
        => context.Todos.FirstOrDefault(x => x.Id == id);

        [HttpPost]
        [Route("/savetodo/")]
        public Todo Post([FromBody] Todo todo, [FromServices] Context context)
        {
            context.Todos.Add(todo);
            context.SaveChanges();
            return todo;
        }

        [HttpPut]
        [Route("/updattodo/{id:int}")]
        public string Put([FromRoute] int id, [FromBody] Todo todo, [FromServices] Context context)
        {
            var oldTodo = context.Todos.FirstOrDefault(x => x.Id == id);
            if (oldTodo is null) return "Todo not found";

            oldTodo.Name = todo.Name;
            oldTodo.Done = todo.Done;
            oldTodo.CreatedAt = todo.CreatedAt;

            context.Todos.Update(oldTodo);
            context.SaveChanges();
            return "The todo has been updated successufuly";
        }

        [HttpDelete]
        [Route("/deletetodo/{id:int}")]
        public string Delete([FromRoute] int id, [FromServices] Context context)
        {
            var todo = context.Todos.FirstOrDefault(x => x.Id == id);
            if (todo is null) return "Todo not found";
            context.Todos.Remove(todo);
            context.SaveChanges();
            return "Todo deleted successufuly";
        }
    }
}