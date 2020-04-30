using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreBuy.UnitofWork.Interfaces
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}