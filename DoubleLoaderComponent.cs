using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Vondra.DataTier.Common
{
    public class DoubleLoaderComponent : ILoaderComponent
    {
        public object GetValue(IDataReader reader, int ordinal)
        {
            return reader.GetDouble(ordinal);
        }

        public bool IsApplicable(ColumnMapping mapping)
        {
            return mapping.Info.PropertyType.Equals(typeof(double)) || mapping.Info.PropertyType.Equals(typeof(double?));
        }
    }
}
