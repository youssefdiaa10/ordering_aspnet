using Ordering.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Core.Repositories.Interfaces
{
    public interface IBasketRepository
    {

        Task<CustomerBasket?> GetBasketAsync(string basketId);
        Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket); // update & create
        Task<bool> DeleteBasketAsync(string basketId); 

    }
}
