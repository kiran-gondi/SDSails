namespace SDSailsAdminUI
{
    using Microsoft.Practices.Unity;
    using SDSails.Core.Data;
    using SDSails.Core.Data.Entities;
    using SDSails.Core.Data.Mappers;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Web;

    public class ConnectionConfig : IConnectionConfig
    {
        /// <summary>
        /// Static property container.
        /// </summary>
        //private static UnityContainer container;
        private readonly IUnityContainer _unityContainer;
        private readonly IConnectionStringRepository _connectionStringRepository;

        public ConnectionConfig(IConnectionStringRepository connectionStringRepository, IUnityContainer unityContainer)
        {
            _connectionStringRepository = connectionStringRepository;
            _unityContainer = unityContainer;

            this.Initialize();
        }

        public void Initialize()
        {
            if (DatabaseConnection.ConnectionStrings.Count == 0)
            {
                GetConnectionStrings();
            }
        }

        /// <summary>
        /// Gets the connection strings.
        /// </summary>
        /// <remarks></remarks>
        private void GetConnectionStrings()
        {
            Collection<Connection> connectionStringsBE = _connectionStringRepository.GetDatabaseConnectionStrings();
            FillConnectionStrings(connectionStringsBE);
        }

        /// <summary>
        /// Fills the connection strings.
        /// </summary>
        /// <param name="connections">The connections.</param>
        /// <remarks></remarks>
        private static void FillConnectionStrings(Collection<Connection> connections)
        {
            foreach (Connection connection in connections)
            {
                DatabaseConnection.ConnectionStrings.Add(connection.DatabaseName, connection);
            }
        }
    }
}