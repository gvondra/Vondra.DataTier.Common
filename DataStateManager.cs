using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Vondra.DataTier.Common
{
    public class DataStateManager<T> : IDataStateManager<T> where T : class
    {
        public DataStateManager(T original)
        {
            this.Original = original;
        }

        public DataStateManager() { }

        public T Original { get; set; }

        public DataStateManagerState GetState(T target)
        {
            DataStateManagerState state = DataStateManagerState.Unchaged;

            if (this.Original == null) {
                state = DataStateManagerState.New;
            }
            else
            {
                if (!(target == null) && target != Original)
                {
                    if (GetProperties(typeof(T)).Any((PropertyInfo p) => IsChanged(p, target)))
                    {
                        state = DataStateManagerState.Updated;
                    }
                }
            }
            return state;
        }

        public bool IsChanged(PropertyInfo property, T target)
        {
            Object oValue;
            Object tValue;
            bool changed = false;

            oValue = property.GetValue(Original);
            tValue = property.GetValue(target);
            if (oValue != tValue)
            {
                if (oValue == null || tValue == null)
                {
                    changed = true;
                }
                else
                {
                    if (property.PropertyType.Name == "Nullable`1" && property.PropertyType.GenericTypeArguments.Length > 0) {
                        if (IsNullableChanged(property.PropertyType, oValue, tValue)) {
                            changed = true;
                        }
                    }
                    else {
                        if (property.PropertyType.Equals(typeof(byte[])))
                        {
                            if (IsByteArrayChanged((byte[])oValue, (byte[])tValue))
                            {
                                changed = true;
                            }
                        }
                        else
                        {
                            if (oValue.ToString() != tValue.ToString())
                            {
                                changed = true;
                            }
                        }                        
                    }
                }
            }
            return changed;
        }

        public bool IsNullableChanged(Type type, Object oValue, Object tValue)
        {
            bool oHasValue;
            bool tHasValue;
            PropertyInfo valueInfo;
            bool changed = false;
            PropertyInfo hasValue = type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where((PropertyInfo p) => p.CanRead && p.Name == "HasValue" && p.PropertyType.Equals(typeof(bool)))
                .First();

            oHasValue = (bool)hasValue.GetValue(oValue);
            tHasValue = (bool)hasValue.GetValue(tValue);

            if (oHasValue != tHasValue)
            {
                changed = true;
            }
            else
            {
                if (oHasValue)
                {
                    valueInfo = type.GetProperties(BindingFlags.Instance | BindingFlags.Public) 
                        .Where((PropertyInfo p) => p.CanRead && p.Name == "Value") 
                        .First();

                    oValue = valueInfo.GetValue(oValue);
                    tValue = valueInfo.GetValue(tValue);
                    if (valueInfo.PropertyType.Equals(typeof(int)))
                    {
                        changed = (int)oValue != (int)tValue;
                    }                        
                    else
                    {
                        if (valueInfo.PropertyType.Equals(typeof(decimal)))
                        {
                            changed = (decimal)oValue != (decimal)tValue;
                        }                            
                        else
                        {
                            if (valueInfo.PropertyType.Equals(typeof(DateTime)))
                            {
                                changed = (DateTime)oValue != (DateTime)tValue;
                            } 
                            else
                            {
                                if (oValue.ToString() != tValue.ToString())
                                {
                                    changed = true;
                                }
                            }                            
                        }                        
                    }
                    
                }                
            }

            return changed;
        }

        public bool IsByteArrayChanged(byte[] oValue, byte[] tValue)
        {
            bool changed = false;
            int i;

            if (oValue != null && oValue != tValue) 
            {
                if (oValue.Length != tValue.Length)
                {
                    changed = true;
                }
                else
                {
                    i = 0;
                    while (changed == false && i < oValue.Length)
                     {
                        if (oValue[i] != tValue[i])
                        {
                            changed = true;
                        }
                        i += 1;
                     }
                }
            }
            return changed;
        }

        private IEnumerable<PropertyInfo> GetProperties(Type type) 
        {
            return (from p in type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                   where p.CanRead && p.GetCustomAttributes<ColumnMappingAttribute>(true).Any()
                   select p);
        }
    }
}
