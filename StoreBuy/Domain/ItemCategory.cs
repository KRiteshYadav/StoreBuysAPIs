using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreBuy.Domain
{
    public class ItemCategory
    {
        public virtual long CategoryId { get; set; }
        public virtual string CategoryName { get; set; }

    }
}