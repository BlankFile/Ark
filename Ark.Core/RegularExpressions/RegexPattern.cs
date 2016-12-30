namespace Ark.Core.RegularExpressions
{
    /// <summary>
    /// 正規表現のパターンを定義します。
    /// </summary>
    public static class RegexPattern
    {
        /// <summary>
        /// アルファベットのみ
        /// </summary>
        public const string AlphabetOnly = "[^a-zA-Z]";

        /// <summary>
        /// 数字のみ
        /// </summary>
        public const string NumberOnly = "[^0-9]";

        /// <summary>
        /// アルファベットと数字のみ
        /// </summary>
        public const string AlphabetNumberOnly = "[^a-zA-Z0-9]";

        /// <summary>
        /// フォルダーパス
        /// </summary>
        public const string FolderPath = "^([a-zA-Z]:)?(\\\\[^<>:\"/\\\\|?*]+)+\\\\?$";
    }
}