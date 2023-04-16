using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using System.Reflection;

namespace EFRepository.Internals
{
    public abstract partial class AbstractEFRepository<T>
    {
        static Type typeNotMapped = typeof(NotMappedAttribute);
        static ImmutableArray<Type> _interfaces = typeof(T).GetInterfaces().ToImmutableArray();
        static ImmutableDictionary<string, PropertyInfo> _props = typeof(T)
            .GetProperties()
            .ToDictionary(r => r.Name, r => r)
            .ToImmutableDictionary();
        static ImmutableDictionary<string, Func<T, object>> _getters = typeof(T)
            .GetProperties()
            .Where(r => r.CustomAttributes.FirstOrDefault(r => r.AttributeType == typeNotMapped) == null)
            .Where(r => r.CanRead)
            .ToDictionary(r => r.Name, _prop =>
            {
                var instanceExpression = Expression.Parameter(typeof(T), "instance");
                var getterExpression = Expression.Property(instanceExpression, _prop.Name);
                var getterConverted = Expression.Convert(getterExpression, typeof(object));
                var lambda = Expression.Lambda<Func<T, object>>(getterConverted, instanceExpression).Compile();
                return lambda;
            })
            .ToImmutableDictionary();
        static ImmutableDictionary<string, Action<T, object>> _setters = typeof(T)
            .GetProperties()
            .Where(r => r.CustomAttributes.FirstOrDefault(r => r.AttributeType == typeNotMapped) == null)
            .Where(r => r.CanWrite)
            .ToDictionary(r => r.Name, _prop =>
            {
                var instanceExpression = Expression.Parameter(typeof(T));
                var valueExpression = Expression.Parameter(typeof(object), _prop.Name);
                var valueConverted = Expression.Convert(valueExpression, _prop.PropertyType);
                var getterExpression = Expression.Property(instanceExpression, _prop.Name);

                var lambda = Expression.Lambda<Action<T, object>>
                (
                    Expression.Assign(getterExpression, valueConverted), instanceExpression, valueExpression
                ).Compile();

                return lambda;
            })
            .ToImmutableDictionary();
        T Map(T proxy, T item)
        {
            if (proxy == null)
                return item;
            if (proxy == item)
                return proxy;

            var assigned = false;
            //var fields = new List<string>();
            foreach (var getter in _getters)
            {
                var oldval = getter.Value(proxy);
                var newval = getter.Value(item);

                if (oldval == null && newval == null) continue;

                var eq = oldval?.Equals(newval) ?? false;

                if (!eq)
                {
                    var setter = _setters.FirstOrDefault(r => r.Key == getter.Key).Value;
                    if (setter != null)
                    {
                        setter(proxy, newval);
                        assigned = true;
                        //fields.Add(getter.Key);
                    }
                    else
                    {

                    }
                }
            }
            if (assigned)
                return proxy;

            return item;
        }
        public bool SQLEquals(T v1, T v2)
        {
            return _getters.All(r =>
            {
                var vv1 = r.Value(v1);
                var vv2 = r.Value(v2);

                var result = (vv1 == null && vv2 == null) || (vv1?.Equals(vv2) ?? false);
                if (!result)
                    return false;

                return result;
            });
        }
    }
}
