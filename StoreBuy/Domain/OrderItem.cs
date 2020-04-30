using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreBuy.Domain
{
    public class OrderItem
    {
        public virtual long OrderItemId { get; set; }
        public virtual ItemCatalogue Item { get; set; }
        public virtual Orders Order { get; set; }
        public virtual long Quantity { get; set; }
    }
}