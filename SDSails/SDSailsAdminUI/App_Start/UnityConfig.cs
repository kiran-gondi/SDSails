namespace SDSailsAdminUI
{
    using Microsoft.Practices.Unity;
    using SDSails.Core.Data;
    using SDSails.Core.Data.Entities;
    using SDSails.Core.Data.Mappers;
    using SDSailsBusiness.Implementation;
    using SDSailsBusiness.Interfaces;
    using SDSailsGateway.AutoMapperInitializations;
    using SDSailsGateway.Implementation;
    using SDSailsGateway.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class UnityConfig
    {
        public static UnityContainer container = null;

        /// <summary>
        /// Allows an implementer to register services during the bootstrapping process.
        /// </summary>
        /// <remarks></remarks>
        public static void RegisterComponents()
        {
            container = new UnityContainer();

            container.RegisterType<IDataAccessMapper, DataAccessMapper>();
            container.RegisterType<IAutoMapperSupplier, ApplicationdataAutomapperSupplier>();

            container.RegisterType<IConnectionStringRepository, ConnectionStringRepository>();
            container.RegisterType<IConnectionConfig, ConnectionConfig>();

            container.RegisterType<ICommonBL, CommonBL>();
            container.RegisterType<ICommonGateway, CommonGateway>();

            container.Resolve<IAutoMapperSupplier>();
            container.Resolve<IConnectionStringRepository>();

            container.Resolve<IConnectionConfig>();
            container.Resolve<IDataAccessMapper>();

            container.Resolve<ICommonGateway>();
            container.Resolve<ICommonBL>();
        }
    }
}