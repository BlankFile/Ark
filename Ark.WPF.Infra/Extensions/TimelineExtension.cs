
using System.Windows.Media.Animation;

namespace System.Windows.Media.Animation
{
    /// <summary>
    /// <see cref="Timeline"/> クラスの拡張メソッドを定義します。
    /// </summary>
    public static class TimelineExtension
    {
        /// <summary>
        /// <see cref="Storyboard"/> に変換します。
        /// </summary>
        /// <param name="timeline">時間のセグメント</param>
        /// <returns>タイムラインコンテナ</returns>
        public static Storyboard ToStoryboard(this Timeline timeline)
        {
            var storyboard = new Storyboard();

            storyboard.Children.Add(timeline);

            return storyboard;
        }
    }
}
