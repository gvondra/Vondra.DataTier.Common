using System;
using System.Collections.Generic;
using System.Data;

namespace Vondra.DataTier.Common
{
    public class BytesLoaderComponent : ILoaderComponent
    {

        public Object GetValue(IDataReader reader, int ordinal)
        {
            return (byte[])reader.GetValue(ordinal);
        }
    
        public bool IsApplicable(ColumnMapping mapping)
        {
            return mapping.Info.PropertyType.Equals(typeof(byte[]));
        }
        
    }
}
