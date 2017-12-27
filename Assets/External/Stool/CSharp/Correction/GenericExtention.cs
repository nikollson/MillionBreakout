using System;
using System.Collections.Generic;

namespace Stool.CSharp
{
    static class GenericExtention
    {
        public static void RemoveNodeFromHashSet<T>(this LinkedList<T> list, HashSet<T> hashSet)
        {
            var current = list.First;
            while (current != null)
            {
                var next = current.Next;
                if (hashSet.Contains(current.Value))
                {
                    list.Remove(current);
                }
                current = next;
            }
        }

        public static void RemoveNodeIf<T>(this LinkedList<T> list, Func<T, bool> judgeFunc, Action<T> callBackFunc = null)
        {
            var current = list.First;
            while (current != null)
            {
                var next = current.Next;
                if (judgeFunc(current.Value))
                {
                    callBackFunc(current.Value);
                    list.Remove(current);
                }
                current = next;
            }
        }

        public static void AddUpdate<TKey, TValue>(this Dictionary<TKey, TValue> self, TKey key, TValue value)
        {
            if (self.ContainsKey(key))
            {
                self[key] = value;
            }
            else
            {
                self.Add(key, value);
            }
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> callBack)
        {
            foreach (var element in enumerable)
            {
                callBack(element);
            }
        }

        public static List<T[]> SplitByLength<T>(this T[] array, int length)
        {
            var ret = new List<T[]>();
            for (int i = 0; i < array.Length; i += length)
            {
                int n = Math.Min(length, array.Length - i);
                var split = new T[n];
                for (int j = 0; j < n; j++)
                {
                    split[j] = array[i + j];
                }
                ret.Add(split);
            }
            return ret;
        }

        public static void Fill<T>(this T[] a, T value)
        {
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = value;
            }
        }

        public static void Fill<T>(this List<T> a, T value)
        {
            for (int i = 0; i < a.Count; i++)
            {
                a[i] = value;
            }
        }
    }
}

