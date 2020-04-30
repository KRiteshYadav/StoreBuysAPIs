using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreBuy.Domain
{
    public class StoreFilledSlot
    {
        public virtual long SlotId { get; set; }



        public virtual string SlotDate { get; set; }
        public virtual StoreInfo Store { get; set; }



        public virtual string SlotTime { get; set; }
    }
}