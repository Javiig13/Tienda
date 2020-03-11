using System.Threading.Tasks;
using Tienda.DTOs;
using Tienda.Models;

namespace Tienda.Services
{
    public interface IProductOrdersService
    {
        Task CreateAsync(OrderDTO order);

        void UpdateStock(ProductOrder order);
    }
}
