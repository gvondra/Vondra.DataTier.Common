using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Vondra.DataTier.Common
{
    public interface IGenericDataFactory<T>
    {
        IEnumerable<T> GetData(ISettings settings, IDbProviderFactory providerFactory, string commandText, Func<T> createModelObject);
        IEnumerable<T> GetData(ISettings settings, IDbProviderFactory providerFactory, string commandText, Func<T> createModelObject, IEnumerable<IDataParameter> parameters);
        IEnumerable<T> GetData(ISettings settings, IDbProviderFactory providerFactory, string commandText, Func<T> createModelObject, IEnumerable<IDataParameter> parameters, CommandType commandType);
        IEnumerable<T> GetData(ISettings settings, IDbProviderFactory providerFactory, string commandText, Func<T> createModelObject, Action<IEnumerable<T>> assignDataStateManager);
        IEnumerable<T> GetData(ISettings settings, IDbProviderFactory providerFactory, string commandText, Func<T> createModelObject, Action<IEnumerable<T>> assignDataStateManager, IEnumerable<IDataParameter> parameters);
        IEnumerable<T> GetData(ISettings settings, IDbProviderFactory providerFactory, string commandText, Func<T> createModelObject, Action<IEnumerable<T>> assignDataStateManager, IEnumerable<IDataParameter> parameters, CommandType commandType);
        IEnumerable<R> LoadData<R>(IDataReader reader, Func<R> createModelObject );
        IEnumerable<R> LoadData<R>(IDataReader reader, Func<R> createModelObject, Action<IEnumerable<R>> assignDataStateManager);
    }
}
