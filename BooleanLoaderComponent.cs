using System;
using System.Collections.Generic;
using System.Data;

namespace Vondra.DataTier.Common
{
    public class BooleanLoaderComponent : ILoaderComponent
    {

        public object GetValue(IDataReader reader, int ordinal)
        {
            if (reader.GetFieldType(ordinal).Equals(typeof(string)))
            {
                return GetValueString(reader, ordinal);
            }
            else
            {
                return reader.GetBoolean(ordinal);
            }
        }

        public object GetValueString(IDataReader reader, int ordinal)
        {
            string value = reader.GetString(ordinal).Trim();
                
            if (String.IsNullOrEmpty(value))
            {
                return null;
            }
            else
            {
                return value.ToUpper().StartsWith("Y") || value.ToUpper().StartsWith("T");
            }
        }
    
        public bool IsApplicable(ColumnMapping mapping)
        {
            return mapping.Info.PropertyType.Equals(typeof(bool)) || mapping.Info.PropertyType.Equals(typeof(bool?));
        }
        
    }
}
