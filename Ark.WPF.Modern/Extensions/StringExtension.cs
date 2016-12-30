using System.Windows.Media;

namespace System
{
    /// <summary>
    /// <see cref="string"/> クラスの拡張メソッドを定義します。
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// カラーに変換します。
        /// </summary>
        /// <param name="code">色コード文字列</param>
        /// <returns>カラー</returns>
        public static Color ToColor(this string code)
        {
            return (Color)ColorConverter.ConvertFromString(code);
        }

        /// <summary>
        /// ブラシに変換します。
        /// </summary>
        /// <param name="code">色コード文字列</param>
        /// <returns>ブラシ</returns>
        public static SolidColorBrush ToBrush(this string code)
        {
            return new SolidColorBrush(ToColor(code));
        }
    }
}
