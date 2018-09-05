using System;
using System.Collections.Generic;
using System.Text;

namespace Vondra.DataTier.Common
{
    public enum DataStateManagerState : short
    {
        New = 0,
        Updated = 1,
        Unchaged = 2,
    }

    public interface IDataStateManager<T>
    {
        T Original { get; set; }
        DataStateManagerState GetState(T target);
    }
}
