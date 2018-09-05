using System;
using System.Collections.Generic;
using System.Text;

namespace Vondra.DataTier.Common
{
    public class LoaderComponentNotFound : ApplicationException
    {

        public LoaderComponentNotFound(ColumnMapping columnMapping) : base(String.Format("Loader not found for property {0} on {1}", columnMapping.Info.Name, columnMapping.Info.DeclaringType.FullName))
        {
        }
    }
}
