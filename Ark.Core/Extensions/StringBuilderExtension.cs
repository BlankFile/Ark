namespace System.Text
{
    /// <summary>
    /// <see cref="StringBuilder"/> クラスの拡張メソッドを定義します。
    /// </summary>
    public static class StringBuilderExtension
    {
        /// <summary>
        /// ビルダーの末尾に、指定した文字列のコピーと空白文字を追加します。
        /// </summary>
        /// <param name="builder">ビルダー</param>
        /// <param name="value">追加するオブジェクト</param>
        /// <returns>ビルダー</returns>
        public static StringBuilder AppendSpace(this StringBuilder builder, object value)
        {
            builder.Append(value);
            builder.Append(" ");

            return builder;
        }
    }
}