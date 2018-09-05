using System;
using System.Collections.Generic;
using System.Data;

namespace Vondra.DataTier.Common
{
    public interface ITransactionHandler : ISettings
    {
        IDbConnection Connection { get; set; }
        IDbTransaction Transaction { get; set; }
    }
}
