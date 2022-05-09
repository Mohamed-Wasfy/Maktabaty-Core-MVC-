using Maktabty.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Maktabty.viewModels
{
    public class addBookVM
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public IFormFile book { get; set; }
        public bool IsFeatured { get; set; }
        public DateTime PublishDate { get; set; }
        public string Language { get; set; }
        public string Description { get; set; }
        public int Pages { get; set; }
        public int? NumOfDownloads { get; set; }

        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<Author> authors { get; set; }
    }
}
