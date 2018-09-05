using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Vondra.DataTier.Common
{
    public class ByteLoaderComponent : ILoaderComponent
    {

        public Object GetValue(IDataReader reader, int ordinal)
        {
            return reader.GetByte(ordinal);
        }
    
        public bool IsApplicable(ColumnMapping mapping)
        {
            return mapping.Info.PropertyType.Equals(typeof(byte)) || mapping.Info.PropertyType.Equals(typeof(byte?));
        }
    
    }
}
