using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Maktabty.Models
{
    public class Downloads
    {
       
        [ForeignKey("User")]
        public string UserId { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }
        public DateTime? DateOfDownLoad { get; set; }
        public ApplicationUser User { get; set; }
        public Book Book { get; set; }
    }
}
