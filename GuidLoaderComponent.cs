using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Vondra.DataTier.Common
{
    public class GuidLoaderComponent : ILoaderComponent
    {
        public object GetValue(IDataReader reader, int ordinal)
        {
            return Guid.Parse(reader.GetString(ordinal).Trim());
        }

        public bool IsApplicable(ColumnMapping mapping)
        {
            return mapping.Info.PropertyType.Equals(typeof(Guid)) || mapping.Info.PropertyType.Equals(typeof(Guid?));
        }
    }
}
