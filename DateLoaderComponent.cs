using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Vondra.DataTier.Common
{
    public class DateLoaderComponent : ILoaderComponent
    {
        public object GetValue(IDataReader reader, int ordinal)
        {
            return reader.GetDateTime(ordinal);
        }

        public bool IsApplicable(ColumnMapping mapping)
        {
            return IsMatchingType(mapping.Info.PropertyType, typeof(DateTime));
        }

        public bool IsMatchingType(Type propertyType, Type targetType)
        {
            bool isMatch = propertyType.Equals(targetType);

            if (isMatch == false
                    && propertyType.IsGenericType 
                    && !(propertyType.GenericTypeArguments == null)
                    && propertyType.GenericTypeArguments.Length == 1) 
            {

                isMatch = IsMatchingType(propertyType.GenericTypeArguments[0], targetType);
            }
            return isMatch;
        }
        
    }
}
