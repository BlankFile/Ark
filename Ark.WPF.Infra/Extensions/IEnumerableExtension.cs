using System.Collections.Generic;
using Ark.WPF.Infra.Collections;

namespace System.Collections.Generic
{
    /// <summary>
    /// <see cref="IEnumerable"/> インターフェースの拡張メソッドを定義します。
    /// </summary>
    public static class IEnumerableExtension
    {
        #region 変換

        /// <summary>
        /// コレクションを動的なスレッドセーフコレクションに変換します。
        /// </summary>
        /// <typeparam name="T">コレクションの型</typeparam>
        /// <param name="collection">コレクション</param>
        /// <returns>動的なスレッドセーフコレクション</returns>
        public static ConcurrencyCollection<T> ToConcurrency<T>(this IEnumerable<T> collection)
        {
            return new ConcurrencyCollection<T>(collection);
        }

        #endregion
    }
}
