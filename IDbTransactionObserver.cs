using System;
using System.Collections.Generic;
using System.Text;

namespace Vondra.DataTier.Common
{
    public interface IDbTransactionObserver
    {
        void AfterCommit();
    }
}
