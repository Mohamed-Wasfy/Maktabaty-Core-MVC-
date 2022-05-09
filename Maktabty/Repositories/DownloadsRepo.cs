using Maktabty.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Maktabty.Repositories
{
    public class DownloadsRepo : IDownloadsRepo
    {
        DbEntities context;

        public DownloadsRepo(DbEntities _context)
        {
            context = _context;
        }
        public List<Book> GetUserDownloadsByID(string id)
        {
       
            List<Book> books=context.Downloads.Where(d => Equals(d.UserId, id)).Select(b=>b.Book).ToList();
            return books;
        }

        public Downloads GetSinglEDownloadById( string userId, int bookId)
        {
            
            return context.Downloads.Include(b=>b.Book).SingleOrDefault(d => Equals(d.UserId, userId) && Equals(d.BookId, bookId));


        }

        public void RemoveDownload(string userId, int bookId)
        {
            Downloads download = GetSinglEDownloadById(userId, bookId);
            context.Downloads.Remove(download); 
            context.SaveChanges();

        }
     
        public void DownloadBook(Downloads download)
        {
            
            context.Downloads.Add(download);
            context.SaveChanges();
        }

    }
}
