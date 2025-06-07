using DotNet_API.Domain;
using Microsoft.AspNetCore.Mvc;

namespace DotNet_API.Controllers
{


    [ApiController]
    [Route("[controller]")]
    public class MembersController : ControllerBase
    {
        private readonly ILogger<MembersController> _logger; // Corrected type to match PersonController
        private static readonly Member[] People = new[]
        {
            new Member
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1990, 1, 1),
                Email = "john.doe@example.com",
                PhoneNumber = "123-456-7890",
                Address = "123 Main St",
                City = "Anytown",
                State = "CA",
                ZipCode = "90001",
                Country = "USA"
            },
            new Member
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Smith",
                DateOfBirth = new DateTime(1985, 5, 15),
                Email = "jane.smith@example.com",
                PhoneNumber = "987-654-3210",
                Address = "456 Oak Ave",
                City = "Othertown",
                State = "NY",
                ZipCode = "10001",
                Country = "USA"
            }
        };

        public MembersController(ILogger<MembersController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetMembers")]
        public IEnumerable<Member> Get()
        {
            IEnumerable<Member> people = People.Select(person => new Member
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                DateOfBirth = person.DateOfBirth,
                Email = person.Email,
                PhoneNumber = person.PhoneNumber,
                Address = person.Address,
                City = person.City,
                State = person.State,
                ZipCode = person.ZipCode,
                Country = person.Country
            });
            return people;
        }
    }
}
