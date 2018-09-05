using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Vondra.DataTier.Common
{
    public interface IDbProviderFactory
    {
        IDbConnection CreateConnection();
        IDataParameter CreateParameter();
        /// <summary>
        /// Open connection will create a connection instance, specify the connection string, and open the connection
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns>The open connection</returns>
        IDbConnection OpenConnection(string connectionString);
        /// <summary>
        /// First, checks the settings object for an instance of a connection and if no connection is present then a connection is opened
        /// Second, if no transaction is present then a transaction is started
        /// </summary>
        /// <param name="settings"></param>
        void EstablishTransaction(ITransactionHandler transactionHandler);
        void EstablishTransaction(ITransactionHandler transactionHandler, IDbTransactionObserver observer);
    }
}
