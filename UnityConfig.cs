using System.Web.Http;
using ExternalAPI.DataAccess;
using ExternalAPI.DataAccess.Interfaces;
using Framework.Logging;
using Framework.Logging.Interfaces;
using Framework.OnlineAccountManagement;
using Framework.OnlineAccountManagement.Interfaces;
using Framework.WebApi.Utility.Dependencies;
using Microsoft.Practices.Unity;

namespace ExternalAPI
{

    public static class UnityConfig
    {
        private static readonly UnityContainer IocContainer;

        private const string UserManagementConnectionString = "AuthContext";

        static UnityConfig()
        {
            IocContainer = new UnityContainer();
        }

        public static void RegisterComponents()
        {
            IocContainer.RegisterType<ILog, Log>(new InjectionConstructor());
            IocContainer.RegisterType<IAccountDataAccess, AccountDataAccess>();
            IocContainer.RegisterType<ITenantedAccountManager, TenantedAccountManager>(new InjectionConstructor(string.Empty));
            GlobalConfiguration.Configuration.DependencyResolver = new DependencyResolver(IocContainer);
        }

        public static ITenantedAccountManager GetTenantedAccountManager()
        {
            var parameterOverrides = new ParameterOverrides
            {
                {"connectionString", UserManagementConnectionString}
            };
            return IocContainer.Resolve<ITenantedAccountManager>(parameterOverrides);
        }
        
        public static ILog GetLog()
        {
            return IocContainer.Resolve<ILog>();
        }
    }
}