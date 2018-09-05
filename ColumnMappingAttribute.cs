using System;
using System.Collections.Generic;
using System.Text;

namespace Vondra.DataTier.Common
{
    public class ColumnMappingAttribute : Attribute
    {
        public string ColumnName { get; set; }
        public bool IsNullable { get; set; }

        public ColumnMappingAttribute(string columnName)
        {
            this.ColumnName = columnName;
        }
    }
}
