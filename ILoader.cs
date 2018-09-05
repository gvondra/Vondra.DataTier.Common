using System;
using System.Collections.Generic;
using System.Data;

namespace Vondra.DataTier.Common
{
    public interface ILoader
    {
        Object Load(Object data, IDataReader reader);
    }
}
