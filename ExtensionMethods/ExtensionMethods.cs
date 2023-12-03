using System;
using System.Collections;

namespace ExtensionMethods
{
    public static class ExtensionMethods
    {
        public static bool All<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            HasNullParams(source, predicate);

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
            HasNullParams(source, predicate);

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
            HasNullParams(source, predicate);

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
            HasNullParams(source, selector);

            foreach (var item in source)
            {
                yield return selector(item);
            }
        }

        private static void HasNullParams<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, TResult> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException($"The {source} collection is null");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException($"The {predicate} function is null");
            }
        }
    }
}