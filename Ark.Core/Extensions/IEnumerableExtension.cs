using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace System.Collections.Generic
{
    /// <summary>
    /// <see cref="IEnumerable"/> インターフェースの拡張メソッドを定義します。
    /// </summary>
    public static class IEnumerableExtension
    {
        #region 判断

        /// <summary>
        /// コレクションが null または空であるかどうかを判断します。
        /// </summary>
        /// <typeparam name="T">コレクションの型</typeparam>
        /// <param name="collection">コレクション</param>
        /// <returns>
        /// True ：null または空
        /// False：null または空でない
        /// </returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            if (collection == null)
            {
                return true;
            }

            var enumerator = collection.GetEnumerator();

            return !enumerator.MoveNext();
        }

        /// <summary>
        /// コレクションの要素が1つかどうか判断します。
        /// </summary>
        /// <typeparam name="T">コレクションの型</typeparam>
        /// <param name="collection">コレクション</param>
        /// <returns>
        /// True ：要素が1つ
        /// False：要素がないもしくは2つ以上
        /// </returns>
        public static bool IsSingle<T>(this IEnumerable<T> collection)
        {
            var enumerator = collection.GetEnumerator();

            return enumerator.MoveNext() && !enumerator.MoveNext();
        }

        #endregion

        #region 反復

        /// <summary>
        /// コレクションの各要素に対して、指定した処理を実行します。
        /// </summary>
        /// <typeparam name="T">コレクションの型</typeparam>
        /// <param name="collection">コレクション</param>
        /// <param name="action">各要素に対する処理</param>
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }
        }

        /// <summary>
        /// コレクションの各要素に対して、指定した処理を実行します。
        /// <para>処理の戻り値がFalseの場合、後続の処理を中止します。</para>
        /// </summary>
        /// <typeparam name="T">コレクションの型</typeparam>
        /// <param name="collection">コレクション</param>
        /// <param name="func">各要素に対する処理</param>
        public static void ForEach<T>(this IEnumerable<T> collection, Func<T, bool> func)
        {
            foreach (var item in collection)
            {
                if (!func(item))
                {
                    break;
                }
            }
        }

        /// <summary>
        /// コレクションの各要素に対して、インデックス付きの指定した処理を実行します。
        /// </summary>
        /// <typeparam name="T">コレクションの型</typeparam>
        /// <param name="collection">コレクション</param>
        /// <param name="action">各要素に対する処理</param>
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T, int> action)
        {
            var count = 0;

            foreach (var item in collection)
            {
                action(item, count);

                count++;
            }
        }

        /// <summary>
        /// コレクションの各要素に対して、指定した処理を遅延実行します。
        /// </summary>
        /// <typeparam name="T">コレクションの型</typeparam>
        /// <param name="collection">コレクション</param>
        /// <param name="action">各要素に対する処理</param>
        /// <returns>コレクション</returns>
        public static IEnumerable<T> Do<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
            {
                action(item);

                yield return item;
            }
        }

        /// <summary>
        /// コレクションの各要素に対して、指定した処理を遅延実行します。
        /// <para>処理の戻り値がFalseの場合、後続の処理を中止します。</para>
        /// </summary>
        /// <typeparam name="T">コレクションの型</typeparam>
        /// <param name="collection">コレクション</param>
        /// <param name="func">各要素に対する処理</param>
        /// <returns>コレクション</returns>
        public static IEnumerable<T> Do<T>(this IEnumerable<T> collection, Func<T, bool> func)
        {
            foreach (var item in collection)
            {
                if (!func(item))
                {
                    yield break;
                }

                yield return item;
            }
        }

        /// <summary>
        /// コレクションの各要素に対して、インデックス付きの指定した処理を遅延実行します。
        /// </summary>
        /// <typeparam name="T">コレクションの型</typeparam>
        /// <param name="collection">コレクション</param>
        /// <param name="action">各要素に対する処理</param>
        /// <returns>コレクション</returns>
        public static IEnumerable<T> Do<T>(this IEnumerable<T> collection, Action<T, int> action)
        {
            var count = 0;

            foreach (var item in collection)
            {
                action(item, count);

                count++;

                yield return item;
            }
        }

        /// <summary>
        /// 2つのコレクションの各要素に対して、指定した処理を実行します。
        /// </summary>
        /// <typeparam name="T1">1つ目のコレクションの型</typeparam>
        /// <typeparam name="T2">2つ目のコレクションの型</typeparam>
        /// <param name="collection1">1つ目のコレクション</param>
        /// <param name="collection2">2つ目のコレクション</param>
        /// <param name="action">各要素に対する処理</param>
        public static void Double<T1, T2>(this IEnumerable<T1> collection1, IEnumerable<T2> collection2, Action<T1, T2> action)
        {
            if (collection1 == null) return;
            if (collection2 == null) return;

            var list1 = collection1.ToList();
            var list2 = collection2.ToList();

            var maxCount = Math.Max(list1.Count, list2.Count);

            for (int i = 0; i < maxCount; i++)
            {
                var element1 = list1.ElementAtOrDefault(i);
                var element2 = list2.ElementAtOrDefault(i);

                action(element1, element2);
            }
        }

        /// <summary>
        /// 2つのコレクションの各要素に対して、指定した処理を実行します。
        /// <para>処理の戻り値がFalseの場合、後続の処理を中止します。</para>
        /// </summary>
        /// <typeparam name="T1">1つ目のコレクションの型</typeparam>
        /// <typeparam name="T2">2つ目のコレクションの型</typeparam>
        /// <param name="collection1">1つ目のコレクション</param>
        /// <param name="collection2">2つ目のコレクション</param>
        /// <param name="func">各要素に対する処理</param>
        public static void Double<T1, T2>(this IEnumerable<T1> collection1, IEnumerable<T2> collection2, Func<T1, T2, bool> func)
        {
            if (collection1 == null) return;
            if (collection2 == null) return;

            var list1 = collection1.ToList();
            var list2 = collection2.ToList();

            var maxCount = Math.Max(list1.Count, list2.Count);

            for (int i = 0; i < maxCount; i++)
            {
                var element1 = list1.ElementAtOrDefault(i);
                var element2 = list2.ElementAtOrDefault(i);

                var isContinue = func(element1, element2);

                if (!isContinue)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// 2つのコレクションの各要素に対して、インデックス付きの指定した処理を実行します。
        /// </summary>
        /// <typeparam name="T1">1つ目のコレクションの型</typeparam>
        /// <typeparam name="T2">2つ目のコレクションの型</typeparam>
        /// <param name="collection1">1つ目のコレクション</param>
        /// <param name="collection2">2つ目のコレクション</param>
        /// <param name="action">各要素に対する処理</param>
        public static void Double<T1, T2>(this IEnumerable<T1> collection1, IEnumerable<T2> collection2, Action<T1, T2, int> action)
        {
            if (collection1 == null) return;
            if (collection2 == null) return;

            var list1 = collection1.ToList();
            var list2 = collection2.ToList();

            var maxCount = Math.Max(list1.Count, list2.Count);

            for (int i = 0; i < maxCount; i++)
            {
                var element1 = list1.ElementAtOrDefault(i);
                var element2 = list2.ElementAtOrDefault(i);

                action(element1, element2, i);
            }
        }

        /// <summary>
        /// 2つのコレクションの各要素に対して、インデックス付きの指定した処理を実行します。
        /// <para>処理の戻り値がFalseの場合、後続の処理を中止します。</para>
        /// </summary>
        /// <typeparam name="T1">1つ目のコレクションの型</typeparam>
        /// <typeparam name="T2">2つ目のコレクションの型</typeparam>
        /// <param name="collection1">1つ目のコレクション</param>
        /// <param name="collection2">2つ目のコレクション</param>
        /// <param name="func">各要素に対する処理</param>
        public static void Double<T1, T2>(this IEnumerable<T1> collection1, IEnumerable<T2> collection2, Func<T1, T2, int, bool> func)
        {
            if (collection1 == null) return;
            if (collection2 == null) return;

            var list1 = collection1.ToList();
            var list2 = collection2.ToList();

            var maxCount = Math.Max(list1.Count, list2.Count);

            for (int i = 0; i < maxCount; i++)
            {
                var element1 = list1.ElementAtOrDefault(i);
                var element2 = list2.ElementAtOrDefault(i);

                var isContinue = func(element1, element2, i);

                if (!isContinue)
                {
                    break;
                }
            }
        }

        #endregion

        #region 索引

        /// <summary>
        /// 指定した条件に合致する要素の位置を返します。
        /// <para>見つからなかった場合は null を返します。</para>
        /// </summary>
        /// <typeparam name="T">コレクションの型</typeparam>
        /// <param name="collection">コレクション</param>
        /// <param name="condition">要素の検索条件</param>
        /// <returns>コレクション内の要素の位置</returns>
        public static int? IndexOf<T>(this IEnumerable<T> collection, Func<T, bool> condition)
        {
            var count = 0;

            foreach (var item in collection)
            {
                if (condition(item))
                {
                    return count;
                }

                count++;
            }

            return null;
        }

        #endregion

        #region 変換

        /// <summary>
        /// コレクションをSelectした後、リストに変換します。
        /// </summary>
        /// <typeparam name="T1">コレクションの型</typeparam>
        /// <typeparam name="T2">変換後の型</typeparam>
        /// <param name="collection">コレクション</param>
        /// <param name="func">変換関数</param>
        /// <returns>リスト</returns>
        public static List<T2> SelectToList<T1, T2>(this IEnumerable<T1> collection, Func<T1, T2> func)
        {
            return collection.Select(func).ToList();
        }

        /// <summary>
        /// コレクションを動的なコレクションに変換します。
        /// </summary>
        /// <typeparam name="T">コレクションの型</typeparam>
        /// <param name="collection">コレクション</param>
        /// <returns>動的なコレクション</returns>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> collection)
        {
            return new ObservableCollection<T>(collection);
        }

        /// <summary>
        /// コレクションをハッシュセットに変換します。
        /// </summary>
        /// <typeparam name="T">コレクションの型</typeparam>
        /// <param name="collection">コレクション</param>
        /// <returns>ハッシュセット</returns>
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> collection)
        {
            return new HashSet<T>(collection);
        }

        #endregion

        #region 非同期

        /// <summary>
        /// コレクションの各要素を非同期で変換します。
        /// </summary>
        /// <typeparam name="T1">コレクションの型</typeparam>
        /// <typeparam name="T2">変換後の型</typeparam>
        /// <param name="collection">コレクション</param>
        /// <param name="func">変換関数</param>
        /// <returns>待機可能な変換後のコレクション</returns>
        public static async Task<T2[]> SelectAsync<T1, T2>(this IEnumerable<T1> collection, Func<T1, T2> func)
        {
            var tasks = collection.Select(x => Task.Run(() => func(x)));

            return await Task.WhenAll(collection.Select(x => Task.Run(() => func(x))));

        }

        /// <summary>
        /// コレクションの各要素を、指定した個数ずつ非同期で変換します。
        /// </summary>
        /// <typeparam name="T1">コレクションの型</typeparam>
        /// <typeparam name="T2">変換後の型</typeparam>
        /// <param name="collection">コレクション</param>
        /// <param name="func">変換関数</param>
        /// <param name="onceCount">1度に変換する個数</param>
        /// <returns>待機可能な変換後のコレクション</returns>
        public static async Task<T2[]> SelectAsync<T1, T2>(this IEnumerable<T1> collection, Func<T1, T2> func, int onceCount)
        {
            var array = collection.ToArray();
            var buffer = (array.Length / onceCount);
            var buffers = Enumerable.Range(0, (buffer == 0) ? 1 : buffer);

            return await Task.Run(() =>
            {
                return buffers.SelectMany((i) =>
                {
                    return array.Skip(i * onceCount)
                               .Take(onceCount)
                               .SelectAsync(func)
                               .Result;
                })
                .ToArray();
            });
        }

        #endregion
    }
}
