using Maktabty.Models;
using System.Collections.Generic;

namespace Maktabty.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> getAll();
    }
}