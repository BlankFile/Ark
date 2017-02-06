using System.Threading.Tasks;

namespace System.Windows.Media.Animation
{
    /// <summary>
    /// <see cref="Storyboard"/> クラスの拡張メソッドを定義します。
    /// </summary>
    public static class StoryboardExtensions
    {
        /// <summary>
        /// アニメーションを非同期で開始します。
        /// </summary>
        /// <param name="storyboard">対象の <see cref="Storyboard"/></param>
        /// <param name="target">アニメーションを適用する対象</param>
        /// <returns>アニメーションが実行されたかどうか</returns>
        public static async Task<bool> BeginAsync(this Storyboard storyboard, FrameworkElement target)
        {
            var tcs = new TaskCompletionSource<bool>();

            if (storyboard != null && target != null)
            {
                var temp = target.CacheMode;
                var cache = new BitmapCache();

                target.CacheMode = cache;

                var animation = storyboard.Clone();

                animation.Completed += (s, e) =>
                {
                    target.CacheMode = temp;

                    tcs.SetResult(true);
                };

                animation.Freeze();
                animation.Begin(target);
            }
            else
            {
                tcs.SetResult(false);
            }

            return await tcs.Task;
        }
    }
}