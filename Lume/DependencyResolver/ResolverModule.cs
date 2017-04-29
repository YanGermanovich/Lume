using System.Data.Entity;
using Ninject;
using Ninject.Web.Common;
using NLog;
using ORM;
using DAL.Interface;
using DAL.Concrete;

namespace CustomDependencyResolver
{
    public static class ResolverConfig
    {
        public static void ConfigurateResolverWeb(this IKernel kernel)
        {
            Configure(kernel, true);
        }
        public static void ConfigurateResolverConsole(this IKernel kernel)
        {
            Configure(kernel, false);
        }
        private static void Configure(IKernel kernel, bool isWeb)
        {
            if (isWeb)
            {
                kernel.Bind<DbContext>().To<lume_LumeEntities>().InRequestScope();
            }
            else
            {
                kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();
                kernel.Bind<DbContext>().To<lume_LumeEntities>().InSingletonScope();
            }

            kernel.Bind<ILogger>().ToMethod(f => LogManager.GetCurrentClassLogger()).InSingletonScope();


        }
    }
}

