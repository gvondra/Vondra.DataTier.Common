using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Vondra.DataTier.Common
{
    public class DbProviderFactory : IDbProviderFactory
    {
        private System.Data.Common.DbProviderFactory m_innerFactory;

        public DbProviderFactory()
        {
            m_innerFactory = System.Data.SqlClient.SqlClientFactory.Instance;
        }

        public IDbConnection CreateConnection()
        {
            return m_innerFactory.CreateConnection();
        }

        public IDataParameter CreateParameter()
        {
            return m_innerFactory.CreateParameter();
        }

        public void EstablishTransaction(ITransactionHandler transactionHandler)
        {
            EstablishTransaction(transactionHandler, null);
        }

        public void EstablishTransaction(ITransactionHandler transactionHandler, IDbTransactionObserver observer)
        {
            //first check the connection state.  If it not open then dispose of it
            if (!(transactionHandler.Connection == null)) {
                if (transactionHandler.Connection.State != ConnectionState.Open)
                {
                    transactionHandler.Connection.Dispose();
                    transactionHandler.Connection = null;
                }
            }
            //second open a connection if no connection is already set
            if (transactionHandler.Connection == null) 
            {
                transactionHandler.Connection = OpenConnection(transactionHandler.ConnectionString);
            }
            //third begin a transaction
            if (transactionHandler.Transaction == null) 
            {
                transactionHandler.Transaction = new DbTransaction(transactionHandler.Connection.BeginTransaction());
            }
            //furth add observer
            if (transactionHandler.Transaction != null && observer != null)
            {
                transactionHandler.Transaction.AddObserver(observer);
            }
        }

        public IDbConnection OpenConnection(string connectionString)
        {
            DbConnection connection = null;
            if (String.IsNullOrEmpty(connectionString) == false)
            {
                connection = m_innerFactory.CreateConnection();
                connection.ConnectionString = connectionString;
                connection.Open();
            }
            return connection;
        }
    }
}
