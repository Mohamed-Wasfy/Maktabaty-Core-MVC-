using Maktabty.Models;
using System.Collections.Generic;

namespace Maktabty.Repositories
{
    public interface IBookRepository
    {
        List<Book> getAll();
        List<Book> getAllWithCategoryAndAuthors();
        List<Book> getByAuthor(int authorId);
        List<Book> getByCategory(int categoryId);
        Book getById(int id);
        List<Book> getByName(string name);
    }
}