using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreBuy.Domain
{
    public class Location
    {
        public virtual long LocationId { get; set; }
        public virtual long Latitude { get; set; }
        public virtual long Longitude { get; set; }

    }
}