using System.Data.Entity;
using Ninject;
using Ninject.Web.Common;
using NLog;
using ORM;
using DAL.Interface;
using DAL.Concrete;
using BLL.Entities;
using BLL.Services_Interface;
using DAL.DTO;
using Bll.Services;

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
                kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
                kernel.Bind<DbContext>().To<lume_LumeEntities>().InRequestScope();
            }
            else
            {
                kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();
                kernel.Bind<DbContext>().To<lume_LumeEntities>().InSingletonScope();
            }

            kernel.Bind<ILogger>().ToMethod(f => LogManager.GetCurrentClassLogger()).InSingletonScope();

            kernel.Bind<IService<BllAvatar>>().To<AvatarService>();
            kernel.Bind<IRepository<DalAvatar>>().To<AvatarRepository>();

            kernel.Bind<IService<BllCity>>().To<CityService>();
            kernel.Bind<IRepository<DalCity>>().To<CityRepository>();

            kernel.Bind<IService<BllCountry>>().To<CountryService>();
            kernel.Bind<IRepository<DalCountry>>().To<CountryRepository>();

            kernel.Bind<IService<BllDataType>>().To<DataTypeService>();
            kernel.Bind<IRepository<DalDataType>>().To<DataTypeRepository>();

            kernel.Bind<IService<BllEvent>>().To<EventService>();
            kernel.Bind<IRepository<DalEvent>>().To<EventRepository>();

            kernel.Bind<IService<BllHistory>>().To<HistoryService>();
            kernel.Bind<IRepository<DalHistory>>().To<HistoryRepository>();

            kernel.Bind<IService<BllImageProperties>>().To<ImagePropertiesService>();
            kernel.Bind<IRepository<DalImageProperties>>().To<ImagePropertiesRepository>();

            kernel.Bind<IService<BllImage>>().To<ImageService>();
            kernel.Bind<IRepository<DalImage>>().To<ImageRepository>();

            kernel.Bind<IService<BllPrize>>().To<PrizeService>();
            kernel.Bind<IRepository<DalPrize>>().To<PrizeRepository>();

            kernel.Bind<IService<BllPrizeType>>().To<PrizeTypeService>();
            kernel.Bind<IRepository<DalPrizeType>>().To<PrizeTypeRepository>();

            kernel.Bind<IService<BllStockImage>>().To<StockImageService>();
            kernel.Bind<IRepository<DalStockImage>>().To<StockImageRepository>();

            kernel.Bind<IService<BllStockPrize>>().To<StockPrizeService>();
            kernel.Bind<IRepository<DalStockPrize>>().To<StockPrizeRepository>();

            kernel.Bind<IService<BllStockProgress>>().To<StockProgressService>();
            kernel.Bind<IRepository<DalStockProgress>>().To<StockProgressRepository>();

            kernel.Bind<IService<BllStock>>().To<StockService>();
            kernel.Bind<IRepository<DalStock>>().To<StockRepository>();

            kernel.Bind<IService<BllStockType>>().To<StockTypeService>();
            kernel.Bind<IRepository<DalStockType>>().To<StockTypeRepository>();

            kernel.Bind<IService<BllUser>>().To<UserService>();
            kernel.Bind<IRepository<DalUser>>().To<UserRepository>();

            kernel.Bind<IService<BllUserStock>>().To<UserStockService>();
            kernel.Bind<IRepository<DalUserStock>>().To<UserStockRepository>();
        }
    }
}

