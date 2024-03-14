using Microsoft.AspNetCore.Mvc;
using test_swagger_api.Data;
using test_swagger_api.Models;

namespace test_swagger_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly AppDbContext Context;
        public PersonsController(AppDbContext context) {
            Context = context;
        }

        [HttpGet("{id}")]
        public async Task<Person> Get(int id)
        {
            var person = await Context.Person.FindAsync(id);
            return person ?? throw new Exception(message: "Not found");
        }
    }
}
