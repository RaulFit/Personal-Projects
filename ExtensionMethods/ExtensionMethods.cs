using System.Collections;

namespace ExtensionMethods
{
    public static class ExtensionMethods
    {
        public static bool All<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            CollectionIsNull(source);
            FunctionIsNull(predicate);

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
            CollectionIsNull(source);
            FunctionIsNull(predicate);

            foreach (var item in source)
            {
                if (predicate(item))
                {
                    return true;
                }
            }

            return false;
        }

        private static void CollectionIsNull<TSource>(IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("The collection is null");
            }
        }

        private static void FunctionIsNull<TSource>(Func<TSource, bool> function)
        {
            if (function == null)
            {
                throw new NullReferenceException("The function is null");
            }
        }
    }
}