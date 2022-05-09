using Maktabty.Models;

namespace Maktabty.Repositories
{
    public interface IProfileRepo
    {
        ApplicationUser GetUserByID(string _id);
    }
}