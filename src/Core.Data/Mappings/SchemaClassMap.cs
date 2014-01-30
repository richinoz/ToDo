using System;
using FluentNHibernate.Mapping;

namespace Core.Data.Mappings
{
    public class SchemaClassMap<T> : ClassMap<T>
    {
        public SchemaClassMap()
        {
            var ns = typeof (T).Namespace;
            if (string.IsNullOrEmpty(ns)) return;

            var typeSchema = ns.Substring(ns.LastIndexOf(".", StringComparison.Ordinal)+1);
            Schema(typeSchema);
        }
    }
}
