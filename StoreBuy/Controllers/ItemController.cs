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
    [RoutePrefix("api/item")]
    public class ItemController : ApiController
    {
        IItemRepository ItemRepository = null;
        IStoreSearchRepository StoreRepository = null;

        public ItemController(IItemRepository ItemRespository,
            IStoreSearchRepository StoreRepository)
        {
            this.ItemRepository = ItemRespository;
            this.StoreRepository = StoreRepository;
 
        }


        [HttpGet]
        [Route("SearchRelatedItems")]
        public IEnumerable<ItemCatalogueModel> GetSearchRelatedItems(string SearchString)
        {
            {
                List<ItemCatalogueModel> SearchedItems = new List<ItemCatalogueModel>();

                try
                {
                    var Items = ItemRepository.GetItemsBySearchString(SearchString);

                    foreach (ItemCatalogue Item in Items)
                    {
                        ItemCatalogueModel SearchItems = new ItemCatalogueModel();
                        SearchItems.ItemId = Item.ItemId;
                        SearchItems.ItemName = Item.ItemName;
                        SearchItems.ItemDescription = Item.ItemDescription;
                        SearchItems.Price = Item.EstimatedPrice;
                        SearchItems.CategoryName = Item.ItemCategory.CategoryName;
                        SearchedItems.Add(SearchItems);
                    }
                    return SearchedItems;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        [HttpGet]
        [Route("CategoryRelatedItems")]
        public IEnumerable<ItemCatalogueModel> GetCategoryRelatedItems(long CategoryId)
        {
            List<ItemCatalogueModel> SearchedItems = new List<ItemCatalogueModel>();
            try
            {
                var Items = ItemRepository.GetItemsOfCategoryId(CategoryId);
                foreach (ItemCatalogue Item in Items)
                {
                    ItemCatalogueModel ItemCatalogueModel = new ItemCatalogueModel();
                    ItemCatalogueModel.ItemId = Item.ItemId;
                    ItemCatalogueModel.ItemName = Item.ItemName;
                    ItemCatalogueModel.ItemDescription = Item.ItemDescription;
                    ItemCatalogueModel.Price = Item.EstimatedPrice;
                    ItemCatalogueModel.CategoryName = Item.ItemCategory.CategoryName;
                    SearchedItems.Add(ItemCatalogueModel);
                    if (SearchedItems.Count == Int32.Parse(Resources.ItemsInCategoryLimit))
                    {
                        break;
                    }
                }
                return SearchedItems;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("RelatedStores")]
        public Dictionary<string, float> GetRelatedStores(long ItemId)
        {
            List<StoreInfo> ListOfStores = StoreRepository.GetAll().ToList();
            Dictionary<string,float> StoreToPriceMap = new Dictionary<string, float>();

            foreach (StoreInfo Store in ListOfStores)
            {
                var ItemPrice = StoreRepository.GetPriceIfItemAvailableInStore(ItemId, Store);
                if(ItemPrice!=-1)
                    StoreToPriceMap.Add(Store.StoreName, ItemPrice);           
            }
            return StoreToPriceMap;
        }
    }
}
