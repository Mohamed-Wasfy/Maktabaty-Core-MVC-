using System.Collections.Generic;

namespace Maktabty.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public virtual ICollection<Book> books { get; set; }
    }
}
