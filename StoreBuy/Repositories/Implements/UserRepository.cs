using StoreBuy.Domain;
using StoreBuy.Repositories.Interfaces;
using StoreBuy.UnitofWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreBuy.Repositories.Implements
{
    public class UserRepository : GenericRepository<Users>, IUserRepository
    {

        public UserRepository(IUnitOfWork UnitOfWork) : base(UnitOfWork)
        {
        }
    }
}