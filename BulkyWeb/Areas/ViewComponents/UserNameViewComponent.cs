using BulkyBook.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Security.Claims;

namespace BulkyBookWeb.Areas.ViewComponents
{
    public class UserNameViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserNameViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (claim != null)
            {
                if (HttpContext.Session.GetString("UserName") == null)
                {
                    HttpContext.Session.SetString("UserName",
                        _unitOfWork.ApplicationUserRepository.Get(s => s.Id == claim.Value).Name);
                }
                return View(HttpContext.Session.GetString("UserName"));
            }
            else
            {
                HttpContext.Session.Remove("UserName");
                return View("");
            }
        }
    }
}
