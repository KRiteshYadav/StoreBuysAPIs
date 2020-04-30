using NHibernate;
using StoreBuy.Domain;
using StoreBuy.Repositories.Interfaces;
using StoreBuy.UnitofWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreBuy.Repositories.Implements
{
    public class StoreFilledSlotRepository : GenericRepository<StoreFilledSlot>,IStoreFilledSlotRepository
    {
        public StoreFilledSlotRepository(IUnitOfWork UnitOfWork) : base(UnitOfWork)
        {
        }

        public bool CheckIfStoreSlotFilled(StoreInfo Store, string SlotTime, string SlotDate)
        {
            try
            {
                var IsAvailable = Session.Query<StoreFilledSlot>().Where(x => x.Store ==Store && x.SlotDate==SlotDate && x.SlotTime==SlotTime).SingleOrDefault();
                
                if (IsAvailable==null)
                    return false;
                else
                    return true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

    }
}