﻿using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
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

        private static void IsNull<TSource>(TSource param, [CallerArgumentExpression(nameof(param))] string paramName = "")
        {
            if (param == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }
    }
}