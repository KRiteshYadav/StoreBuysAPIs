using NHibernate;
using StoreBuy.Domain;
using StoreBuy.Repositories.Interfaces;
using StoreBuy.UnitofWork.Interfaces;
using StoreBuy.UnitofWork.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreBuy.Repositories.Implements
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        readonly UnitOfWork UnitOfWork;

        public GenericRepository(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = (UnitOfWork)unitOfWork;
        }

        protected ISession Session { get { return UnitOfWork.Session; } }


        public void Delete(object id)
        {
            try
            {
                var entitytoDelete = Session.Get<T>(id);
                Session.Delete(entitytoDelete);
                
            }
            catch
            {
                return;
            }
        }

        public IEnumerable<T> GetAll()
        {
             var list = Session.Query<T>().ToList();
             return list;

        }

        public T GetById(object id)
        {
            return Session.Get<T>(id);
        }

        public void InsertOrUpdate(T obj)
        {
            try
            {
                Session.SaveOrUpdate(obj);

            }
            catch (Exception exe)
            {
                throw exe;
            }
        }


    }
}