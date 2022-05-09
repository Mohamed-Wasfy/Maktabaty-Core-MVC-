using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Maktabty.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string book { get; set; }
        public bool IsFeatured { get; set; }
        public DateTime PublishDate { get; set; }
        public string Language { get; set; }
        public string Description { get; set; }
        public int Pages { get; set; }
        public int NumOfDownloads { get; set; }
        public int? TotalRate { get; set; }

        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }
        
        public virtual ICollection<Author> authors { get; set; }
        public virtual ICollection<Review> reviews { get; set; }
    }
}
