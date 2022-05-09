using Maktabty.Models;
using System.Collections.Generic;

namespace Maktabty.Repositories
{
    public interface IFavListRepo
    {
        List<Book> GetUserFavsByID(string id);
        void FavingBook(Fav fav);
        Fav GetSingleFavById(string userId, int bookId);
        void RemoveFav(string userId, int bookId);
    }
}