using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreBuy.Models
{
    public class CartItemModel
    {
        public virtual long CartId { get; set; }
        public virtual long ItemId { get; set; }
        public virtual long UserId { get; set; }
        public virtual long Quantity { get; set; }
    }
}