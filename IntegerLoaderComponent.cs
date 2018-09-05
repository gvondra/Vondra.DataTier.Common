using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Vondra.DataTier.Common
{
    public class IntegerLoaderComponent : ILoaderComponent
    {

        public Object GetValue(IDataReader reader, int ordinal)
        {
            return reader.GetInt32(ordinal);
        }
    
        public bool IsApplicable(ColumnMapping mapping)
        {
            return mapping.Info.PropertyType.Equals(typeof(int)) || mapping.Info.PropertyType.Equals(typeof(int?));
        }
    
    }
}
