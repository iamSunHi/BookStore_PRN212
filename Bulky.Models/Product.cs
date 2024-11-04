using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Models
{
    public class Product
	{
		public int Id { get; set; }
		public string Title { get; set; } = null!;
		public string? Description { get; set; }
		public string ISBN { get; set; } = null!;
		public string Author { get; set; } = null!;
        [ValidateNever]
        public string ImageUrl { get; set; } = null!;
		public double Price { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }
		[ValidateNever]
		public Category Category { get; set; } = null!;

        [Display(Name = "Cover Type")]
        public int CoverTypeId { get; set; }
        [ValidateNever]
        public CoverType? CoverType { get; set; }
    }
}
