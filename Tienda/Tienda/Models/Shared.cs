using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using Tienda.Repositories;

namespace Tienda.Models
{
    public static class Shared
    {
        public static async Task<byte[]> GetBytes(IFormFile formFile)
        {
            using var memoryStream = new MemoryStream();
            await formFile.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }

        public static bool UserIsLogged(this ISession session)
        {
            if (session.GetString("UserSession") == null)
            {
                return false;
            }
            return true;
        }

        public static bool IsAdministrator(this ISession session, IRepository<Customer> repository)
        {
            if (!UserIsLogged(session))
            {
                return false;
            }

            int userId = int.Parse(session.GetString("UserSession"));
            
            if (repository.GetById(userId).UserRole == UserRole.Administrator)
            {
                return true;
            }

            return false;
        }
    }
}
