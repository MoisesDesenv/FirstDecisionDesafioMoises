using System;
using System.Linq;

namespace FirstDecisionDesafioMoises.Infraestructure.Extensions
{
    public static class TypeExtensions
    {
        private static Type[] PrimitiveTypes => new Type[]
        {
            typeof(string),
            typeof(decimal),
            typeof(DateTime),
            typeof(DateTimeOffset),
            typeof(TimeSpan),
            typeof(Guid),
        };

        public static bool IsPrimitiveType(this Type type)
            => (Nullable.GetUnderlyingType(type) ?? type).IsPrimitive ||
               PrimitiveTypes.Contains(Nullable.GetUnderlyingType(type) ?? type);
    }
}