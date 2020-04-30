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
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
       

        public CartRepository(IUnitOfWork UnitOfWork) : base(UnitOfWork)
        {            
        }

        public Cart RetrieveExistingCartItem(Users User, ItemCatalogue ItemCatalogue)
        {
            try
            {
                var cart = Session.Query<Cart>().Where(x => x.User == User && x.ItemCatalogue == ItemCatalogue).SingleOrDefault();
                return cart;

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void DeleteCartItemOfUser(Users User, ItemCatalogue ItemCatalogue)
        {
            try
            {

                var cart = Session.Query<Cart>().Where(x => x.User == User && x.ItemCatalogue == ItemCatalogue).SingleOrDefault();
                Session.Delete(cart);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        
        public IEnumerable<Cart> RetrieveCartItemsOfUser(Users User)
        {
            try
            {
                var CartItems = Session.Query<Cart>().Where(x => x.User == User).ToList();
                return CartItems;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}