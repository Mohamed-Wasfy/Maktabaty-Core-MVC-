using Maktabty.Models;
using Maktabty.Repositories;
using Maktabty.viewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;

namespace Maktabty.Controllers
{
    [Authorize(Roles ="admin")]
    public class AdminController : Controller
    {
       
        IAdminRepository adminRepository;
        
        public AdminController(IAdminRepository admRepo)
        {
            this.adminRepository = admRepo;
        }

        public IActionResult Books()
        {
           
            List<Book> books = adminRepository.getAllBooks();
          
            return View("Books",books);
        }
        public  IActionResult BookDetails(int id)
        {
            Book book = adminRepository.getBookById(id);
            
            return View("BookDetails",book);
        }
        public IActionResult DeleteBook(int id)
        {
            int numOfDeletd = adminRepository.deleteBook(id);
            return RedirectToAction("Books");           
        }

        [HttpGet]
        public IActionResult NewBook()
        {
            List<Category> Cats = adminRepository.getAllCategories();
            ViewData["Cats"] = Cats;
            List<Author> Authors = adminRepository.getAllAuthors();
            ViewData["Authors"] = Authors;
            return View("NewBook",new addBookVM());
        }

        [HttpPost]
        public IActionResult SaveNewBook(addBookVM newBook)
        {
            if(ModelState.IsValid)
            {
                string stringImageName = uploadImageFile(newBook);
                string stringFileName = uploadFile(newBook);
                Book book = new Book
                {
                    Name = newBook.Name,
                    PublishDate=newBook.PublishDate,
                    Language=newBook.Language,
                    Description=newBook.Description,
                    Pages=newBook.Pages,
                    IsFeatured=false,
                    NumOfDownloads=0,
                    CategoryId=newBook.CategoryId,
                    Image= stringImageName,
                    book=stringFileName
                };
                adminRepository.insertBook(book);
                return RedirectToAction("Books");
            }
            List<Book> books = adminRepository.getAllBooks();
            ViewData["books"] = books;
            return View(newBook);
        }

        [HttpGet]
        public IActionResult EditBook(int id)
        {
            Book bookSample = adminRepository.getBookById(id);
            addBookVM bookvm = new addBookVM()
            {
                Id= bookSample.Id,
                Name=bookSample.Name,
                PublishDate=bookSample.PublishDate, 
                Language=bookSample.Language,   
                Description=bookSample.Description,
                Pages=bookSample.Pages,
                IsFeatured =false,
                NumOfDownloads=0,
                CategoryId=bookSample.CategoryId,
            };
            List<Category> Cats = adminRepository.getAllCategories();
            ViewData["Cats"] = Cats;
            List<Author> Authors = adminRepository.getAllAuthors();
            ViewData["Authors"] = Authors;
            return View("EditBook", bookvm);
        }

        [HttpPost]
        public IActionResult SaveEditBook(int id, addBookVM bookView)
        {
            if (ModelState.IsValid)
            {
            
                adminRepository.updateBook(id, bookView);

                return RedirectToAction("Books");
            }
            
            return View("EditBook", bookView);
        }

        public IActionResult getBooksByCategory(int categId)
        {
            List<Book> bookList = adminRepository.getBooksByCategoryId(categId);
            return View(bookList);
        }
        public IActionResult Categories()
        {
            List<Category> cats = adminRepository.getAllCategories();
            
            return View("Categories",cats);
        }

        [HttpGet]
        public IActionResult NewCategory()
        {
            List<Category> Cats = adminRepository.getAllCategories();
            ViewData["Cats"] = Cats;
            return View("NewCategory", new Category());
        }

        [HttpPost]
        public IActionResult SaveNewCategory(Category newCategory)
        {
            if (ModelState.IsValid)
            {
                adminRepository.insertCategory(newCategory);
                return RedirectToAction("Categories");
            }
            List<Category> category  = adminRepository.getAllCategories();
            ViewData["category"] = category;
            return View(newCategory);
        }

        public IActionResult DeleteCategory(int id)
        {
            adminRepository.RemoveCategory(id);
            return RedirectToAction("Categories");
        }
        public IActionResult Authors()
        {
            List<Author> authors = adminRepository.getAllAuthors();

            return View("Authors", authors);
        }

        public IActionResult DeleteAuthor(int id)
        {
            int numOfDeletd = adminRepository.deleteAuthor(id);
            return RedirectToAction("Authors");
        }

        [HttpGet]
        public IActionResult NewAuthor()
        {
            List<Author> Authors = adminRepository.getAllAuthors();
            ViewData["Authors"] = Authors;
            return View("NewAuthor", new Author());
        }

        [HttpPost]
        public IActionResult SaveNewAuthor(Author newAuthor)
        {
            if (ModelState.IsValid)
            {
                adminRepository.insertAuthor(newAuthor);
                return RedirectToAction("Authors");
            }
            List<Author> authors = adminRepository.getAllAuthors();
            ViewData["Authors"] = authors;
            return View(newAuthor);
        }

        public IActionResult Users()
        {
            List<ApplicationUser> users = adminRepository.getAllUsers();

            return View("Users", users);
        }
        private string uploadFile(addBookVM newBook)
        {
            string fileName = null;
            if (newBook.book != null)
            {
                fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(newBook.book.FileName);

                string mypath = @"C:/Users/طيبة/Desktop/Maktabty/Maktabty/wwwroot/Files/";
                string uploadDir = Path.Combine(mypath);

                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    newBook.book.CopyTo(fileStream);
                }
            }
            return fileName;
        }
        private string uploadImageFile(addBookVM newBook)
        {
            string fileName = null;
            if (newBook.Image != null)
            {
                fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(newBook.Image.FileName);

                string mypath = @"C:/Users/طيبة/Desktop/Maktabty/Maktabty/wwwroot/Images/";
                string uploadDir = Path.Combine(mypath);

                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    newBook.Image.CopyTo(fileStream);
                }
            }
            return fileName;
        }

    }
}
