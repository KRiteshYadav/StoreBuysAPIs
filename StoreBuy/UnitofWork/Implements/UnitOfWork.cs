using NHibernate;
using NHibernate.Cfg;
using StoreBuy.UnitofWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreBuy.UnitofWork.Implements
{
    public class UnitOfWork : IUnitOfWork
    {
        private static readonly ISessionFactory _sessionFactory;
        private ITransaction _transaction;

        public ISession Session { get; private set; }

        static UnitOfWork()
        {
            var configuration = new Configuration();
            var configurationPath =
                HttpContext.Current.Server.MapPath(@"~\Models\hibernate.cfg.xml");
            configuration.Configure(configurationPath);
            var CartConfigurationFile =HttpContext.Current.Server.MapPath(@"~\Mappings\Cart.hbm.xml");
            configuration.AddFile(CartConfigurationFile);
            var ItemCatalogueConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Mappings\ItemCatalogue.hbm.xml");
            configuration.AddFile(ItemCatalogueConfigurationFile);
            var ItemCategoryConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Mappings\ItemCategory.hbm.xml");
            configuration.AddFile(ItemCategoryConfigurationFile);
            var LocationConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Mappings\Location.hbm.xml");
            configuration.AddFile(LocationConfigurationFile);
            var OrderItemConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Mappings\OrderItem.hbm.xml");
            configuration.AddFile(OrderItemConfigurationFile);
            var OrdersConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Mappings\Orders.hbm.xml");
            configuration.AddFile(OrdersConfigurationFile);
            var OTPValidatorConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Mappings\OTPValidator.hbm.xml");
            configuration.AddFile(OTPValidatorConfigurationFile);
            var StoreFilledSlotConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Mappings\StoreFilledSlot.hbm.xml");
            configuration.AddFile(StoreFilledSlotConfigurationFile);
            var StoreInfoConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Mappings\StoreInfo.hbm.xml");
            configuration.AddFile(StoreInfoConfigurationFile);
            var StoreItemCatalogueConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Mappings\StoreItemCatalogue.hbm.xml");
            configuration.AddFile(StoreItemCatalogueConfigurationFile);
            var UsersConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Mappings\Users.hbm.xml");
            configuration.AddFile(UsersConfigurationFile);
            _sessionFactory = configuration.BuildSessionFactory();
        }

        public UnitOfWork()
        {
            Session = _sessionFactory.OpenSession();

        }

        public void BeginTransaction()
        {
            if(Session==null)
            {
                Session = _sessionFactory.OpenSession();

            }
            _transaction = Session.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                if (_transaction != null && _transaction.IsActive)
                    _transaction.Commit();
            }
            catch
            {
                if (_transaction != null && _transaction.IsActive)
                    _transaction.Rollback();

                throw;
            }
            finally
            {
                Session.Dispose();
                Session = null;
            }
        }

        public void Rollback()
        {
            try
            {
                if (_transaction != null && _transaction.IsActive)
                    _transaction.Rollback();
            }
            finally
            {
                Session.Dispose();
                Session = null;
            }
        }
    }
}