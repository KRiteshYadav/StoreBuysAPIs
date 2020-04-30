using StoreBuy.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreBuy.Models
{
    public class StoreModel
    {
        public virtual long StoreId { get; set; }
        public virtual string StoreName { get; set; }
        public virtual string Phone { get; set; }
       // public virtual string StoreLocation { set; get; }
        public List<ItemCatalogueModel> ListItems { set; get; }
    }
}