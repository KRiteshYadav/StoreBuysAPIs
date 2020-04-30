using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreBuy.Domain
{
    public class StoreItemCatalogue
    {
        public virtual long Id { get; set; }
        public virtual long StoreItemId { get; set; }
        public virtual string StoreItemName { get; set; }
        public virtual float StoreItemPrice { get; set; }
        public virtual ItemCategory StoreItemCategory { get; set; }
        public virtual StoreInfo Store { get; set; }
    }
}