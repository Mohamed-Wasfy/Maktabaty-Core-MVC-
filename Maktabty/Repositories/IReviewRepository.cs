using Maktabty.Models;
using System.Collections.Generic;

namespace Maktabty.Repositories
{
	public interface IReviewRepository
	{
		//creat read update delete
		public int addReview(Review review);
		public int updateReview(int bookId, string userId, Review review);

		public int deleteReview(int bookId, string userId);

		//get
		public List<Review> getAll();
		public List<Review> getAllIncloud();
		public Review getById(int bookId, string userId);
		public List<Review> getByBookId(int bookId);
		public Review getByIdIncloud(int bookId, string userId);
		public List<Review> getAllIncloudByUserId(string userId);
		public int getTotalRateBybookId(int bookId);



	}
}
