using Maktabty.Models;
using Maktabty.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Maktabty.Controllers
{
	public class ReviewsController : Controller
	{
		private readonly IReviewRepository reviewRepository; 
		private readonly UserManager<ApplicationUser> userManager;
		private readonly IBookRepository bookRepository;

		public ReviewsController(IReviewRepository _reviewRepository,
								 UserManager<ApplicationUser> userManager,IBookRepository bookRepository)
		{
			reviewRepository = _reviewRepository;
			this.userManager = userManager;
			this.bookRepository = bookRepository;
		}
		// GET: ReviewsController
		//get all Reviews of one book
		public ActionResult Index(int id)
		{
			//var userId = userManager.GetUserId(User);
			ViewBag.Id=id;
			var res = reviewRepository.getByBookId(id);
			var book =  bookRepository.getById(id);
			ViewBag.BookName = book.Name;
			return View(res);
		}

		// GET: ReviewsController/Details/5
		public ActionResult Details(int bookId)
		{
			var res = reviewRepository.getById(bookId,userManager.GetUserId(User));
			//return View(res);
			if (res != null)
			{
				return Json(res);
			}
			return null;
		}

		// GET: ReviewsController/Create
		public ActionResult Create(int? id=null)
		{
			
			
			if (id==null)
			{
				return RedirectToAction("Index");
			}
			ViewBag.userId = userManager.GetUserId(User);
			ViewData["bookId"] = id;
			return View();
		}

		// POST: ReviewsController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(string id, Review review)
		{
			try
			{
				review.UserId = userManager.GetUserId(User);

				var x = reviewRepository.addReview(review);
				return NoContent();
			}
			catch(Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		// GET: ReviewsController/Edit/5
		public ActionResult Edit(int bookId,string userId)
		{
			//var _userId = userManager.GetUserId(User);
			var res = reviewRepository.getById(bookId, userId);
			return View(res);
		}

		// POST: ReviewsController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public RedirectResult Edit(Review review)
		{

			//try
			//{
				//if (ModelState.IsValid)
				//{
				var x = userManager.GetUserId(User);
				review.UserId = x;

				reviewRepository.updateReview(review.BookId, x, review);


				return Redirect($"/book/Details/{review.BookId}");
				//}
			
			//}
			//catch(Exception ex)
			//{
			//	return BadRequest(ex.Message);
			//}
		}

		// GET: ReviewsController/Delete/5
		//public ActionResult Delete(int id)
		//{
		//	return View();
		//}

		// POST: ReviewsController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
