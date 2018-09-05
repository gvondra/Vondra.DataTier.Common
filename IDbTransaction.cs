using System;
using System.Collections.Generic;
using System.Text;

namespace Vondra.DataTier.Common
{
    public interface IDbTransaction : System.Data.IDbTransaction
    {
        System.Data.IDbTransaction InnerTransaction { get; }

        void AddObserver(IDbTransactionObserver observer);
    }
}
