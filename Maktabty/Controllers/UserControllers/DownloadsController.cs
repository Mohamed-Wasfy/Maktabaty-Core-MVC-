using Maktabty.Models;
using Maktabty.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Maktabty.Controllers.UserControllers
{
    public class DownloadsController : Controller
    {
        private readonly UserManager<ApplicationUser> usermanager;
        IDownloadsRepo downloadsRepo;

        public DownloadsController(IDownloadsRepo _downloadsRepo, UserManager<ApplicationUser> _usermanager)
        {
            usermanager = _usermanager;
            downloadsRepo = _downloadsRepo;
        }
        public IActionResult Downloads()
        {
            ApplicationUser applicationUser = usermanager.GetUserAsync(HttpContext.User).Result;
            List<Book> downloads = downloadsRepo.GetUserDownloadsByID(applicationUser.Id);
            return View("Downloads",downloads);
        }

        public IActionResult Download(int id)
        {
            ApplicationUser applicationUser = usermanager.GetUserAsync(HttpContext.User).Result;

            Downloads download = new Downloads() {
                UserId = applicationUser.Id,
                 BookId = id
               };
            var bookDownloaded= downloadsRepo.GetSinglEDownloadById(applicationUser.Id, id);
            if(bookDownloaded != null)
            {
                downloadsRepo.RemoveDownload(applicationUser.Id, id);
                downloadsRepo.DownloadBook(download);
            }
            else
            {
                downloadsRepo.DownloadBook(download);

            }
            return View("Download", download);
        }


        public IActionResult RemoveDownload(int id)
        {
            ApplicationUser applicationUser = usermanager.GetUserAsync(HttpContext.User).Result;
            downloadsRepo.RemoveDownload(applicationUser.Id, id);
            return RedirectToAction("Downloads");
        }
    }
}
