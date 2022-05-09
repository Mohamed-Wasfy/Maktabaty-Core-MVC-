using Maktabty.Models;
using System.Linq;

namespace Maktabty.Repositories
{
    public class ProfileRepo : IProfileRepo
    {
        DbEntities context;

        public ProfileRepo(DbEntities _context)
        {
            context = _context;
        }

        public ApplicationUser GetUserByID(string _id)
        {
            return context.Users.FirstOrDefault(u => Equals(u.Id, _id));
        }


    }
}
