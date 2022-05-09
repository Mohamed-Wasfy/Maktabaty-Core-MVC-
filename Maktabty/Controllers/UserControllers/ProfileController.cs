using Maktabty.Models;
using Maktabty.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Maktabty.Controllers.UserControllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        IProfileRepo profileRepo;

        public ProfileController( IProfileRepo _profileRepo, UserManager<ApplicationUser> _userManager)
        {
            userManager = _userManager; 
            profileRepo = _profileRepo;
        }
    
        public IActionResult Profile()
        {
            var userId = userManager.GetUserId(HttpContext.User);
            if( userId == null )
            {
                return RedirectToAction("index", "book");
            }

            else
            {
                ApplicationUser applicationUser = userManager.FindByIdAsync(userId).Result;

                return View("Profile", applicationUser);
            }
           

        }
    }
}
