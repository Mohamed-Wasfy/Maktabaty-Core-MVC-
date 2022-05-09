using Maktabty.Models;
using Maktabty.Repositories;
using Maktabty.viewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Maktabty.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        DbEntities db;
        public AdminRepository(DbEntities _db)
        {
            db = _db;
        }
        #region Book Operations
        public int deleteBook(int id)
        {
            Book book = db.Books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                db.Books.Remove(book);
                return db.SaveChanges();
            }

            return 0;
        }
        public List<Book> getAllBooks()
        {
            List<Book> books = db.Books.Include(b => b.Category).Include(b => b.authors).ToList();
            return books;
        }

        public List<Book> getBooksByCategoryId(int categId)
        {
            return db.Books.Where(b => b.CategoryId == categId).ToList();
        }

        public Book getBookById(int id)
        {
             return db.Books.Include(b => b.Category).Include(b => b.authors).SingleOrDefault(b => b.Id == id);
        }

        public int insertBook(Book book)
        {
            db.Books.Add(book);
            return db.SaveChanges();
        }

        public int updateBook(int id, addBookVM bookView)
        {
            var oldBooks = getBookById(id);
            string stringImageName = uploadImageFile(bookView);
            string stringFileName = uploadFile(bookView);
           
                oldBooks.Name = bookView.Name;
                oldBooks.PublishDate = bookView.PublishDate;
                oldBooks.Language = bookView.Language;
                oldBooks.Description = bookView.Description;
                oldBooks.Pages = bookView.Pages;
                oldBooks.CategoryId = bookView.CategoryId;
                if (bookView.Image != null)
                {
                    oldBooks.Image = stringImageName;
                }
                if (bookView.book != null)
                {
                    oldBooks.book = stringFileName;
                }
                return db.SaveChanges();
         
        }
        #endregion
        #region Author Operations
        public int deleteAuthor(int id)
        {
            Author author = db.Authors.FirstOrDefault(b => b.Id == id);
            if (author != null)
            {
                db.Authors.Remove(author);
                return db.SaveChanges();
            }

            return 0;
        }

        public List<Author> getAllAuthors()
        {
            return db.Authors.ToList();
        }

        public Author getAuthorById(int id)
        {
            return db.Authors.FirstOrDefault(a => a.Id == id);
        }

        public int insertAuthor(Author author)
        {
            db.Authors.Add(author);
            return db.SaveChanges();
        }

        public int updateAuthor(int id, Author author)
        {
            Author oldAuthor = db.Authors.FirstOrDefault(a => a.Id == id);
            if(oldAuthor != null)
            {
                oldAuthor.Name = author.Name;
                oldAuthor.Image = author.Image;
                return db.SaveChanges();
            }
            return 0;
        }
        #endregion
        #region User Operations
        public List<ApplicationUser> getAllUsers()
        {
            return db.Users.ToList();
        }
        public ApplicationUser getUserById(string id)
        {
            return db.Users.FirstOrDefault(u => u.Id == id);
        }
        #endregion
        #region Category Operations
        public List<Category> getAllCategories()
        {
            return db.Categories.ToList();
        }

        public Category getCategoryById(int id)
        {
            return db.Categories.FirstOrDefault(c=>c.Id == id);
        }

        public int insertCategory(Category category)
        {
            db.Categories.Add(category);
            return db.SaveChanges();
        }

        public int updateCategory(int id, Category category)
        {
            Category oldCat = db.Categories.FirstOrDefault(c=>c.Id == id);
            if(oldCat != null)
            {
                oldCat.Name = category.Name;
                return db.SaveChanges();
            }
            return 0;
        }

        public int RemoveCategory(int id)
        {
            Category category = db.Categories.FirstOrDefault(c => c.Id == id);
            db.Categories.Remove(category);
            return db.SaveChanges();
        }
        #endregion

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
