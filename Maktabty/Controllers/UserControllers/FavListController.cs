using Maktabty.Models;
using Maktabty.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Maktabty.Controllers.UserControllers
{
    public class FavListController : Controller
    {
        private readonly UserManager<ApplicationUser> usermanager;

        IFavListRepo favListRepo;

        public FavListController(IFavListRepo _favListRepo, UserManager<ApplicationUser> _usermanager)
        {
            favListRepo = _favListRepo;
            usermanager = _usermanager;
        }
        public IActionResult FavList()
        {

            ApplicationUser applicationUser = usermanager.GetUserAsync(HttpContext.User).Result;
            List<Book> favs = favListRepo.GetUserFavsByID(applicationUser.Id);
            return View("FavList", favs);
        }
        public IActionResult AddToFav(int id)
        {
            ApplicationUser applicationUser = usermanager.GetUserAsync(HttpContext.User).Result;
            Fav fav = new Fav();
            fav.UserId = applicationUser.Id;
            fav.BookId = id;
            var favBook = favListRepo.GetSingleFavById(applicationUser.Id, id);
            if( favBook != null)
            {
                favListRepo.RemoveFav(favBook.UserId, favBook.BookId);  
                favListRepo.FavingBook(favBook);    
            }
            else
            {
                favListRepo.FavingBook(fav);

            }
            return View("AddToFav", favBook);
        }
        public IActionResult RemoveFromFav(int id)
        {
            ApplicationUser applicationUser = usermanager.GetUserAsync(HttpContext.User).Result;
            favListRepo.RemoveFav(applicationUser.Id,id);
            return RedirectToAction("FavList");
        }
    }
}
