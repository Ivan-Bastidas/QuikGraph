#if SUPPORTS_GRAPHS_SERIALIZATION
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Serialization;
using System.ComponentModel;
using JetBrains.Annotations;

namespace QuikGraph.Serialization
{
    internal static class SerializationHelper
    {
        /// <summary>
        /// Gets all properties that are marked with <see cref="XmlAttributeAttribute"/> on given type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
#if SUPPORTS_CONTRACTS
        [System.Diagnostics.Contracts.Pure]
#endif
        [Pure]
        [NotNull]
        public static IEnumerable<PropertySerializationInfo> GetAttributeProperties([CanBeNull] Type type)
        {
            Type currentType = type;
            while (currentType != null
                   && currentType != typeof(object)
                   && currentType != typeof(ValueType))
            {
                foreach (PropertyInfo property in currentType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    // Must have a get, and not be an index
                    if (!property.CanRead || property.GetIndexParameters().Length > 0)
                        continue;

                    // Is it tagged with XmlAttributeAttribute
                    if (TryGetAttributeName(property, out string name))
                    {
                        if (TryGetDefaultValue(property, out object value))
                            yield return new PropertySerializationInfo(property, name, value);
                        else
                            yield return new PropertySerializationInfo(property, name);
                    }
                }

                currentType = currentType.BaseType;
            }
        }

#if SUPPORTS_CONTRACTS
        [System.Diagnostics.Contracts.Pure]
#endif
        [Pure]
        public static bool TryGetAttributeName([NotNull] PropertyInfo property, out string name)
        {
            var attribute = Attribute.GetCustomAttribute(property, typeof(XmlAttributeAttribute)) as XmlAttributeAttribute;
            if (attribute is null)
            {
                name = null;
                return false;
            }

            name = string.IsNullOrEmpty(attribute.AttributeName)
                ? property.Name
                : attribute.AttributeName;
            return true;
        }

#if SUPPORTS_CONTRACTS
        [System.Diagnostics.Contracts.Pure]
#endif
        [Pure]
        public static bool TryGetDefaultValue([NotNull] PropertyInfo property, out object value)
        {
            var attribute = Attribute.GetCustomAttribute(property, typeof(DefaultValueAttribute)) as DefaultValueAttribute;
            if (attribute is null)
            {
                value = null;
                return false;
            }

            value = attribute.Value;
            return true;
        }
    }
}
#endif