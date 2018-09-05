using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Vondra.DataTier.Common
{
    public class ShortLoaderComponent : ILoaderComponent
    {
        public object GetValue(IDataReader reader, int ordinal)
        {
            return reader.GetInt16(ordinal);
        }

        public bool IsApplicable(ColumnMapping mapping)
        {
            return mapping.Info.PropertyType.Equals(typeof(short)) || mapping.Info.PropertyType.Equals(typeof(short?));
        }
    }
}
