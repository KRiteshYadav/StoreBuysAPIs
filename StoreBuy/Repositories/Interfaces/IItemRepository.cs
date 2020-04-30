using StoreBuy.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreBuy.Repositories.Interfaces
{
    public interface IItemRepository: IGenericRepository<ItemCatalogue>
    {
        IEnumerable<ItemCatalogue> GetItemsBySearchString(string SearchString);
        IEnumerable<ItemCatalogue> GetItemsOfCategoryId(long CategoryId);



    }
}