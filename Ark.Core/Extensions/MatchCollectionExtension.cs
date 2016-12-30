using System.Collections.Generic;
using System.Linq;

namespace System.Text.RegularExpressions
{
    /// <summary>
    /// <see cref="MatchCollection"/> クラスの拡張メソッドを定義します。
    /// </summary>
    public static class MatchCollectionExtension
    {
        /// <summary>
        /// マッチのリストに変換します。
        /// </summary>
        /// <param name="collection">MatchCollection</param>
        /// <returns>リスト</returns>
        public static List<Match> ToList(this MatchCollection collection)
        {
            return collection.OfType<Match>().ToList();
        }

        /// <summary>
        /// マッチ文字列のリストに変換します。
        /// </summary>
        /// <param name="collection">MatchCollection</param>
        /// <returns>リスト</returns>
        public static List<string> ToValueList(this MatchCollection collection)
        {
            return collection.OfType<Match>().Select(x => x.Value).ToList();
        }

    }
}
