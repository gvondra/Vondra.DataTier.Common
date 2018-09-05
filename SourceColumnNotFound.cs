using System;
using System.Collections.Generic;
using System.Text;

namespace Vondra.DataTier.Common
{
    public class SourceColumnNotFound : ApplicationException
    {
        public SourceColumnNotFound(string columnName) : base($"The dataset does not include the column \"{columnName}\"")
        {
            
        }

        public SourceColumnNotFound(string columnName, Exception innerException) : base($"The dataset does not include the column \"{columnName}\"", innerException)
        {

        }
    }
}
