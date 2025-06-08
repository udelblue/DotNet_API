using System.Text.Json;

namespace DotNet_API.Domain
{
    public class Member
    {

        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public string? City { get; set; } = string.Empty;
        public string? State { get; set; } = string.Empty;
        public string? ZipCode { get; set; } = string.Empty;
        public string? Country { get; set; } = string.Empty;

        public bool isActive { get; set; } = true;

        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }




    }
}
