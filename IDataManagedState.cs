using System;
using System.Collections.Generic;
using System.Text;

namespace Vondra.DataTier.Common
{
    public interface IDataManagedState<T> : ICloneable, IDbTransactionObserver
    {
        IDataStateManager<T> DataStateManager { get; set; }

        void AcceptChanges();
    }
}
