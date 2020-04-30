using NHibernate;
using StoreBuy.Domain;
using StoreBuy.Models;
using StoreBuy.Repositories.Interfaces;
using StoreBuy.UnitofWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using StoreBuy.Utilities;

namespace StoreBuy.Repositories.Implements
{
    public class OrdersRepository : GenericRepository<Orders>, IOrdersRepository
    {

        public OrdersRepository(IUnitOfWork UnitOfWork) : base(UnitOfWork)
        { }

    
        public bool Notify(Orders Order,List<ItemCatalogueModel> Items)
        {
            try
            {
                
                string MailBody = "" + Order.OrderId + " " + Order.SlotDate + "" + Order.SlotTime + "\n";
                foreach (ItemCatalogueModel Item in Items)
                {
                    MailBody += " " + Item.ItemName + " " + Item.Quantity + "\n";
                }
                Utility.SendEmail(Resources.ToMailAddress, MailBody, Resources.OrderSubject);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public int GetSlotCount(Orders order)
        {
            try
            {
                var SlotsFillCount = Session.Query<Orders>().Where(x => x.Store == order.Store && x.SlotTime == order.SlotTime && x.SlotDate == order.SlotDate).Count();
                return SlotsFillCount;
            }
            catch (Exception exception)
            {
                throw exception;
            }

        }

    }
}