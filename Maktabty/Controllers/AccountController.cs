using Maktabty.Models;
using Maktabty.Repositories;
using Maktabty.viewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Maktabty.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
   
        public IWebHostEnvironment HostEnvironment { get; }

        public AccountController
            (UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IWebHostEnvironment hostEnvironment)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            HostEnvironment = hostEnvironment;

        }
        //Create Account
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerUser)//username,password,address + Validation
        {
            if (ModelState.IsValid == false)
            {
                return View(registerUser);
            }
            try
            {
                //save db
                string stringFileName = uploadFile(registerUser);
                ApplicationUser userModel = new ApplicationUser();
                userModel.UserName = registerUser.UserName;
                userModel.PasswordHash = registerUser.Password;
                userModel.Address = registerUser.Address;
                userModel.Name= registerUser.Name;
                userModel.Gender = registerUser.Gender;
                userModel.BirthDate = registerUser.birthDate;
                userModel.Image = stringFileName;
                userModel.PhoneNumber = registerUser.Phone;
                IdentityResult result =
                    await userManager.CreateAsync(userModel, registerUser.Password);
                if (result.Succeeded == true)
                {
                    //ispressitant=true or not 
                    await signInManager.SignInAsync(userModel, false);
                    //authoniticat create cookie
                    return RedirectToAction("Index", "Book");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                    }
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(registerUser);
        }


        public async Task<IActionResult> Signout()
        {

            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
            //return RedirectToAction("Register");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginUser)
        {
            if (ModelState.IsValid == false)
            {
                return View(loginUser);
            }
            ApplicationUser appUser = await userManager.FindByNameAsync(loginUser.UserName);
            if (appUser != null)
            {
                bool result = await userManager.CheckPasswordAsync(appUser, loginUser.Password);
                if (result == true)
                {
                    ////cooki
                    
                    await signInManager.SignInAsync(appUser, loginUser.RememberMe);
                    //await signInManager.SignInAsync(appUser, loginUser.RememberMe);//id ,name,role
                    return RedirectToAction("Index", "Book");
                }
            }
            ModelState.AddModelError("", "User name & password in correct");
            return View(loginUser);

        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult RegisterAdmin()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> RegisterAdmin(RegisterVM registerUser)
        {
            if (ModelState.IsValid == false)
            {
                return View(registerUser);
            }
            try
            {
                //save db
                string stringFileName = uploadFile(registerUser);
                ApplicationUser userModel = new ApplicationUser();
                userModel.UserName = registerUser.UserName;
                userModel.PasswordHash = registerUser.Password;
                userModel.Address = registerUser.Address;
                userModel.Name = registerUser.Name;
                userModel.Gender = registerUser.Gender;
                userModel.BirthDate = registerUser.birthDate;
                userModel.Image = stringFileName;
                userModel.PhoneNumber = registerUser.Phone;
                IdentityResult result =
                    await userManager.CreateAsync(userModel, registerUser.Password);
                if (result.Succeeded == true)
                {
                    //regsiter user as Admin
                    await userManager.AddToRoleAsync(userModel, "admin");
                    await signInManager.SignInAsync(userModel, false);
                    //authoniticat create cookie
                    return RedirectToAction("Index", "Book");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                    }
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(registerUser);
        }
        private string uploadFile(RegisterVM registerVM)
        {
            string fileName = null;
            if (registerVM.Image != null)
            {
                fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(registerVM.Image.FileName);

                string mypath = @"C:/Users/طيبة/Desktop/Maktabty/Maktabty/wwwroot/Images/";
                string uploadDir = Path.Combine(mypath);

                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    registerVM.Image.CopyTo(fileStream);
                }
            }
            return fileName;
        }
    
    }
}
