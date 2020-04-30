using StoreBuy.Domain;
using StoreBuy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreBuy.Repositories.Interfaces
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        Cart RetrieveExistingCartItem(Users User, ItemCatalogue ItemCatalogue);
        void DeleteCartItemOfUser(Users User, ItemCatalogue ItemCatalogue);
        IEnumerable<Cart> RetrieveCartItemsOfUser(Users User);

    }
}