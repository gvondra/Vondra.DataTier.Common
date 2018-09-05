using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Vondra.DataTier.Common
{
    public class DbTransaction : IDbTransaction
    {
        private System.Data.IDbTransaction m_innerTransaction;
        private List<IDbTransactionObserver> m_observers = new List<IDbTransactionObserver>();

        public DbTransaction(System.Data.IDbTransaction transaction)
        {
            m_innerTransaction = transaction;
        }

        public System.Data.IDbTransaction InnerTransaction
        {
            get
            {
                return m_innerTransaction;
            }
        }

        public IDbConnection Connection
        {
            get
            {
                return m_innerTransaction.Connection;
            }
        }

        public IsolationLevel IsolationLevel
        {
            get
            {
                return m_innerTransaction.IsolationLevel;
            }
        }

        public void AddObserver(IDbTransactionObserver observer)
        {
            if (m_observers.Contains(observer) == false)
            {
                m_observers.Add(observer);
            }
        }

        public void Commit()
        {
            m_innerTransaction.Commit();
            foreach (IDbTransactionObserver observer in m_observers )
            {
                observer.AfterCommit();
            }
        }

        public void Dispose()
        {
            m_innerTransaction.Dispose();
        }

        public void Rollback()
        {
            m_observers.Clear();
            m_innerTransaction.Rollback();
        }
    }
}
