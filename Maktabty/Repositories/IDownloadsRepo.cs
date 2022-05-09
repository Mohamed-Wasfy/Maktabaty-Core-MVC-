using Maktabty.Models;
using System.Collections.Generic;

namespace Maktabty.Repositories
{
    public interface IDownloadsRepo
    {
        List<Book> GetUserDownloadsByID(string id);
        void DownloadBook(Downloads download);
        Downloads GetSinglEDownloadById(string userId, int bookId);
        void RemoveDownload(string userId, int bookId);
    }
}