namespace System.Windows.Media
{
    /// <summary>
    /// <see cref="Color"/> 構造体の拡張メソッドを定義します。
    /// </summary>
    public static class ColorExtension
    {
        /// <summary>
        /// ブラシに変換します。
        /// </summary>
        /// <param name="color">色</param>
        /// <returns>ブラシ</returns>
        public static SolidColorBrush ToBrush(this Color color)
        {
            return new SolidColorBrush(color);
        }
    }
}