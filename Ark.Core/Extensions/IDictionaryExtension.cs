namespace System.Collections.Generic
{
    /// <summary>
    /// <see cref="IDictionary"/> インターフェースの拡張メソッドを定義します。
    /// </summary>
    public static class IDictionaryExtension
    {
        #region 変更

        /// <summary>
        /// コレクションに複数の項目を追加します。
        /// </summary>
        /// <typeparam name="T1">コレクションのキーの型</typeparam>
        /// <typeparam name="T2">コレクションの値の型</typeparam>
        /// <param name="collection">コレクション</param>
        /// <param name="dictionary">追加するコレクション</param>
        public static void AddRange<T1, T2>(this IDictionary<T1, T2> collection, IEnumerable<KeyValuePair<T1, T2>> dictionary)
        {
            foreach (var item in dictionary)
            {
                collection.Add(item);
            }
        }

        #endregion

        #region 取得

        /// <summary>
        /// 指定したキーに関連付けられている値を取得します。
        /// </summary>
        /// <typeparam name="T1">コレクションのキーの型</typeparam>
        /// <typeparam name="T2">コレクションの値の型</typeparam>
        /// <param name="collection">コレクション</param>
        /// <param name="key">キー</param>
        /// <param name="defaultValue">キーが見つからなかった場合に返す値</param>
        /// <returns>指定したキーに関連付けられている値、もしくは<paramref name="defaultValue"/></returns>
        public static T2 GetOrDefault<T1, T2>(this IDictionary<T1, T2> collection, T1 key, T2 defaultValue = default(T2))
        {
            var value = defaultValue;

            collection.TryGetValue(key, out value);

            return value;
        }

        #endregion
    }
}