using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Maktabty.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
    }
    public class DbEntities : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public DbEntities() : base()
        {

        }
        public DbEntities(DbContextOptions options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=MaktabtyDb;Integrated Security=True;MultipleActiveResultSets = True");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            builder.Entity<Downloads>().HasKey(a => new { a.BookId, a.UserId });
            builder.Entity<Fav>().HasKey(a => new { a.BookId, a.UserId });
            builder.Entity<Review>().HasKey(a => new { a.BookId, a.UserId });
            builder.Entity<AuthorBook>().HasKey(a => new { a.BookId, a.AuthorId });
            base.OnModelCreating(builder);
        }
        public DbSet<Author> Authors { get; set; }
    
        public DbSet<Book> Books { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Downloads> Downloads { get; set; }
        public DbSet<Fav> Favs { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<AuthorBook> AuthorBooks { get; set; }
    }
}
