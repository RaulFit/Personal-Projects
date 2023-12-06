using System.Runtime.CompilerServices;

namespace ExtensionMethods
{
    public static class ExtensionMethods
    {
        public static bool All<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            IsNull(source);
            IsNull(predicate);

            foreach (var item in source)
            {
                if (!predicate(item))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool Any<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            IsNull(source);
            IsNull(predicate);

            foreach (var item in source)
            {
                if (predicate(item))
                {
                    return true;
                }
            }

            return false;
        }

        public static TSource First<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            IsNull(source);
            IsNull(predicate);

            foreach (var item in source)
            {
                if (predicate(item))
                {
                    return item;
                }
            }

            throw new InvalidOperationException("No element with the required condition was found");
        }

        public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            IsNull(source);
            IsNull(selector);

            foreach (var item in source)
            {
                yield return selector(item);
            }
        }
        
        public static IEnumerable<TResult> SelectMany<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TResult>> selector)
        {
            IsNull(source);
            IsNull(selector);

            foreach (var item in source)
            {
                foreach (var inner in selector(item))
                {
                    yield return inner;
                }
            }
        }

        public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            IsNull(source);
            IsNull(predicate);

            foreach (var item in source)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }

        public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector) where TKey : notnull
        {
            IsNull(source);
            IsNull(keySelector);
            IsNull(elementSelector);

            Dictionary<TKey, TElement> dictionary = new Dictionary<TKey, TElement>();

            foreach (var item in source)
            {
                dictionary[keySelector(item)] = elementSelector(item);
            }

            return dictionary;
        }

        public static IEnumerable<TResult> Zip<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector)
        {
            IsNull(first);
            IsNull(second);
            IsNull(resultSelector);

            var firstEnum = first.GetEnumerator();
            var secondEnum = second.GetEnumerator();

            while (firstEnum.MoveNext() && secondEnum.MoveNext())
            {
                yield return (resultSelector(firstEnum.Current, secondEnum.Current));
            }
        }

        public static TAccumulate Aggregate<TSource, TAccumulate>(this IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func)
        {
            IsNull(source);
            IsNull(seed);
            IsNull(func);

            TAccumulate value = seed;
            
            foreach(var item in source)
            {
                value = func(value, item);
            }

            return value;
        }

        private static void IsNull<TSource>(TSource param, [CallerArgumentExpression(nameof(param))] string paramName = "")
        {
            if (param == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }
    }
}