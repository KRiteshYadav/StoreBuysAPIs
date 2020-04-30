using Newtonsoft.Json;
using StoreBuy.Domain;
using StoreBuy.Models;
using StoreBuy.Repositories;
using StoreBuy.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StoreBuy.Controllers
{
    [RoutePrefix("api/StoreSearch")]
    public class StoreSearchController : ApiController
    {

        ICartRepository CartRepository = null;
        IStoreSearchRepository StoreRepository = null;

       public  StoreSearchController(ICartRepository CartRepository,
           IStoreSearchRepository StoreRepository)
        {
            this.CartRepository = CartRepository;
            this.StoreRepository = StoreRepository;            
        }

        [HttpGet]
        [Route("FindStores")]
        public List<StoreModel> FindStores(long UserId, long Latitude,long Longitude)
        {
            List<StoreInfo> AllListOfStores = StoreRepository.GetAll().ToList();
            List<StoreInfo> ListOfStores =  StoreRepository.FindNearestStores(Latitude,Longitude,AllListOfStores);
            Dictionary<StoreInfo, List<ItemCatalogueModel>> StoresToAvailableItemsMap = new Dictionary<StoreInfo, List<ItemCatalogueModel>>();

            foreach (StoreInfo Store in ListOfStores)
            {
                List<ItemCatalogueModel> ItemsList = StoreRepository.ItemsAvailableInStore(UserId, Store);
                StoresToAvailableItemsMap.Add(Store, ItemsList);
            }

            List<StoreModel> Stores = new List<StoreModel>();

            foreach (KeyValuePair<StoreInfo, List<ItemCatalogueModel>> Item in StoresToAvailableItemsMap.OrderBy(key => -key.Value.Count))
            {
                var Storemodel = new StoreModel();
                Storemodel.StoreId = Item.Key.StoreId;
                Storemodel.StoreName = Item.Key.StoreName;
                Storemodel.Phone = Item.Key.Phone;
                Storemodel.ListItems = Item.Value;
                Stores.Add(Storemodel);
            }
            return Stores;
        }

    }
}
