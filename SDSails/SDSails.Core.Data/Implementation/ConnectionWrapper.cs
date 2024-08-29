namespace SDSails.Core.Data
{
    using System;
    using System.Data.Common;

    internal class ConnectionWrapper : IDisposable
    {
        private readonly DbConnection connection;
        private readonly bool disposeConnection;
        private readonly bool isTransactionEnabled;
        public ConnectionWrapper(DbConnection connection, bool disposeConnection, bool isTransactionEnabled)
        {
            this.connection = connection;
            this.disposeConnection = disposeConnection;
            this.isTransactionEnabled = isTransactionEnabled;
        }
        public bool IsTransactionEnabled
        {
            get { return this.isTransactionEnabled; }
        }
        public DbConnection Connection
        {
            get { return this.connection; }
        }
        public void Dispose()
        {
            if (this.disposeConnection)
            {
                this.connection.Dispose();
            }

            GC.SuppressFinalize(this);
        }
    }
}
