namespace SDSailsAdminUI
{
    using Microsoft.Practices.Unity;
    using SDSails.Core.Data;
    using SDSails.Core.Data.Mappers;
    using SDSailsBusiness.Implementation;
    using SDSailsBusiness.Interfaces;
    using SDSailsGateway.AutoMapperInitializations;
    using SDSailsGateway.Implementation;
    using SDSailsGateway.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();
            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<IDataAccessMapper, DataAccessMapper>();
            container.RegisterType<IAutoMapperSupplier, ApplicationdataAutomapperSupplier>();

            container.RegisterType<IConnectionStringRepository, ConnectionStringRepository>();
            container.RegisterType<IConnectionConfig, ConnectionConfig>();

            container.RegisterType<ICommonBL, CommonBL>();
            container.RegisterType<ICommonGateway, CommonGateway>();

            container.Resolve<IDataAccessMapper>();
            container.Resolve<IAutoMapperSupplier>();
            container.Resolve<IConnectionStringRepository>();

            container.Resolve<IConnectionConfig>();
            container.Resolve<IDataAccessMapper>();

            container.Resolve<ICommonGateway>();
            container.Resolve<ICommonBL>();

            MvcUnityContainer.Container = container;
            return container;
        }
    }
}