using StoreBuy.Domain;
using StoreBuy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreBuy.Repositories.Interfaces
{
    public interface IStoreSearchRepository : IGenericRepository<StoreInfo>
    {
        List<ItemCatalogueModel> ItemsAvailableInStore(long UserId, StoreInfo Store);
        float GetPriceIfItemAvailableInStore(long ItemId, StoreInfo Store);
        List<StoreInfo> FindNearestStores(long Latitude, long Longitude, List<StoreInfo> Stores);
    }
}