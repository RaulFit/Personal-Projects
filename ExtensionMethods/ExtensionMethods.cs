using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

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
            
            foreach (var item in source)
            {
                value = func(value, item);
            }

            return value;
        }

        public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector,
        Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector)
        {
            IsNull(outer);
            IsNull(inner);
            IsNull(outerKeySelector);
            IsNull(innerKeySelector);
            IsNull(resultSelector);

            foreach (var outerItem in outer)
            {
                foreach (var innerItem in inner)
                {
                    if (Equals(outerKeySelector(outerItem), innerKeySelector(innerItem)))
                    {
                        yield return resultSelector(outerItem, innerItem);
                    }
                }
            }
        }

        public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
        {
            IsNull(source);
            IsNull(comparer);

            HashSet<TSource> distinctItems= new HashSet<TSource>(comparer);

            foreach (var item in source)
            {
                if (distinctItems.Add(item))
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<TSource> Union<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
        {
            IsNull(first);
            IsNull(second);
            IsNull(comparer);

            HashSet<TSource> union = new HashSet<TSource>(comparer);

            foreach (var item in first)
            {
                union.Add(item);
                yield return item;
            }

            foreach (var item in second)
            {
                if (union.Add(item))
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<TSource> Intersect<TSource>(this IEnumerable<TSource> first,IEnumerable<TSource> second,IEqualityComparer<TSource> comparer)
        {
            IsNull(first);
            IsNull(second);
            IsNull(comparer);

            HashSet<TSource> union = new HashSet<TSource>(second, comparer);

            foreach (var item in first)
            {
                if (union.Contains(item))
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
        {
            IsNull(first);
            IsNull(second);
            IsNull(comparer);

            HashSet<TSource> union = new HashSet<TSource>(second, comparer);

            foreach (var item in first)
            {
                if (!union.Contains(item))
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<TResult> GroupBy<TSource, TKey, TElement, TResult>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector,
        Func<TKey, IEnumerable<TElement>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
        {
            IsNull(source);
            IsNull(keySelector);
            IsNull(elementSelector);
            IsNull(resultSelector);
            IsNull(comparer);

            Dictionary<TKey, List<TElement>> dict = new Dictionary<TKey, List<TElement>>(comparer);

            foreach (var item in source)
            {
                TKey key = keySelector(item);
                if (!dict.TryGetValue(key, out List<TElement>? list))
                {
                    dict.Add(key, list = new List<TElement>());
                }
                list.Add(elementSelector(item));
            }
            
            foreach (var item in dict.Keys)
            {
                yield return resultSelector(item, dict[item]);
            }
        }

        public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            IsNull(source);
            IsNull(keySelector);
            IsNull(comparer);

            var generalComparer = new GeneralComparer<TSource>((TSource first, TSource second) => comparer.Compare(keySelector(first), keySelector(second)));
            return new OrderedEnumerable<TSource>(source, generalComparer);
        }

        public static IOrderedEnumerable<TSource> ThenBy<TSource, TKey>(this IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            IsNull(source);
            IsNull(keySelector);
            IsNull(comparer);

            return source.CreateOrderedEnumerable(keySelector, comparer, false);
        }

        internal class OrderedEnumerable<TSource> : IOrderedEnumerable<TSource>
        {
            private readonly IEnumerable<TSource> source;
            private readonly IComparer<TSource> comparer;

            internal OrderedEnumerable(IEnumerable<TSource> source, IComparer<TSource> comparer)
            {
                this.source = source;
                this.comparer = comparer;
            }

            public IOrderedEnumerable<TSource> CreateOrderedEnumerable<TKey>(Func<TSource, TKey> keySelector, IComparer<TKey>? comparer, bool descending)
            {
                IsNull(keySelector);

                Func<TSource, TSource, int> compare = (TSource first, TSource second) =>
                this.comparer.Compare(first, second) != 0 || comparer == null ?
                this.comparer.Compare(first, second) :
                comparer.Compare(keySelector(first), keySelector(second));

                return new OrderedEnumerable<TSource>(source, new GeneralComparer<TSource>(compare));
            }

            public IEnumerator<TSource> GetEnumerator()
            {
                var elements = source.ToList();
                QuickSort(elements, 0, elements.Count - 1);

                foreach (var elem in elements)
                {
                    yield return elem;
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            private void QuickSort(List<TSource> array, int leftIndex, int rightIndex)
            {
                if (array.Count == 0)
                {
                    return;
                }

                var stack = new Stack<(int left, int right)>();
                stack.Push((leftIndex, rightIndex));

                while (stack.Count > 0)
                {
                    var (left, right) = stack.Pop();
                    int i = left;
                    int j = right;
                    var pivot = array[left];

                    while (i <= j)
                    {
                        while (comparer.Compare(array[i], pivot) < 0)
                        {
                            i++;
                        }

                        while (comparer.Compare(array[j], pivot) > 0)
                        {
                            j--;
                        }

                        if (i <= j)
                        {
                            (array[i], array[j]) = (array[j], array[i]);
                            i++;
                            j--;
                        }
                    }

                    if (left < j)
                    {
                        stack.Push((left, j));
                    }

                    if (i < right)
                    {
                        stack.Push((i, right));
                    }
                }
            }
        }

        internal class GeneralComparer<TSource> : IComparer<TSource>
        {
            Func<TSource, TSource, int> compare;

            internal GeneralComparer(Func<TSource, TSource, int> compare)
            {
                this.compare = compare;
            }

            public int Compare(TSource first, TSource second)
            {
                return compare(first, second);
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