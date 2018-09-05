using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Vondra.DataTier.Common
{
    public class Loader : ILoader
    {

        private static Dictionary<Type, List<ColumnMapping>> m_columnMappings;
        private static AutoResetEvent m_mappingsLock;

        static Loader()
        {
            m_mappingsLock = new AutoResetEvent(true);
            m_columnMappings = new Dictionary<Type, List<ColumnMapping>>();
        }

        public List<ILoaderComponent> Components { get; set; }

        public object Load(object data, IDataReader reader)
        {
            List<ColumnMapping> columnMappings = GetColumnMappings(data);            
            int ordinal;

            foreach (ColumnMapping columnMapping in columnMappings)
            {
                try
                {
                    ordinal = reader.GetOrdinal(columnMapping.MappingAttribute.ColumnName);
                }                    
                catch (IndexOutOfRangeException ex)
                {
                    throw new SourceColumnNotFound(columnMapping.MappingAttribute.ColumnName, ex);
                }
                if (ordinal >= 0)
                {
                    if (columnMapping.MappingAttribute.IsNullable && reader.IsDBNull(ordinal))
                    {
                        columnMapping.SetValue(data, null);
                    }
                    else
                    {
                        columnMapping.SetValue(data, GetValue(reader, ordinal, columnMapping));
                    }
                }
            }
            return data;
        }
        
        private Object GetValue(IDataReader reader, int ordinal, ColumnMapping columnMapping)
        {
            bool found = false;
            IEnumerator<ILoaderComponent> enumerator;
            Object value = null;
            if (!(Components == null))
            {
                enumerator = Components.GetEnumerator();
                while (found == false && enumerator.MoveNext())
                { 
                    if (enumerator.Current.IsApplicable(columnMapping))
                    {
                        found = true;
                        value = enumerator.Current.GetValue(reader, ordinal);
                    }
                }
            }
            if (found == false)
            {
                throw new LoaderComponentNotFound(columnMapping);
            }
            return value;
        }

        private List<ColumnMapping> GetColumnMappings(Object data)
        {
            Type type = data.GetType();

            if (m_columnMappings.ContainsKey(type) == false)
            {
                m_mappingsLock.WaitOne();
                try
                {
                    if (m_columnMappings.ContainsKey(type) == false)
                    {
                        m_columnMappings.Add(type, LoadColumnMappings(type));
                    }
                }                    
                finally
                {
                    m_mappingsLock.Set();
                }
            }
            return m_columnMappings[type];
        }

        private List<ColumnMapping> LoadColumnMappings(Type type)
        {
            List<ColumnMapping> mappings = new List<ColumnMapping>();
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            int i;
            int j;
            Object[] attributes;
            ColumnMappingAttribute attribute;
            ColumnMapping mapping;

            if (!(properties == null))
            {
                for (i = 0; i < properties.Length; i+=1)
                {
                    if (properties[i].CanWrite)
                    {
                        attributes = properties[i].GetCustomAttributes(typeof(ColumnMappingAttribute), true);
                        if (!(attributes == null))
                        {
                            for (j = 0; j < attributes.Length; j += 1)
                            {
                                attribute = (ColumnMappingAttribute)attributes[j];
                                mapping = new ColumnMapping() { MappingAttribute = attribute, Info = properties[i] };
                                mappings.Add(mapping);
                            }
                        }
                    }
                }
            }
            return mappings;
        }
    }
}
