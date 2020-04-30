using StoreBuy.Domain;
using StoreBuy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreBuy.Repositories.Interfaces
{
    public interface IOrdersRepository : IGenericRepository<Orders>
    {
        bool Notify(Orders order, List<ItemCatalogueModel> Items);
        int GetSlotCount(Orders order);
    }
}