using StoreBuy.Repositories;
using StoreBuy.Domain;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StoreBuy.Models;
using StoreBuy.Repositories.Interfaces;

namespace StoreBuy.Controllers
{
    [Route("api/cart")]
    public class CartController : ApiController
    {
        ICartRepository CartRepository = null;
        IUserRepository UserRepository = null;
        IItemRepository ItemRepository = null;
        
        public CartController(ICartRepository CartRepository,
            IUserRepository UserRepository,
            IItemRepository ItemRepository)
        {
            this.CartRepository = CartRepository;
            this.UserRepository = UserRepository;
            this.ItemRepository = ItemRepository;            
        }

        [HttpGet]
        [Route("CartItems")]
        public IEnumerable<CartItemModel> GetCartItems(long UserId)
        {
            List<CartItemModel> CartList = new List<CartItemModel>();
            Users User = UserRepository.GetById(UserId);
            var CartItems = CartRepository.RetrieveCartItemsOfUser(User);

            foreach (Cart Cart in CartItems)
            {
                CartItemModel CartItem = new CartItemModel();
                CartItem.CartId = Cart.CartId;
                CartItem.ItemId = Cart.ItemCatalogue.ItemId;
                CartItem.Quantity = Cart.Quantity;
                CartItem.UserId = Cart.User.UserId;
                CartList.Add(CartItem);

            }
            return CartList;
        }

        [HttpPost]
        [Route("AddToCart")]
        public void AddToCart(long UserId,long ItemId)
        {
            Users User = UserRepository.GetById(UserId);
            ItemCatalogue ItemCatalogue = ItemRepository.GetById(ItemId);
            Cart Cart =CartRepository.RetrieveExistingCartItem(User, ItemCatalogue);

            if(Cart==null)
            {
                Cart = new Cart();
                Cart.ItemCatalogue = ItemCatalogue;
                Cart.User = User;
                Cart.Quantity = 1;
            }
            else
            {
                Cart.Quantity += 1;

            }
            CartRepository.InsertOrUpdate(Cart);
        }

        
        [HttpPost]
        [Route("UpdateQuantity")]
        public void UpdateQuantity(long UserId, long ItemId, int Quantity)
        {
            Users User = UserRepository.GetById(UserId);
            ItemCatalogue ItemCatalogue = ItemRepository.GetById(ItemId);
            Cart Cart = CartRepository.RetrieveExistingCartItem(User, ItemCatalogue);
            Cart.Quantity = Quantity;
            CartRepository.InsertOrUpdate(Cart);
        }


        [HttpDelete]
        [Route("DeleteCartItem")]
        public void DeleteCartItem(long UserId, long ItemId)
        {
            Users User = UserRepository.GetById(UserId);
            ItemCatalogue ItemCatalogue = ItemRepository.GetById(ItemId);
            CartRepository.DeleteCartItemOfUser(User,ItemCatalogue);
        }      
      
    }
}