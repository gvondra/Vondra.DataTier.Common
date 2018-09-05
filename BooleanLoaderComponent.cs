using System;
using System.Collections.Generic;
using System.Data;

namespace Vondra.DataTier.Common
{
    public class BooleanLoaderComponent : ILoaderComponent
    {

        public object GetValue(IDataReader reader, int ordinal)
        {
            string value = reader.GetString(ordinal).Trim();
                
            if (String.IsNullOrEmpty(value))
            {
                return null;
            }
            else
            {
                return value.StartsWith("Y");
            }
        }
    
        public bool IsApplicable(ColumnMapping mapping)
        {
            return mapping.Info.PropertyType.Equals(typeof(bool)) || mapping.Info.PropertyType.Equals(typeof(bool?));
        }
        
    }
}
