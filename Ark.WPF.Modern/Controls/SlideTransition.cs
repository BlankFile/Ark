using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Ark.WPF.Modern.Controls
{
    /// <summary>
    /// スライドで画面を遷移するコントロールを表します。
    /// </summary>
    public class SlideTransition : ItemsControl
    {
        #region [Member]

        private SlideTransitionCore _core;

        #endregion

        #region [Property] Dependency

        /// <summary>
        /// <see cref="Duration"/> 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty DurationProperty = DependencyProperty.Register(
            nameof(Duration), typeof(TimeSpan), typeof(SlideTransition), new PropertyMetadata(TimeSpan.FromMilliseconds(1000)));

        /// <summary>
        /// スライドアニメーションの期間を取得または設定します。
        /// </summary>
        public TimeSpan Duration { get { return (TimeSpan)GetValue(DurationProperty); } set { SetValue(DurationProperty, value); } }

        /// <summary>
        /// <see cref="SlideAnimation"/> 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty SlideAnimationProperty = DependencyProperty.Register(
            nameof(SlideAnimation), typeof(DoubleAnimation), typeof(SlideTransition), new PropertyMetadata());

        /// <summary>
        /// スライド時のアニメーションを取得または設定します。
        /// </summary>
        public DoubleAnimation SlideAnimation { get { return (DoubleAnimation)GetValue(SlideAnimationProperty); } set { SetValue(SlideAnimationProperty, value); } }

        /// <summary>
        /// <see cref="Index"/> 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty IndexProperty = DependencyProperty.Register(
            nameof(Index), typeof(int), typeof(SlideTransition), new PropertyMetadata(-1, OnIndexChanged));

        /// <summary>
        /// 遷移する量を取得または設定します。
        /// </summary>
        public byte Index { get { return (byte)GetValue(IndexProperty); } set { SetValue(IndexProperty, value); } }

        #endregion

        #region [Method] public override

        /// <summary>
        /// テンプレート適用時の処理を指定します。
        /// </summary>
        public override void OnApplyTemplate()
        {
            _core = GetTemplateChild("core") as SlideTransitionCore;

            base.OnApplyTemplate();
        }

        #endregion

        #region [Method] private static

        /// <summary>
        /// Index 添付プロパティ変更時の処理。
        /// </summary>
        /// <param name="sender">イベント発生オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private static async void OnIndexChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var self = (SlideTransition)sender;
            var index = (int)e.NewValue;

            await self.Translate(index);
        }

        #endregion

        #region [Method] private

        /// <summary>
        /// 画面遷移します。
        /// </summary>
        /// <param name="index">インデックス</param>
        /// <returns>非同期オブジェクト</returns>
        private async Task Translate(int index)
        {
            if (SlideAnimation == null)
            {
                var defaultAnimation = new DoubleAnimation();

                defaultAnimation.From = 0;
                defaultAnimation.Duration = Duration;
                defaultAnimation.EasingFunction = new QuinticEase { EasingMode = EasingMode.EaseOut };

                SlideAnimation = defaultAnimation;
            }

            await _core.Translate(index, SlideAnimation);
        }

        #endregion
    }
}