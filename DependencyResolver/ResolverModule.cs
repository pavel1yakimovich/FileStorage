using System.Data.Entity;
using BLL.Interface.Services;
using BLL.Services;
using DAL;
using DAL.Concrete;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using Ninject;
using Ninject.Web.Common;
using ORM;

namespace DependencyResolver
{
    public static class ResolverConfig
    {
        public static void ConfigurateResolverWeb(this IKernel kernel)
        {
            Configure(kernel);
        }

        private static void Configure(IKernel kernel)
        {
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            kernel.Bind<DbContext>().To<EntityModel>().InRequestScope();
            
            kernel.Bind<IRoleService>().To<RoleService>();
            kernel.Bind<IFileService>().To<FileService>();
            kernel.Bind<IUserService>().To<UserService>();

            kernel.Bind<IRepository<DalUser>>().To<UserRepository>();
            kernel.Bind<IRepository<DalFile>>().To<FileRepository>();
            kernel.Bind<IRoleRepository>().To<RoleRepository>();
            kernel.Bind<ILogger>().To<NLogAdaptor>();
        }
    }
}