using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Vondra.DataTier.Common
{
    public class DecimalLoaderComponent : ILoaderComponent
    {
        public object GetValue(IDataReader reader, int ordinal)
        {
            return reader.GetDecimal(ordinal);
        }

        public bool IsApplicable(ColumnMapping mapping)
        {
            return mapping.Info.PropertyType.Equals(typeof(decimal)) || mapping.Info.PropertyType.Equals(typeof(decimal?));
        }
    }
}
