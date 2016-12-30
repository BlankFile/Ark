using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using Ark.WPF.Infra.Threading;

namespace Ark.WPF.Modern.Controls
{
    /// <summary>
    /// スライドで画面を遷移するコントロールを表します。
    /// </summary>
    public class SlideTransition : ItemsControl
    {
        #region [Member]

        private SlideTransitionCore _core;

        #endregion [Member]

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

        /// <summary> <see cref="Next"/> 依存関係プロパティを識別します。 </summary>
        public static readonly DependencyProperty NextProperty = DependencyProperty.Register(
            nameof(Next), typeof(bool), typeof(SlideTransition), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnNextChanged));

        /// <summary>
        /// 次画面へのトリガーを取得または設定します。
        /// </summary>
        public bool Next { get { return (bool)GetValue(NextProperty); } set { SetValue(NextProperty, value); } }

        /// <summary>
        /// <see cref="Previous"/> 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty PreviousProperty = DependencyProperty.Register(
            nameof(Previous), typeof(bool), typeof(SlideTransition), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPreviousChanged));

        /// <summary>
        /// 前画面へのトリガーを取得または設定します。
        /// </summary>
        public bool Previous { get { return (bool)GetValue(PreviousProperty); } set { SetValue(PreviousProperty, value); } }

        /// <summary>
        /// <see cref="Value"/> 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            nameof(Value), typeof(byte), typeof(SlideTransition), new PropertyMetadata((byte)1));

        /// <summary>
        /// 遷移する量を取得または設定します。
        /// </summary>
        public byte Value { get { return (byte)GetValue(ValueProperty); } set { SetValue(ValueProperty, value); } }

        #endregion [Property] Dependency

        #region [Method] public override

        /// <summary>
        /// テンプレート適用時の処理を指定します。
        /// </summary>
        public override void OnApplyTemplate()
        {
            _core = GetTemplateChild("core") as SlideTransitionCore;

            base.OnApplyTemplate();
        }

        #endregion [Method] public override

        #region [Method] private static

        /// <summary>
        /// Next 添付プロパティ変更時の処理。
        /// </summary>
        /// <param name="sender">イベント発生オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private static async void OnNextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var self = (SlideTransition)sender;
            var fire = (bool)e.NewValue;

            if (fire)
            {
                await self.Translate(SlideTransitionCore.Direction.Next);
            }
        }

        /// <summary>
        /// Previous 添付プロパティ変更時の処理。
        /// </summary>
        /// <param name="sender">イベント発生オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private static async void OnPreviousChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var self = (SlideTransition)sender;
            var fire = (bool)e.NewValue;

            if (fire)
            {
                await self.Translate(SlideTransitionCore.Direction.Previous);
            }
        }

        #endregion [Method] private static

        #region [Method] private

        /// <summary>
        /// 画面遷移します。
        /// </summary>
        /// <param name="direction">スライドする方向</param>
        /// <returns>非同期オブジェクト</returns>
        private async Task Translate(SlideTransitionCore.Direction direction)
        {
            if (SlideAnimation == null)
            {
                var defaultAnimation = new DoubleAnimation();

                defaultAnimation.From = 0;
                defaultAnimation.Duration = Duration;
                defaultAnimation.EasingFunction = new QuinticEase { EasingMode = EasingMode.EaseOut };

                SlideAnimation = defaultAnimation;
            }

            await FlagDown();

            await _core.Translate(direction, Value, SlideAnimation);
        }

        /// <summary>
        /// 遷移トリガーフラグを false にします。
        /// </summary>
        private async Task FlagDown()
        {
            await UITask.Run(() =>
            {
                Next = false;
                Previous = false;
            });
        }

        #endregion [Method] private
    }
}