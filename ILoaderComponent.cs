using System;
using System.Collections.Generic;
using System.Data;

namespace Vondra.DataTier.Common
{
    public interface ILoaderComponent
    {
        bool IsApplicable(ColumnMapping mapping);
        Object GetValue(IDataReader reader, int ordinal);
    }
}
