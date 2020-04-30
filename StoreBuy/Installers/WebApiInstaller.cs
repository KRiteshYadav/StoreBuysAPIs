using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using StoreBuy.Repositories;
using StoreBuy.Repositories.Interfaces;
using StoreBuy.UnitofWork.Implements;
using StoreBuy.UnitofWork.Interfaces;

namespace StoreBuy.Installers
{
    public class WebApiInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes
                    .FromThisAssembly()
                    .BasedOn<ApiController>()
                    .LifestyleScoped()
                );
        }
    }
    public class RepositoryInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
            Classes.FromThisAssembly()
            .BasedOn(typeof(IGenericRepository<>))
            .WithService.AllInterfaces()
            .LifestyleTransient()
            );

        }
    }

    public class UnitOfWorkInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component
                    .For<IUnitOfWork>()
                    .ImplementedBy<UnitOfWork>()
                    .LifestyleSingleton());
        }
    }
}