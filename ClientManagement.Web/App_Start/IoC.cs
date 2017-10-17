using ClientManagement.Core.Data.Repositories;
using ClientManagement.Core.Interfaces;
using ClientManagement.Core.Providers;
using ClientManagement.Core.Services;
using Ninject;
using Ninject.Modules;
using System.Reflection;

namespace ClientManagement.Web.App_Start
{
    public class IoC : NinjectModule
    {
        private static StandardKernel kernal;
        public override void Load()
        {
            Bind<IProjectService>().To<ProjectService>();

            Bind<IEmployeeService>().To<EmployeeService>();
            Bind<IClientService>().To<ClientService>();
            Bind<IUserContext>().To<UserContext>();

            Bind<IEmployeeRepository>().To<EmployeeRepository>();
            Bind<IClientRepository>().To<ClientRepository>();
            Bind<IProjectRepository>().To<ProjectRepository>();
        }

        public static void RegisterNinjectInstance()
        {
            kernal = new StandardKernel();
            kernal.Load(Assembly.GetExecutingAssembly());
        }

        public static T GetInstance<T>()
        {
            return kernal.Get<T>();
        }
    }
}