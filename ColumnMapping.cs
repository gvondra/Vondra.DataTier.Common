using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Vondra.DataTier.Common
{
    public class ColumnMapping
    {
        public ColumnMappingAttribute MappingAttribute { get; set; }
        public PropertyInfo Info { get; set; }
        
        public void SetValue(Object model, Object value )
        {
            Info.SetValue(model, value);
        }    
    }
}
