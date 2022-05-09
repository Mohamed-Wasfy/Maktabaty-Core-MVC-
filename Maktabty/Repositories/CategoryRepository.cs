using Maktabty.Models;
using System.Collections.Generic;
using System.Linq;

namespace Maktabty.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        DbEntities db;

        public CategoryRepository(DbEntities db)
        {
            this.db = db;
        }
        public List<Category> getAll()
        {
            List<Category> categories = db.Categories.ToList();
            return categories;
        }

    }
}
