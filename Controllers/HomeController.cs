using Microsoft.AspNetCore.Mvc;

namespace ToDoApp.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Route("/home/{name}")]
        public string Get(string name)
        {
            return "Welcome Home";
        }
    }
}