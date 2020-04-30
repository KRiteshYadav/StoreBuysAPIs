using NHibernate;
using StoreBuy.Domain;
using StoreBuy.Models;
using StoreBuy.Repositories.Interfaces;
using StoreBuy.UnitofWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreBuy.Repositories.Implements
{
    public class StoreSeachRepository : GenericRepository<StoreInfo>, IStoreSearchRepository
    {
        public StoreSeachRepository(IUnitOfWork UnitOfWork) : base(UnitOfWork)
        {
        }

        public List<StoreInfo> FindNearestStores(long Latitude, long Longitude, List<StoreInfo> Stores)
        {
            Dictionary<StoreInfo, double> StoresMaptoDistance = new Dictionary<StoreInfo, double>();
                       
            foreach (StoreInfo Store in Stores)
            {
                var Distance = Math.Sqrt(Math.Pow(Latitude - Store.StoreLocation.Latitude, 2) + Math.Pow(Longitude - Store.StoreLocation.Longitude, 2));
                StoresMaptoDistance.Add(Store, Distance);
            }
            List<StoreInfo> NearByStores = new List<StoreInfo>();
            foreach (KeyValuePair<StoreInfo, double> Store in StoresMaptoDistance.OrderBy(key => key.Value))
            {
                NearByStores.Add(Store.Key);
                if (NearByStores.Count == Int32.Parse(Resources.StoreLimit))
                    break;
            }
            return NearByStores;
        }

        public List<ItemCatalogueModel> ItemsAvailableInStore(long UserId, StoreInfo Store)
        {
            List<ItemCatalogueModel> ItemsAvailableInStore = new List<ItemCatalogueModel>();
            try
            {
                List<Cart> CartItems = Session.Query<Cart>().Where(x => x.User.UserId == UserId).ToList();
                foreach (Cart CartItem in CartItems)
                {
                    var Item = Session.Get<ItemCatalogue>(CartItem.ItemCatalogue.ItemId);
                    var StoreItemPrice = GetPriceIfItemAvailableInStore(Item.ItemId,Store);

                    if (StoreItemPrice != Int32.Parse(Resources.NotAPrice))
                    {
                        ItemCatalogueModel ItemModel = new ItemCatalogueModel();
                        ItemModel.ItemId = Item.ItemId;
                        ItemModel.ItemDescription = Item.ItemDescription;
                        ItemModel.ItemName = Item.ItemName;
                        ItemModel.Price = StoreItemPrice;
                        ItemModel.Quantity = CartItem.Quantity;
                        ItemModel.CategoryName = Item.ItemCategory.CategoryName;
                        ItemsAvailableInStore.Add(ItemModel);
                    }
                }
                return ItemsAvailableInStore;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }


        public float GetPriceIfItemAvailableInStore(long ItemId, StoreInfo Store)
        {
            var StoreItem=Session.Query<StoreItemCatalogue>().Where(x => x.StoreItemId == ItemId && x.Store == Store).SingleOrDefault();
            
            if (StoreItem != null)
            {
                return StoreItem.StoreItemPrice;
            }
            else
            {
                return -1;
            }
        }
    }
}