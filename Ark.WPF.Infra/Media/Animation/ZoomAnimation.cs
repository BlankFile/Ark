using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Ark.WPF.Infra.Media.Animation
{
    /// <summary>
    /// <see cref="Duration"/> で指定した期間にわたって、ScaleTransform の値を 2 つのターゲット値の間で線形補間を使用してアニメーション化します。
    /// </summary>
    public class ZoomAnimation : DoubleAnimation
    {
        #region [Member]

        private string _transformPath;

        #endregion

        #region [Constructor]

        /// <summary>
        /// <see cref="ZoomAnimation"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public ZoomAnimation() : base() { }

        /// <summary>
        /// <see cref="ZoomAnimation"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="toValue">終了値</param>
        /// <param name="duration">期間</param>
        public ZoomAnimation(double toValue, Duration duration) : base(toValue, duration) { }

        /// <summary>
        /// <see cref="ZoomAnimation"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="toValue">終了値</param>
        /// <param name="duration">期間</param>
        /// <param name="fillBehavior"><see cref="Timeline"/> の動作</param>
        public ZoomAnimation(double toValue, Duration duration, FillBehavior fillBehavior) : base(toValue, duration, fillBehavior) { }

        /// <summary>
        /// <see cref="ZoomAnimation"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="fromValue">開始値</param>
        /// <param name="toValue">終了値</param>
        /// <param name="duration">期間</param>
        public ZoomAnimation(double fromValue, double toValue, Duration duration) : base(fromValue, toValue, duration) { }

        /// <summary>
        /// <see cref="ZoomAnimation"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="fromValue">開始値</param>
        /// <param name="toValue">終了値</param>
        /// <param name="duration">期間</param>
        /// <param name="fillBehavior"><see cref="Timeline"/> の動作</param>
        public ZoomAnimation(double fromValue, double toValue, Duration duration, FillBehavior fillBehavior) : base(fromValue, toValue, duration, fillBehavior) { }

        #endregion

        #region [Method] public

        /// <summary>
        /// 対象のコントロールから ScaleTransform のプロパティパスを初期化します。
        /// </summary>
        /// <param name="target"></param>
        public void InitTransform(FrameworkElement target)
        {
            _transformPath = "(RenderTransform)";

            if (target.RenderTransform is TransformGroup)
            {
                var group = target.RenderTransform as TransformGroup;
                var index = group.Children.IndexOf(x => x is ScaleTransform);

                _transformPath = _transformPath + $".(TransformGroup.Children)[{index}]";
            }
        }

        /// <summary>
        /// <see cref="Storyboard"/> を作成します。
        /// </summary>
        /// <returns><see cref="Storyboard"/></returns>
        public Storyboard MakeStoryboard()
        {
            var scaleXAnimation = Clone();
            var scaleYAnimation = Clone();

            Storyboard.SetTargetProperty(scaleXAnimation, new PropertyPath(_transformPath + ".(ScaleTransform.ScaleX)"));
            Storyboard.SetTargetProperty(scaleYAnimation, new PropertyPath(_transformPath + ".(ScaleTransform.ScaleY)"));

            var storyboard = new Storyboard();

            storyboard.Children.Add(scaleXAnimation);
            storyboard.Children.Add(scaleYAnimation);

            return storyboard;
        }

        #endregion
    }
}