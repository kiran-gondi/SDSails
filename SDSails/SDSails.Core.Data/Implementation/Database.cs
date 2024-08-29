namespace SDSails.Core.Data
{
    using System;
    using System.Configuration;
    using System.Data.Common;
    using System.Transactions;
    using SDSails.Core.Data.Entities;
    using SDSails.Core.Data.Enumerations;
    using SDSails.Core.Data.Mappers;

    public class Database : IDatabase
    {
        private readonly string connectionString;

        private readonly DbProviderFactory dbProviderFactory;

        private Database(string connectionString, DbProviderFactory dbProviderFactory)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("String cannot be null or empty", "connectionString");
            }

            if (dbProviderFactory == null)
            {
                throw new ArgumentNullException("dbProviderFactory");
            }

            this.connectionString = connectionString;
            this.dbProviderFactory = dbProviderFactory;
        }

        public string ConnectionString
        {
            get { return this.connectionString; }
        }

        public DbProviderFactory DBProviderFactory
        {
            get { return this.dbProviderFactory; }
        }

        public static Database GetDatabase(DatabaseName database)
        {
            Connection connection = DatabaseConnection.ConnectionStrings[database.ToString()];
            var connectionString = connection.ConnectionString;
            var provider = System.Data.Common.DbProviderFactories.GetFactory(connection.ProviderName);
            return new Database(connectionString, provider);
        }

        public static Database GetConfigurationDatabase(string name)
        {
            var connection = ConfigurationManager.ConnectionStrings[name];
            var connectionString = connection.ConnectionString;
            var provider = System.Data.Common.DbProviderFactories.GetFactory(connection.ProviderName);
            return new Database(connectionString, provider);
        }

        public virtual DbConnection CreateConnection()
        {
            DbConnection newConnection = this.dbProviderFactory.CreateConnection();
            newConnection.ConnectionString = this.ConnectionString;
            return newConnection;
        }

        public Command GetProcedureCommand(string procedure)
        {
            return Command.GetStoredProcedureCommand(procedure, this);
        }

        public Command GetProcedureCommand(string procedure, int timeOut)
        {
            return Command.GetStoredProcedureCommand(procedure, this, timeOut);
        }

        public Command GetTextCommand(string text)
        {
            return Command.GetTextCommand(text, this);
        }

        internal DbConnection GetNewOpenConnection()
        {
            DbConnection connection = null;
            try
            {
                connection = this.CreateConnection();
                connection.Open();
            }
            catch
            {
                if (connection != null)
                {
                    connection.Close();
                }

                throw;
            }

            return connection;
        }

        internal ConnectionWrapper GetOpenConnection()
        {
            return this.GetOpenConnection(true);
        }

        internal ConnectionWrapper GetOpenConnection(bool disposeInnerConnection)
        {
            DbConnection connection = TransactionScopeConnections.GetConnection(this);
            if (connection != null)
            {
                return new ConnectionWrapper(connection, false, true);
            }
            else
            {
                return new ConnectionWrapper(this.GetNewOpenConnection(), disposeInnerConnection, false);
            }
        }
    }
}
