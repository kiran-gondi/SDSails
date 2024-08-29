namespace SDSails.Core.Data
{
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;

    public class Command
    {
        private DbCommand _command;
        private Database _database;
        private Command()
        {
        }
        private Command(CommandType commandType, string commandText, Database database, int timeOut)
        {
            this._database = database;
            this._command = CreateCommandByCommandType(commandType, commandText, database, timeOut);
        }
        private Command(CommandType commandType, string commandText, Database database)
        {
            this._database = database;
            this._command = CreateCommandByCommandType(commandType, commandText, database);
        }
        public Command SetParameterValue(string name, object value)
        {
            this._command.Parameters[this.BuildParameterName(name)].Value = value ?? DBNull.Value;
            return this;
        }
        public object GetParameterValue(string name)
        {
            return this._command.Parameters[this.BuildParameterName(name)].Value;
        }
        public Command AddInParameter(string name, DbType dbType)
        {
            return this.AddParameter(this._command, name, dbType, ParameterDirection.Input, String.Empty, DataRowVersion.Default, null);
        }
        public Command AddInParameter(string name, DbType dbType, object value)
        {
            return this.AddParameter(this._command, name, dbType, ParameterDirection.Input, String.Empty, DataRowVersion.Default, value);
        }
        public Command AddTableValueParameter(string name, DataTable table, string tableType)
        {
            this.AddInParameter(name, DbType.Object);
            var parameter = this._command.Parameters["@" + name] as SqlParameter;
            parameter.SqlDbType = SqlDbType.Structured;
            parameter.TypeName = tableType;
            parameter.Value = table;
            return this;
        }
        internal static Command GetStoredProcedureCommand(string procedure, Database database)
        {
            return new Command(CommandType.StoredProcedure, procedure, database);
        }
        internal static Command GetStoredProcedureCommand(string procedure, Database database, int timeOut)
        {
            return new Command(CommandType.StoredProcedure, procedure, database, timeOut);
        }
        internal static Command GetTextCommand(string text, Database database)
        {
            return new Command(CommandType.Text, text, database);
        }
        public Command AddOutParameter(string name, DbType dbType, int size)
        {
            return this.AddParameter(this._command, name, dbType, size, ParameterDirection.Output, true, 0, 0, String.Empty, DataRowVersion.Default, DBNull.Value);
        }
        public Command AddParameter(DbCommand command, string name, DbType dbType, ParameterDirection direction, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            return this.AddParameter(command, name, dbType, 0, direction, false, 0, 0, sourceColumn, sourceVersion, value);
        }
        public virtual Command AddParameter(DbCommand command, string name, DbType dbType, int size, ParameterDirection direction, bool nullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            DbParameter parameter = this.CreateParameter(name, dbType, size, direction, nullable, precision, scale, sourceColumn, sourceVersion, value);
            this._command.Parameters.Add(parameter);
            return this;
        }
        protected DbParameter CreateParameter(string name, DbType dbType, int size, ParameterDirection direction, bool nullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            DbParameter param = this.CreateParameter(name);
            this.ConfigureParameter(param, name, dbType, size, direction, nullable, precision, scale, sourceColumn, sourceVersion, value);
            return param;
        }
        protected DbParameter CreateParameter(string name)
        {
            DbParameter param = this._database.DBProviderFactory.CreateParameter();
            param.ParameterName = this.BuildParameterName(name);
            return param;
        }
        protected virtual void ConfigureParameter(DbParameter parameter, string name, DbType dbType, int size, ParameterDirection direction, bool nullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            parameter.DbType = dbType;
            parameter.Size = size;
            parameter.Value = value ?? DBNull.Value;
            parameter.Direction = direction;
            parameter.IsNullable = nullable;
            parameter.SourceColumn = sourceColumn;
            parameter.SourceVersion = sourceVersion;
        }
        public virtual string BuildParameterName(string name)
        {
            return "@" + name;
        }
        private static DbCommand CreateCommandByCommandType(CommandType commandType, string commandText, Database database)
        {
            DbCommand command = database.DBProviderFactory.CreateCommand();
            command.CommandType = commandType;
            command.CommandText = commandText;
            return command;
        }
        private static DbCommand CreateCommandByCommandType(CommandType commandType, string commandText, Database database, int timeOut)
        {
            DbCommand command = database.DBProviderFactory.CreateCommand();
            command.CommandType = commandType;
            command.CommandText = commandText;
            command.CommandTimeout = timeOut;
            return command;
        }
        private DbDataReader DoExecuteReader(CommandBehavior cmdBehavior)
        {
            DbDataReader reader = this._command.ExecuteReader(cmdBehavior);
            return reader;
        }
        public Reader AsReader()
        {
            using (ConnectionWrapper wrapper = this._database.GetOpenConnection(false))
            {
                this._command.CommandTimeout = 600; ////Time in seconds
                PrepareCommand(this._command, wrapper.Connection);

                DbDataReader result;
                if (wrapper.IsTransactionEnabled)
                {
                    result = this.DoExecuteReader(CommandBehavior.Default);
                }
                else
                {
                    result = this.DoExecuteReader(CommandBehavior.CloseConnection);
                }

                return new Reader(result);
            }
        }
        public T AsScalar<T>()
        {
            using (ConnectionWrapper wrapper = this._database.GetOpenConnection())
            {
                PrepareCommand(this._command, wrapper.Connection);
                object result = this.DoExecuteScalar();
                if (result is T)
                {
                    return (T)result;
                }

                throw new InvalidCastException();
            }
        }
        private object DoExecuteScalar()
        {
            object returnValue = this._command.ExecuteScalar();
            return returnValue;
        }
        public int AsNonQuery()
        {
            using (ConnectionWrapper wrapper = this._database.GetOpenConnection())
            {
                this._command.CommandTimeout = 600; ////Time in seconds
                PrepareCommand(this._command, wrapper.Connection);
                return this.DoExecuteNonQuery();
            }
        }
        private static void PrepareCommand(DbCommand command, DbConnection connection)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }

            command.Connection = connection;
        }
        private int DoExecuteNonQuery()
        {
            int rowsAffected = this._command.ExecuteNonQuery();
            return rowsAffected;
        }
    }
}
