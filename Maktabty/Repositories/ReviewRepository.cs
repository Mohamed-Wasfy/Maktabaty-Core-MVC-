using Maktabty.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Maktabty.Repositories
{
	public class ReviewRepository : IReviewRepository
	{
		DbEntities Db;

		public ReviewRepository(DbEntities db)
		{
			this.Db = db;
		}
		//creat read update delete
		public int addReview(Review review)
		{
			Db.Reviews.Add(review);
			
			return Db.SaveChanges();
		}

		

		public List<Review> getAll()
		{
			return Db.Reviews.ToList();
		}
		public List<Review> getAllIncloud()
		{
			return Db.Reviews.Include(x => x.Book).Include(x => x.User).ToList();
		}
		public List<Review> getAllIncloudByUserId(string userId)
		{
			var r = Db.Reviews.Include(x => x.Book).Include(x => x.User).Where(x => x.UserId == userId).ToList();
			return r;
		}
		public Review getById(int bookId ,string userId )
		{
			var review = Db.Reviews.FirstOrDefault(x => x.BookId == bookId && x.UserId == userId);
			return review;
		}
		public Review getByIdIncloud(int bookId,string userId)
		{
			return Db.Reviews.Include(x => x.Book).Include(x => x.User).FirstOrDefault(x=>x.BookId==bookId&&x.UserId==userId);
		}
	
		public int updateReview(int bookId, string userId, Review review)
		{
			var _review = getById( bookId,  userId);
			_review.Comment = review.Comment;
			_review.Rate= review.Rate;
			_review.UserId= review.UserId;
			_review.BookId= review.BookId;
			return Db.SaveChanges();

		}

		public int deleteReview(int bookId, string userId)
		{
			var review = getById(bookId, userId);
			Db.Reviews.Remove(review);
			return Db.SaveChanges();
		}
		
		public List<Review> getByBookId(int bookId)
		{
			return Db.Reviews.Include(x => x.Book).Include(x => x.User).Where(x => x.BookId == bookId).ToList();
			//List<Review> reviews = Db.Reviews.Where(x => x.BookId == bookId).ToList();
			//foreach (Review review in reviews)
			//{
			//	review.User = Db.Users.FirstOrDefault(u => u.Id == review.UserId);
			//}
			//return reviews;
		}

		public int getTotalRateBybookId(int bookId)
		{
			var reviews = getByBookId(bookId);
			int countRates =Db.Reviews.Where(x=>x.BookId==bookId).Count();
			int totalRate=Db.Reviews.Where(x=>x.BookId==bookId).Select(x=>x.Rate).Sum();
			
			//foreach(Review review in reviews)
			//{
			//	totalRate += review.Rate;
			//	countRates++;
			//}

			if (totalRate!=0&&countRates!=0)
			{
				return totalRate/countRates;

			}
			return 0;
		}
	}
}
