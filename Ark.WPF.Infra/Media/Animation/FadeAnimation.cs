using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Ark.WPF.Infra.Media.Animation
{
    /// <summary>
    /// <see cref="Duration"/> で指定した期間にわたって、Opacity プロパティの値を 2 つのターゲット値の間で線形補間を使用してアニメーション化します。
    /// </summary>
    public class FadeAnimation : DoubleAnimation
    {
        #region [Constructor]

        /// <summary>
        /// <see cref="FadeAnimation"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public FadeAnimation() : base() { Initialize(); }

        /// <summary>
        /// <see cref="FadeAnimation"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="toValue">終了値</param>
        /// <param name="duration">期間</param>
        public FadeAnimation(double toValue, Duration duration) : base(toValue, duration) { Initialize(); }

        /// <summary>
        /// <see cref="FadeAnimation"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="toValue">終了値</param>
        /// <param name="duration">期間</param>
        /// <param name="fillBehavior"><see cref="Timeline"/> の動作</param>
        public FadeAnimation(double toValue, Duration duration, FillBehavior fillBehavior) : base(toValue, duration, fillBehavior) { Initialize(); }

        /// <summary>
        /// <see cref="FadeAnimation"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="fromValue">開始値</param>
        /// <param name="toValue">終了値</param>
        /// <param name="duration">期間</param>
        public FadeAnimation(double fromValue, double toValue, Duration duration) : base(fromValue, toValue, duration) { Initialize(); }

        /// <summary>
        /// <see cref="FadeAnimation"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="fromValue">開始値</param>
        /// <param name="toValue">終了値</param>
        /// <param name="duration">期間</param>
        /// <param name="fillBehavior"><see cref="Timeline"/> の動作</param>
        public FadeAnimation(double fromValue, double toValue, Duration duration, FillBehavior fillBehavior) : base(fromValue, toValue, duration, fillBehavior) { Initialize(); }

        #endregion

        /// <summary>
        /// Opacity プロパティをアニメーションの対象に設定します。
        /// </summary>
        private void Initialize()
        {
            Storyboard.SetTargetProperty(this, new PropertyPath(nameof(FrameworkElement.Opacity)));
        }

    }
}
