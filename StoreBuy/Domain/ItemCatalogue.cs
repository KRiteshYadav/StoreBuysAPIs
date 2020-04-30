using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreBuy.Domain
{
    public class ItemCatalogue
    {
        public virtual long ItemId { get; set; }
        public virtual string ItemName { get; set; }
        public virtual float EstimatedPrice { get; set; }
        public virtual string ItemDescription { get; set; }
        public virtual ItemCategory ItemCategory { get; set; }
    }
}