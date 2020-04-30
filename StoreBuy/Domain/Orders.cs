using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreBuy.Domain
{
    public class Orders
    {
        public virtual long OrderId { get; set; }
        public virtual string SlotTime { set; get; }
        public virtual string SlotDate
        { set; get; }
        public virtual Users User { set; get; }
        public virtual StoreInfo Store { set; get; }
    }
}