namespace SDSails.Core.Data
{
    using System.Collections.ObjectModel;
    using System.Data;
    using AutoMapper;
    using SDSails.Core;
    using SDSails.Core.Data;
    using SDSails.Core.Data.Entities;
    using SDSails.Core.Data.Mappers;
    using SDSails.Core.Data.Enumerations;
    using System;

    /// <summary>
    /// Class ConnectionStringRepository.
    /// </summary>
    public class ConnectionStringRepository : IConnectionStringRepository
    {
        /// <summary>
        /// The Database.
        /// </summary>
        private IDatabase _db;

        /// <summary>
        /// The Database AccessMapper.
        /// </summary>
        private IDataAccessMapper _dataAccessMapper;

        /// <summary>
        /// Initializes a new instance of the ConnectionStringRepository class.
        /// </summary>
        /// <param name="dataAccessMapper"> Data AccessMapper. </param>
        public ConnectionStringRepository(IDataAccessMapper dataAccessMapper)
        {
            this._dataAccessMapper = dataAccessMapper;
            this._db = Database.GetConfigurationDatabase("DataAccess.Properties.Settings.DBConfigConnectionString");
        }

        /// <summary>
        /// Gets the Database ConnectionStrings.
        /// </summary>        
        /// <returns>Connection Strings.</returns>
        public Collection<Connection> GetDatabaseConnectionStrings()
        {
            Collection<Connection> connectionStrings = null;

            try
            {
                Reader reader = _db.GetProcedureCommand(@"GspConnectionStrings")
                                   .AsReader();

                connectionStrings = new Collection<Connection>();
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        connectionStrings = _dataAccessMapper.MapRecords<Connection>(reader);

                        reader.Close();
                    }
                }


            }
            catch (Exception ex)
            {

                throw;
            }
            return connectionStrings;

        }
    }
}
