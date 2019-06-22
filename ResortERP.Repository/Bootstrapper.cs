


using SimpleInjector;
using ResortERP.Core;
using ResortERP.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Repository
{
    public static class Bootstrapper
    {
        public static void Bootstrap(Container container)
        {
            // Register your types, for instance using the scoped lifestyle:
            container.Register<ISmartConnectionStringProvider, SmartConnectionStringProvider>(container.Options.DefaultScopedLifestyle);

            container.Register<IDbContext, SmartContext>(container.Options.DefaultScopedLifestyle);

            container.Register<IDefaultConnectionStringProvider, DefaultConnectionStringProvider>(container.Options.DefaultScopedLifestyle);

            container.Register<IContext, DefaultContext>(container.Options.DefaultScopedLifestyle);
        }
    }
}
