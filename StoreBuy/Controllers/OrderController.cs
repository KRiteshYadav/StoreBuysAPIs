using StoreBuy.Domain;
using StoreBuy.Models;
using StoreBuy.Repositories;
using StoreBuy.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;

namespace StoreBuy.Controllers
{
    [RoutePrefix("api/Order")]
    public class OrderController : ApiController
    {

        IOrdersRepository OrdersRepository = null;
        IStoreSearchRepository StoreRepository = null;
        IUserRepository UserRepository =null;
        IOrderItemRepository OrderItemRepository =null;
        IItemRepository ItemRepository =null;
        ICartRepository CartRepository = null;
        IStoreFilledSlotRepository StoreFilledRepository=null;

        public OrderController(IOrdersRepository OrdersRepository,
            IStoreSearchRepository StoreRepository,
            IUserRepository UserRepository,
            IOrderItemRepository OrderItemRepository,
            IItemRepository ItemRepository,
            IStoreFilledSlotRepository StoreFilledRepository,
            ICartRepository CartRepository)
        {
            this.CartRepository = CartRepository;
            this.StoreFilledRepository = StoreFilledRepository;
            this.ItemRepository = ItemRepository;
            this.OrderItemRepository = OrderItemRepository;
            this.UserRepository = UserRepository;
            this.OrdersRepository = OrdersRepository;
            this.StoreRepository = StoreRepository;
        }

        [HttpPost]
        [Route("InsertOrder")]
        public void InsertOrder(string SlotTime,string SlotDate, long UserId,long StoreId)
        {
            try
            {                
                Users User = UserRepository.GetById(UserId);
                StoreInfo StoreInfo = StoreRepository.GetById(StoreId);
                Orders Order = new Orders();
                Order.SlotDate = SlotDate;
                Order.SlotTime = SlotTime;
                Order.User = User;
                Order.Store = StoreInfo;
                FillSlot(Order, SlotDate,SlotTime,StoreInfo);
                OrdersRepository.InsertOrUpdate(Order);
                List<ItemCatalogueModel> ItemList = StoreRepository.ItemsAvailableInStore(UserId, StoreInfo);

                foreach (ItemCatalogueModel item in ItemList)
                {
                    var Item = ItemRepository.GetById(item.ItemId);
                    OrderItem OrderItem = new OrderItem();
                    OrderItem.Order = Order;
                    OrderItem.Item = Item;
                    OrderItem.Quantity = item.Quantity;
                    OrderItemRepository.InsertOrUpdate(OrderItem);
                }
                 OrdersRepository.Notify(Order,ItemList);
                 DeleteCartItems(ItemList, User);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }


        void DeleteCartItems(List<ItemCatalogueModel> ItemList, Users User)
        {
            foreach (ItemCatalogueModel item in ItemList)
            {
                var DeleteItem = ItemRepository.GetById(item.ItemId);
                CartRepository.DeleteCartItemOfUser(User, DeleteItem);
            }
        }

        void FillSlot(Orders Order,string SlotDate,string SlotTime,StoreInfo Store)
        {
            int SlotCount = OrdersRepository.GetSlotCount(Order);
            if (SlotCount == Int32.Parse(Resources.LimitPerSlot))
            {
                StoreFilledSlot StoreFilledSlot = new StoreFilledSlot();
                StoreFilledSlot.SlotDate = SlotDate;
                StoreFilledSlot.SlotTime = SlotTime;
                StoreFilledSlot.Store = Store;
                StoreFilledRepository.InsertOrUpdate(StoreFilledSlot);
            }
        }
    }
}
