using test_swagger_api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
namespace test_swagger_api.Models
{
    public class Person (int id, string name, int age)
    {
        public int Id { get; set; } = id;
        public string Name { get; set; } = name;
        public int Age { get; set; } = age;
    }


public static class PersonEndpoints
{
	public static void MapPersonEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Person").WithTags(nameof(Person));

        group.MapGet("/", async (AppDbContext db) =>
        {
            return await db.Person.ToListAsync();
        })
        .WithName("GetAllPeople")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Person>, NotFound>> (int id, AppDbContext db) =>
        {
            return await db.Person.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Person model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetPersonById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Person person, AppDbContext db) =>
        {
            var affected = await db.Person
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Id, person.Id)
                  .SetProperty(m => m.Name, person.Name)
                  .SetProperty(m => m.Age, person.Age)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdatePerson")
        .WithOpenApi();

        group.MapPost("/", async (Person person, AppDbContext db) =>
        {
            db.Person.Add(person);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Person/{person.Id}",person);
        })
        .WithName("CreatePerson")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, AppDbContext db) =>
        {
            var affected = await db.Person
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeletePerson")
        .WithOpenApi();
    }
}}
