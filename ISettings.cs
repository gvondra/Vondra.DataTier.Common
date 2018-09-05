using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Vondra.DataTier.Common
{
    public interface ISettings
    {
        string ConnectionString { get; }
    }
}
