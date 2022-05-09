using Maktabty.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Maktabty.Repositories
{
    public class FavListRepo : IFavListRepo
    {
        DbEntities context;
        public FavListRepo(DbEntities _context)
        {
            context = _context;
        }

        public List<Book> GetUserFavsByID(string id)
        {
           
            List<Book> books = context.Favs.Where(d => Equals(d.UserId, id)).Select(b => b.Book).ToList();
            return books;
        }

        public Fav GetSingleFavById(string userId, int bookId)
        {

            return context.Favs.Include(b => b.Book).SingleOrDefault(d => Equals(d.UserId, userId) && Equals(d.BookId, bookId));


        }


        public void RemoveFav(string userId, int bookId)
        {
            Fav fav = GetSingleFavById(userId, bookId);
            context.Favs.Remove(fav);   
            context.SaveChanges();

        }
        public void FavingBook(Fav fav)
        {
            context.Favs.Add(fav);
            context.SaveChanges();
        }

    }
}
