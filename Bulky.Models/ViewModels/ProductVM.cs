using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBook.Models.ViewModels
{
	public class ProductVM
	{
		public Product Product { get; set; } = new Product();

        [ValidateNever]
		public IEnumerable<Category> CategoryList { get; set; }
		[ValidateNever]
		public IEnumerable<CoverType> CoverTypeList { get; set; }
		public int Quantity { get; set; }

	}
}
