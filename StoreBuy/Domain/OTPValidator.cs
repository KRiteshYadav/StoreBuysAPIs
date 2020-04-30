using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreBuy.Domain
{
    public class OTPValidator
    {
        public virtual long UserId { get; set; }
        public virtual long CurrentOtp { get; set; }
        public virtual DateTime DateTime { get; set; }
    }

}