using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Vondra.DataTier.Common
{
    public class GenericDataFactory<T> : IGenericDataFactory<T>
    {

        public ILoaderFactory LoaderFactory { get; set; }

        public GenericDataFactory() 
        {
            this.LoaderFactory = new LoaderFactory();
        }

        public GenericDataFactory(ILoaderFactory loaderFactory)
        {
            this.LoaderFactory = LoaderFactory;
        }

        public IEnumerable<T> GetData(ISettings settings, IDbProviderFactory providerFactory, string commandText, Func<T> createModelObject, Action<IEnumerable<T>> assignDataStateManager)
        {
            return GetData(settings, providerFactory, commandText, createModelObject, assignDataStateManager, null, CommandType.StoredProcedure);
        }

        public IEnumerable<T> GetData(ISettings settings, IDbProviderFactory providerFactory, string commandText, Func<T> createModelObject, Action<IEnumerable<T>> assignDataStateManager, IEnumerable<IDataParameter> parameters)
        {
            return GetData(settings, providerFactory, commandText, createModelObject, assignDataStateManager, parameters, CommandType.StoredProcedure);
        }

        public IEnumerable<T> GetData(ISettings settings, IDbProviderFactory providerFactory, string commandText, Func<T> createModelObject, Action<IEnumerable<T>> assignDataStateManager, IEnumerable<IDataParameter> parameters, CommandType commandType)
        {
            IEnumerable<T> data = GetData(settings, providerFactory, commandText, createModelObject, parameters, commandType);

            if (assignDataStateManager != null)
            {
                assignDataStateManager.Invoke(data);
            }

            return data;
        }

        public IEnumerable<T> GetData(ISettings settings, IDbProviderFactory providerFactory, string commandText, Func<T> createModelObject)
        {
            return GetData(settings, providerFactory, commandText, createModelObject, null, CommandType.StoredProcedure);
        }

        public IEnumerable<T> GetData(ISettings settings, IDbProviderFactory providerFactory, string commandText, Func<T> createModelObject, IEnumerable<IDataParameter> parameters)
        {
            return GetData(settings, providerFactory, commandText, createModelObject, parameters, CommandType.StoredProcedure);
        }

        public IEnumerable<T> GetData(ISettings settings, 
                                    IDbProviderFactory providerFactory, 
                                    string commandText, 
                                    Func<T> createModelObject,
                                    IEnumerable<IDataParameter> parameters, 
                                    CommandType commandType)
        {
            IDataReader reader;
            IEnumerable<T> result;

            using (IDbConnection connection = providerFactory.OpenConnection(settings.ConnectionString)) {
                using (IDbCommand command = connection.CreateCommand())
                {                
                    command.CommandText = commandText;
                    command.CommandType = commandType;

                    if (parameters != null)
                    {
                        foreach (IDataParameter parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    reader = command.ExecuteReader();
                    try
                    {
                        result = LoadData(reader, createModelObject);
                    }   
                    finally
                    {
                        reader.Close();
                        reader.Dispose();
                    }                
                }
            }
            return result;
        }

        public IEnumerable<R> LoadData<R>(IDataReader reader, Func<R> createModelObject)
        {
            return this.LoadData(this.LoaderFactory.CreateLoader(), reader, createModelObject);
        }

        public IEnumerable<R> LoadData<R>(IDataReader reader, Func<R> createModelObject, Action<IEnumerable<R>> assignDataStateManager)
        {
            IEnumerable<R> data = LoadData(reader, createModelObject);

            if (assignDataStateManager != null)
            {
                assignDataStateManager.Invoke(data);
            }
            return data;
        }

        public IEnumerable<R> LoadData<R>(ILoader loader, IDataReader reader, Func<R> createModelObject)
        {
            List<R> result = new List<R>();
            R data;

        
            while (reader.Read())
            {
                data = createModelObject.Invoke();
                data = (R)loader.Load(data, reader);
                result.Add(data);
            }
            return result;
        }
    }
}
