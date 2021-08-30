using SmartCity.CitizenAccount.Data.Access.Maps.Common;
using SmartCity.CitizenAccount.Data.Access.Maps.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SmartCity.CitizenAccount.Data.Access.DAL
{
    public static class MappingsHelper
    {
        public static IEnumerable<IMap> GetMainMappings()
        {
            var assemblyTypes = typeof(CitizenMap).GetTypeInfo().Assembly.DefinedTypes;
            var mappings = assemblyTypes
                .Where(t => t.Namespace != null && t.Namespace.Contains(typeof(CitizenMap).Namespace))
                .Where(t => typeof(IMap).GetTypeInfo().IsAssignableFrom(t));
            mappings = mappings.Where(x => !x.IsAbstract);

            return mappings.Select(m => (IMap)Activator.CreateInstance(m.AsType())).ToArray();
        }
    }
}
