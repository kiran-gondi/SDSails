namespace SDSails.Core.Data
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    //Needs to be used if required only.
    public abstract class SQLDBHelper : IDisposable
    {
        #region "Private Member Variables""
        private SqlConnection objConnection = null;
        private string strConnectionString = string.Empty;
        private SqlTransaction objTransaction = null;
        protected SqlCommand objCommand = null;
        protected SqlDataAdapter objAdapter = null;

        #endregion

        #region "Public Member Variables""
        #endregion


        #region Constructors
        /// <summary>
        /// Initializes the member variable strConnectionString.
        /// </summary>
        protected SQLDBHelper()
        {
            //Get the connection string from ConfigFile and assign it to 
            //m_strConnectionString member variable.

            string strConnString = string.Empty;
            strConnString = ConfigurationManager.AppSettings["DatabaseConnection"].ToString();

            if (strConnString != null)
            {
                if (strConnString.Trim().Length > 0)
                    strConnectionString = strConnString;
                else
                    strConnectionString = string.Empty;
            }
        }
        #endregion

        #region "Public Properites"
        public ConnectionState ConnectionState
        {
            get
            {
                return (objConnection.State);
            }
        }
        #endregion

        #region "Private Methods"

        /// <summary>
        /// This function gives back the Transaction connection if
        /// exists otherwise creates a new database connection.
        /// </summary>
        /// <param name="pConnection">Variable to hold DB Connection</param>
        /// <returns>True -succeeds,False-Fails </returns>
        private bool GetConnection(ref System.Data.SqlClient.SqlConnection pConnection)
        {
            try
            {
                IDbConnection objDBConn = null;

                if (objTransaction != null)
                {
                    objDBConn = objTransaction.Connection;

                    if (objDBConn != null)
                    {
                        pConnection = (SqlConnection)objDBConn;
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception)
            {
                Dispose();
                throw;
            }

            return OpenConnection(ref pConnection);
        }

        /// <summary>
        /// This function opens the database connection 
        /// </summary>
        /// <param name="pConnection">Variable to hold DB Connection.</param>
        /// <returns>True -succeeds,False-Fails </returns>
        private bool OpenConnection(ref System.Data.SqlClient.SqlConnection pConnection)
        {
            SqlConnection objSqlConn = null;
            bool boolReturn = false;
            try
            {
                //Check for ConnectionString
                if (strConnectionString == null)
                {
                    return boolReturn;
                }

                //Check for ConnectionString value.
                if ((strConnectionString != null) && (!strConnectionString.Trim().Equals("")))
                {
                    objSqlConn = new SqlConnection(strConnectionString);
                    objSqlConn.Open();
                    pConnection = objSqlConn;
                    boolReturn = true;
                }
                else
                {
                    boolReturn = false;
                }
                return boolReturn;
            }
            catch (Exception)
            {
                Dispose();
                throw;
            }
            //return (boolReturn);
        }

        /// <summary>
        /// This function closes the database connection 
        /// </summary>
        /// <param name="pConnection">Variable to hold DB Connection</param>
        /// <returns>True -succeeds,False-Fails </returns>
        private bool CloseConnection(ref System.Data.SqlClient.SqlConnection pConnection)
        {
            bool boolReturn = false;

            if (pConnection == null)
            {
                return boolReturn;
            }

            //An application can call Close more than one time. 
            //No exception is generated.
            try
            {
                if (objTransaction != null) return true;

                if (pConnection != null)
                    pConnection.Close();

                pConnection = null;
                boolReturn = true;

                return boolReturn;
            }
            catch (Exception)
            {
                Dispose();
                throw;
            }

        }

        #endregion

        #region "Public Methods"

        /// <summary>
        /// This function opens the database connection 
        /// </summary>
        public void OpenConnection()
        {
            try
            {
                if (objConnection == null)
                    GetConnection(ref objConnection);

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// This function creates a new SQL Command Object by assigning Connection 
        ///  and Transaction if the transaction is opened before.
        /// </summary>
        /// <param name="commandText">Procedure name</param>
        /// <returns>command object</returns>
        public SqlCommand GetSQLCommand(string commandText)
        {
            SqlCommand objlocalCommand = null;

            try
            {
                if (objTransaction == null)
                    objlocalCommand = new SqlCommand(commandText, objConnection);
                else
                {
                    if (objCommand == null)
                    {
                        objCommand = new SqlCommand(commandText, objConnection, objTransaction);
                    }
                    else
                    {
                        objCommand.CommandText = commandText;
                        objCommand.Parameters.Clear();
                    }
                    objlocalCommand = objCommand;
                }
                objlocalCommand.CommandTimeout = 100;
            }
            catch (Exception)
            {
                throw;
            }

            return (objlocalCommand);
        }

        /// <summary>
        /// To Get the SQLDataAdapter with existing Command object
        /// </summary>
        /// <returns></returns>

        public SqlDataAdapter GetSQLDataAdapter()
        {
            return (GetSQLDataAdapter(objCommand));
        }
        /// <summary>
        /// To Get the existing or New SQLDataAdapter
        /// </summary>
        /// <param name="pSqlCommand"></param>
        /// <returns></returns>
        public SqlDataAdapter GetSQLDataAdapter(SqlCommand pSqlCommand)
        {
            try
            {
                if (objAdapter == null)
                    objAdapter = new SqlDataAdapter();

                if (pSqlCommand != null)
                    objAdapter.SelectCommand = pSqlCommand;
                else
                    objAdapter.SelectCommand = GetSQLCommand();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //CloseAdapter();
                //CloseCommand();
            }
            return (objAdapter);
        }

        /// <summary>
        /// This function get a SQL Command Object form the existing connection 
        /// </summary>
        /// <returns>command object</returns>
        public SqlCommand GetSQLCommand()
        {
            return (GetSQLCommand(string.Empty));
        }

        /// <summary>
        /// This function creates a new Transaction Connection is there
        /// is no Transaction Connection Exists before.
        /// </summary>
        /// <returns>True -succeeds,False-Fails </returns>
        public bool BeginTransaction()
        {
            bool boolReturn = false;
            SqlConnection objSQLTransConn = null;
            SqlTransaction objSQLTransaction = null;

            if (objTransaction != null)
            {
                return boolReturn;
            }

            //Check for ConnectionString
            if ((strConnectionString == null) || (strConnectionString.Trim().Length == 0))
            {
                return boolReturn;
            }

            try
            {
                //Create the SQL connecton
                objSQLTransConn = new SqlConnection(strConnectionString);
                objSQLTransConn.Open();
                //Get the transaction
                objSQLTransaction = objSQLTransConn.BeginTransaction();
                //objSQLTransaction.IsolationLevel = IsolationLevel.ReadUncommitted;

                objConnection = objSQLTransConn;

                if (objSQLTransaction != null)
                {
                    objTransaction = objSQLTransaction;
                    return true;
                }
                else return false;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                objSQLTransaction = null;
            }
        }

        /// <summary>
        /// This function Commits the trasaction and releases the 
        /// Trasaction and Connection.
        /// </summary>
        /// <returns>True -succeeds,False-Fails </returns>
        public bool CommitTransaction()
        {
            bool boolReturn = false;
            SqlConnection objSqlConn = null;

            if (objTransaction == null)
            {
                return boolReturn;
            }

            try
            {
                objSqlConn = (SqlConnection)objTransaction.Connection;
                objTransaction.Commit();
                boolReturn = true;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                objCommand = null;
                objTransaction = null;
                if (objSqlConn != null)
                {
                    objSqlConn.Close();
                    objSqlConn = null;
                }
                if (objConnection != null)
                {
                    objConnection.Close();
                    objConnection = null;
                }
            }
            return boolReturn;
        }

        /// <summary>
        /// This function rolls back the trasaction and releases the
        /// Trasaction and Connection.
        /// </summary>
        /// <returns>True -succeeds,False-Fails </returns>
        public bool RollBackTransaction()
        {
            bool boolReturn = false;
            SqlConnection objSqlConn = null;

            if (objTransaction == null)
            {
                return boolReturn;
            }

            try
            {
                objSqlConn = (SqlConnection)objTransaction.Connection;
                objTransaction.Rollback();
                boolReturn = true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseAdapter();
                CloseCommand();
                objTransaction = null;

                if (objSqlConn != null)
                {
                    objSqlConn.Close();
                    objSqlConn = null;
                }
                if (objConnection != null)
                {
                    objConnection.Close();
                    objConnection = null;
                }
            }
            return boolReturn;
        }

        /// <summary>
        /// This function closes the database connection
        /// </summary>
        /// <returns>True -succeeds,False-Fails </returns>
        public bool CloseConnection()
        {
            bool boolReturn = false;

            if (objConnection == null)
            {
                return boolReturn;
            }

            //An application can call Close more than one time. 
            //No exception is generated.
            try
            {
                if (objTransaction != null) return true;

                if (objConnection.State != ConnectionState.Closed)
                {
                    CloseAdapter();
                    CloseCommand();
                    objConnection.Close();
                }
                objConnection = null;
                boolReturn = true;

            }
            catch (Exception)
            {
                throw;
            }
            return (boolReturn);
        }

        #endregion


        #region IDisposable Members

        public virtual void Dispose()
        {
            DisposeConnection();
            //objTransaction.Dispose();
            if (objConnection != null)
                objConnection.Dispose();

        }
        private void CloseAdapter()
        {
            if (objAdapter != null)
            {
                objAdapter.Dispose();
                objAdapter = null;
            }
        }

        private void CloseCommand()
        {
            if (objCommand != null)
            {
                objCommand.Dispose();
                objCommand = null;
            }
        }

        private void DisposeConnection()
        {
            CloseAdapter();
            CloseCommand();
            CloseConnection();
        }


        #endregion
    }
}
