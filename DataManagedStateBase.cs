using System;
using System.Collections.Generic;
using System.Text;

namespace Vondra.DataTier.Common
{
    public class DataManagedStateBase<T> : IDataManagedState<T> where T : class
    {
        public IDataStateManager<T> DataStateManager { get; set; } = new DataStateManager<T>();

        public void AcceptChanges()
        {
            if (DataStateManager != null) { DataStateManager.Original = (T)Clone(); }
        }

        public void AfterCommit()
        {
            AcceptChanges();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
