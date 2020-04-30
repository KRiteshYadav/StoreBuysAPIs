using StoreBuy.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreBuy.Repositories.Interfaces
{
    public interface IStoreFilledSlotRepository : IGenericRepository<StoreFilledSlot>
    {
        bool CheckIfStoreSlotFilled(StoreInfo Store,string SlotTime, string SlotDate);
    }
}