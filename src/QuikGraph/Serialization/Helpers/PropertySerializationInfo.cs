#if SUPPORTS_GRAPHS_SERIALIZATION
using System.Reflection;
using JetBrains.Annotations;

namespace QuikGraph.Serialization
{
    internal struct PropertySerializationInfo
    {
        /// <summary>
        /// Gets the embedded <see cref="PropertyInfo"/>.
        /// </summary>
#if SUPPORTS_CONTRACTS
        [System.Diagnostics.Contracts.Pure]
#endif
        [NotNull]
        public PropertyInfo Property { get; }

        /// <summary>
        /// Gets the property name.
        /// </summary>
#if SUPPORTS_CONTRACTS
        [System.Diagnostics.Contracts.Pure]
#endif
        [NotNull]
        public string Name { get; }

        private readonly bool _hasValue;

        [CanBeNull]
        private readonly object _value;

        public PropertySerializationInfo([NotNull] PropertyInfo property, [NotNull] string name)
            : this(property, name, null)
        {
        }

        public PropertySerializationInfo(
            [NotNull] PropertyInfo property,
            [NotNull] string name,
            [CanBeNull] object value)
        {
            Property = property;
            Name = name;
            _value = value;
            _hasValue = _value != null;
        }

#if SUPPORTS_CONTRACTS
        [System.Diagnostics.Contracts.Pure]
#endif
        [Pure]
        public bool TryGetDefaultValue(out object value)
        {
            value = _value;
            return _hasValue;
        }
    }
}
#endif