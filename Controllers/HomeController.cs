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
        public IActionResult Get([FromServices] Context context)
        => Ok(context.Todos.ToList());

        [HttpGet]
        [Route("/listtodo/{id:int}")]
        public IActionResult GetById([FromRoute] int id, [FromServices] Context context)
        {
            var todo = context.Todos.FirstOrDefault(x => x.Id == id);
            if (todo is null) return NotFound();
            return Ok(todo);
        }

        [HttpPost]
        [Route("/savetodo/")]
        public IActionResult Post([FromBody] Todo todo, [FromServices] Context context)
        {
            context.Todos.Add(todo);
            context.SaveChanges();
            return Created($"/savetodo/{todo.Id}", todo);
        }

        [HttpPut]
        [Route("/updattodo/{id:int}")]
        public IActionResult Put([FromRoute] int id, [FromBody] Todo todo, [FromServices] Context context)
        {
            var oldTodo = context.Todos.FirstOrDefault(x => x.Id == id);
            if (oldTodo is null) return NotFound("Todo not found");

            oldTodo.Name = todo.Name;
            oldTodo.Done = todo.Done;
            oldTodo.CreatedAt = todo.CreatedAt;

            context.Todos.Update(oldTodo);
            context.SaveChanges();
            return Ok("The todo has been updated successufuly");
        }

        [HttpDelete]
        [Route("/deletetodo/{id:int}")]
        public IActionResult Delete([FromRoute] int id, [FromServices] Context context)
        {
            var todo = context.Todos.FirstOrDefault(x => x.Id == id);
            if (todo is null) return NotFound("Todo not found");
            context.Todos.Remove(todo);
            context.SaveChanges();
            return Ok("Todo deleted successufuly");
        }
    }
}