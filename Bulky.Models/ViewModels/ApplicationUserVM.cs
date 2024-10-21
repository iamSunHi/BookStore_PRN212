using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBook.Models.ViewModels
{
    public class ApplicationUserVM
	{
		public ApplicationUser ApplicationUser { get; set; }
		public IEnumerable<Company> Companies { get; set; }
	}
}
