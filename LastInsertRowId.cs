using System;
using System.Collections.Generic;
using System.Data;

namespace Vondra.DataTier.Common
{
    public class LastInsertRowId
    {
        public long GetLastInsertRowId(IDbConnection connection)
        {
            IDbCommand command;

            command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "Select last_insert_rowid()";
            return (long)command.ExecuteScalar();
        }
    }
}
