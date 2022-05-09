
using Maktabty.Models;
using Maktabty.Repositories;
using Maktabty.viewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Maktabty.Controllers
{
	public class BookController : Controller
	{
		IBookRepository bookRepository;
		ICategoryRepository CategoryRepository;
		IAuthorRepository AuthorRepository;
		private readonly IReviewRepository reviewRepository;
		private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager;

		public BookController(IBookRepository bookRepository, ICategoryRepository CategoryRepository
			, IAuthorRepository AuthorRepository
			, IReviewRepository reviewRepository, UserManager<ApplicationUser> userManager)
		{
			this.bookRepository = bookRepository;
			this.CategoryRepository = CategoryRepository;
			this.AuthorRepository = AuthorRepository;
			this.reviewRepository = reviewRepository;
			this.userManager = userManager;
		}
		public IActionResult Index()
		{
			BookVM model = new BookVM
			{
				Book = bookRepository.getAllWithCategoryAndAuthors(),
				Author = AuthorRepository.getAll(),
				Category = CategoryRepository.getAll(),
			};
			foreach (var book in model.Book)
			{
				book.TotalRate = reviewRepository.getTotalRateBybookId(book.Id);

			}
			return View(model);
		}
		public IActionResult GetBooksByCategory(int categoryId)
		{
			List<Book> bookList = bookRepository.getByCategory(categoryId);
			return PartialView("_BookByCategoryListPartial", bookList);
		}
		public IActionResult GetBooksByAuthors(int authorId)
		{

			BookVM model = new BookVM
			{
				Book = bookRepository.getByAuthor(authorId)
			};
			return PartialView("_BookByAuthorListPartial", model);
		}
		public IActionResult GetBookByName(string bookName)
		{

			List<Book> book = bookRepository.getByName(bookName);

			return PartialView("_BookByNameListPartial", book);
		}
		public IActionResult Details(int id)
		{




			var res = reviewRepository.getById(id, userManager.GetUserId(User));

			if (res == null)
			{
				ViewBag.HasReview = new Review();

			}
			else
			{

				ViewBag.HasReview = res;
			}




			ViewBag.userId = userManager.GetUserId(User);

			Book book = bookRepository.getById(id);
			book.TotalRate = reviewRepository.getTotalRateBybookId(book.Id);
			return View(book);

		}

		public IActionResult About()
        {
			return View();
        }
		public IActionResult Services()
		{
			return View();
		}
		public IActionResult Team()
		{
			return View();
		}
		public IActionResult Contact()
		{
			return View();
		}
	}
}

