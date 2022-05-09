using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Maktabty.Models
{
    public class AuthorBook
    {
        [ForeignKey("Author")]
        public int AuthorId { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }
        public virtual Author Author { get; set; }
        public virtual Book Book { get; set; }
    }
}
