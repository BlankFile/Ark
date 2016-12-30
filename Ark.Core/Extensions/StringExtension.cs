using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace System
{
    /// <summary>
    /// <see cref="string"/> クラスの拡張メソッドを定義します。
    /// </summary>
    public static class StringExtension
    {
        #region 判断

        /// <summary>
        /// 文字列が null または <see cref="string.Empty"/> 文字列であるかどうかを示します。
        /// </summary>
        /// <param name="value">テストする文字列</param>
        /// <returns>
        /// True ：文字列が null または空の文字列
        /// False：それ以外
        /// </returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// 文字列が null または空であるか、空白文字だけで構成されているかどうかを示します。
        /// </summary>
        /// <param name="value">テストする文字列</param>
        /// <returns>
        /// True ：文字列が null または空の文字列または空白文字だけで構成されている場合
        /// False：それ以外
        /// </returns>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        #endregion 判断

        #region 置換

        /// <summary>
        /// 文字列の書式項目を、指定した配列内の対応するオブジェクトの文字列形式に置換します。
        /// </summary>
        /// <param name="value">複合書式指定文字列</param>
        /// <param name="args">0 個以上の書式設定対象オブジェクトを含んだオブジェクト配列</param>
        /// <returns>書式項目が args の対応するオブジェクトの文字列形式に置換された value のコピー</returns>
        public static string Put(this string value, params object[] args)
        {
            return string.Format(value, args);
        }

        #endregion 置換

        #region 整形

        /// <summary>
        /// 文字列から、ファイル名に使用出来ない文字を削除します。
        /// </summary>
        /// <param name="value">文字列</param>
        /// <returns>ファイル名に使用出来ない文字を削除した <paramref name="value"/> のコピー</returns>
        public static string TrimFileName(this string value)
        {
            var invalidChars = Path.GetInvalidFileNameChars().ToHashSet();

            return value.Where(x => !invalidChars.Contains(x)).Join();
        }

        /// <summary>
        /// 文字列から、パスに使用出来ない文字を削除します。
        /// </summary>
        /// <param name="value">文字列</param>
        /// <returns>パスに使用出来ない文字を削除した <paramref name="value"/> のコピー</returns>
        public static string TrimPath(this string value)
        {
            var invalidChars = Path.GetInvalidPathChars().ToHashSet();

            return value.Where(x => !invalidChars.Contains(x)).Join();
        }

        /// <summary>
        /// 文字列から、指定した文字エンコーディングでエンコード出来ない文字を削除します。
        /// </summary>
        /// <param name="value">文字列</param>
        /// <param name="enc">文字エンコーディング</param>
        /// <returns>エンコード出来ない文字を削除した <paramref name="value"/> のコピー</returns>
        public static string TrimEncoding(this string value, Encoding enc)
        {
            return value.Where(x => x.CanEncoding(enc)).Join();
        }

        /// <summary>
        /// 文字が、指定した文字エンコーディングでエンコード可能かどうか判断します。
        /// </summary>
        /// <param name="c">テストする文字</param>
        /// <param name="enc">文字エンコーディング</param>
        /// <returns>エンコードの可否</returns>
        private static bool CanEncoding(this char c, Encoding enc)
        {
            var charString = c.ToString();
            var bytes = enc.GetBytes(charString);
            var encString = enc.GetString(bytes);

            return charString == encString;
        }

        /// <summary>
        /// 文字のコレクションを連結します。
        /// </summary>
        /// <param name="chars">文字のコレクション</param>
        /// <returns>文字列</returns>
        private static string Join(this IEnumerable<char> chars)
        {
            return string.Concat(chars);
        }

        #endregion 整形
    }
}