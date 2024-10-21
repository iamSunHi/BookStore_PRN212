using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Models
{
    public class ApplicationUser
    {
        [Key]
        public string Id { get; set; } = null!; // GUID
        public string UserName { get; set; } = null!;
        public string? Email { get; set; }
        public string Password { get; set; } = null!;

        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }

        public int? CompanyId { get; set; }
        public Company? Company { get; set; }

        public string Role { get; set; } = null!;
    }
}
