using Maktabty.Models;
using System.Collections.Generic;

namespace Maktabty.viewModels
{
    public class BookVM
    {
        public List<Book> Book { get; set; }
        public List<Category> Category { get; set; }
        public List<Author> Author { get; set; }
        public List<AuthorBook> AuthorBooks { get; set; }

    }
}
