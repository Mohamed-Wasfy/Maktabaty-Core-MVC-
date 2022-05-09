using Maktabty.Models;
using System.Collections.Generic;
using System.Linq;

namespace Maktabty.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        DbEntities db;

        public AuthorRepository(DbEntities db)
        {
            this.db = db;
        }
        public List<Author> getAll()
        {
            List<Author> categories = db.Authors.ToList();
            return categories;
        }
    }
}
