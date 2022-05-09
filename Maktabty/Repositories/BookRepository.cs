using Maktabty.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Maktabty.Repositories
{
    public class BookRepository : IBookRepository
    {
        DbEntities db;

        public BookRepository(DbEntities db)
        {
            this.db = db;
        }
        public List<Book> getAll()
        {
            List<Book> books = db.Books.ToList();
            return books;
        }

        public List<Book> getAllWithCategoryAndAuthors()
        {
        //    List<Author> authors = db.Authors.ToList();
        //    List<Book> books = new List<Book>();
        //    foreach(Author author in authors)
        //    {
        //        List<AuthorBook> AuthBook = db.AuthorBooks.Where(ab=>ab.AuthorId== author.Id).ToList();  
        //        foreach(AuthorBook AuthorBook in AuthBook)
        //        {
        //            var book = db.Books.FirstOrDefault(b => b.Id == AuthorBook.BookId);
        //            books.Add(book);
        //        }
                
        //    }
            List<Book> books = db.Books.Include(b=>b.Category).Include(b=>b.authors).ToList();

            return books;
        }

        public Book getById(int id)
        {
            return db.Books.Include(b=>b.Category).Include(b=>b.authors).SingleOrDefault(b => b.Id == id);
        }

        public List<Book> getByCategory(int categoryId)
        {
            List<Book> books;
            if (categoryId == 0)
            {
               books= getAllWithCategoryAndAuthors();
                return books;
            }
             books = db.Books.
                Include(b => b.Category).
                Include(b=>b.authors).
                Where(b => b.CategoryId==categoryId).
                ToList();
            return books;
        }

        public List<Book> getByAuthor(int authorId)
        {
            List<Book> authorBook;
            if (authorId == 0)
            {
                authorBook = getAllWithCategoryAndAuthors();
            }
            else
            {
                authorBook = db.AuthorBooks.
                    Include(b => b.Author).
                    Include(b => b.Book).
                    Include(b => b.Book.Category).
                    Include(b => b.Book.authors).
                    Where(b => b.AuthorId == authorId).Select(b => b.Book).ToList();
                return authorBook;
            }
            return authorBook;
        }

        public List<Book> getByName(string name)
        {
            List<Book> book=db.Books.
                Include(b=>b.Category)
                .Include(b=>b.authors).
                Where(b=>b.Name.ToLower().Contains(name.ToLower())).ToList();
           
            return book;
        }
    }
}
