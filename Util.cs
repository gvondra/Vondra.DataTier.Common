using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Xml;

namespace Vondra.DataTier.Common
{
    public static class Util
    {
        public static IDataParameter CreateParameter(IDbProviderFactory providerFactory, DbType type)
        {
            return Util.CreateParameter(providerFactory, null, type);
        }

        public static IDataParameter CreateParameter(IDbProviderFactory providerFactory, string name, DbType type)
        {
            IDataParameter parameter = providerFactory.CreateParameter();
            if (String.IsNullOrEmpty(name) == false) {
                parameter.ParameterName = name;
            }
            parameter.DbType = type;
            return parameter;
        }

        public static Int64 GetLastRowId(ITransactionHandler transactionHandler)
        {
            using (IDbCommand command = transactionHandler.Connection.CreateCommand()) {
                command.CommandType = CommandType.Text;
                command.CommandText = "Select last_insert_rowid(); ";

                return (Int64)command.ExecuteScalar();
            }
        }

        public static Object GetParameterValue(Guid? value)
        {
            if (value.HasValue && value.Value.Equals(Guid.Empty) == false)
            {
                return value.Value;
            }
            else
            {
                return DBNull.Value;
            }
        }

        public static Object GetParameterValue(string value)
        {
            if (value == null) {
                value = String.Empty;
            }
            return value.TrimEnd();
        }

        public static Object GetParameterValue(decimal? value)
        {
            if (value.HasValue)
            {
                return value.Value;
            }
            else
            {
                return DBNull.Value;
            }
        }

        public static Object GetParameterValue(double? value)
        {
            if (value.HasValue)
            {
                return value.Value;
            }
            else
            {
                return DBNull.Value;
            }
        }

        public static Object GetParameterValue(int? value)
        {
            if (value.HasValue)
            {
                return value.Value;
            }
            else
            {
                return DBNull.Value;
            }
        }

        public static Object GetParameterValue(short? value)
        {
            if (value.HasValue)
            {
                return value.Value;
            }
            else
            {
                return DBNull.Value;
            }
        }

        public static Object GetParameterValue(byte? value)
        {
            if (value.HasValue)
            {
                return value.Value;
            }
            else
            {
                return DBNull.Value;
            }
        }

        public static Object GetParameterValue(DateTime? value)
        {
            if (value.HasValue)
            {
                return value.Value;
            }
            else
            {
                return DBNull.Value;
            }
        }

        public static Object GetParameterValue(byte[] value)
        {
            if (value != null)
            {
                return value;
            }
            else
            {
                return DBNull.Value;
            }
        }

        public static Object GetParameterValue(bool? value)
        {
            if (value.HasValue && value.Value)
            {
                return "Y";
            }                
            else if (value.HasValue && value.Value == false)
            {
                return "N";
            }
            else
            {
                return string.Empty;
            }
        }

        public static Object GetParameterValue(XmlNode value)
        {
            if (value != null)
            {
                return value.OuterXml;
            }                
            else
            {
                return DBNull.Value;
            }
        }

        public static void AddParameter(IDbProviderFactory providerFactory,
                                IList parameterCollection,
                                string name,
                                DbType dbType,
                                Object value)
        {
            IDataParameter parameter = CreateParameter(providerFactory, name, dbType);
            parameter.Value = value;
            parameterCollection.Add(parameter);
        }

        public static void AssignDataStateManager<T>(IEnumerable<T> data) where T : class, IDataManagedState<T>
        {
            foreach (T d in data) 
            {
                d.DataStateManager = new DataStateManager<T>((T)d.Clone());
            }
        }
    }
}
