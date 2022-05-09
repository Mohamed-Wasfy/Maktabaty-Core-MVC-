using Maktabty.Models;
using System.Collections.Generic;

namespace Maktabty.Repositories
{
    public interface IAuthorRepository
    {
        List<Author> getAll();
    }
}