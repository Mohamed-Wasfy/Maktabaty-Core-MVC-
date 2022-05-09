using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Maktabty.viewModels
{
    public class RegisterVM
    {
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public IFormFile Image { get; set; }
        public string Gender { get; set; }
        public DateTime birthDate { get; set; }

        [Required]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [MaxLength(11)]
        public string Phone { get; set; }
    }
}
