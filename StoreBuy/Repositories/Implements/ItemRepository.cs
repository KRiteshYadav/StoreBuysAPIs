using NHibernate;
using NHibernate.Linq;
using StoreBuy.Domain;
using StoreBuy.Repositories.Interfaces;
using StoreBuy.UnitofWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreBuy.Repositories.Implements
{
    public class ItemRepository: GenericRepository<ItemCatalogue>,IItemRepository
    {        

       public ItemRepository(IUnitOfWork UnitOfWork) : base(UnitOfWork)
        {          
        }

        public IEnumerable<ItemCatalogue> GetItemsBySearchString(string SearchString)
        {            
            try
            {
                var SearchItems= Session.Query<ItemCatalogue>().Where(x => x.ItemName.Like(SearchString)).ToList();

                return  SearchItems;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        
        public IEnumerable<ItemCatalogue> GetItemsOfCategoryId(long CategoryId)
        {
            try
            {
                var CategoryItems = Session.Query<ItemCatalogue>().Where(x => x.ItemCategory.CategoryId == CategoryId).ToList();
                return CategoryItems;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}