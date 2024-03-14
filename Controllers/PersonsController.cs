using Microsoft.AspNetCore.Mvc;
using test_swagger_api.Models;

namespace test_swagger_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonsController : ControllerBase
    {

        public PersonsController() { }

        [HttpGet("{id}")]
        public Person Get()
        {
            return new Person(id: 1, name: "John", age: 30);
        }
    }
}
