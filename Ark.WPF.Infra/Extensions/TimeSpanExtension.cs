using System.Windows;

namespace System
{
    /// <summary>
    /// <see cref="TimeSpan"/> 構造体の拡張メソッドを定義します。
    /// </summary>
    public static class TimeSpanExtension
    {
        /// <summary>
        /// <see cref="Duration"/> に変換します。
        /// </summary>
        /// <param name="timeSpan">時間間隔</param>
        /// <returns><see cref="Duration"/></returns>
        public static Duration ToDuration(this TimeSpan timeSpan)
        {
            return new Duration(timeSpan);
        }
    }
}