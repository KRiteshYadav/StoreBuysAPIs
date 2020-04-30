using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreBuy.Domain
{
    public class Cart
    {
        public virtual long CartId { get; set; }
        public virtual ItemCatalogue ItemCatalogue { get; set; }
        public virtual Users User { get; set; }
        public virtual long Quantity { get; set; }

    }
}