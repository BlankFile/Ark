using System;
using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Media.Animation;
using Ark.WPF.Infra.Media.Animation;

namespace Ark.WPF.Infra.Interactivity.Actions
{
    /// <summary>
    /// フェードアニメーションを実行します。
    /// </summary>
    public class FadeEffectAction : TriggerAction<DependencyObject>
    {
        #region [Property] Dependency

        /// <summary>
        /// <see cref="From"/> 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty FromProperty = DependencyProperty.Register(
            nameof(From), typeof(double), typeof(FadeEffectAction), new PropertyMetadata(1d));

        /// <summary>
        /// フェードのの開始値を取得または設定します。
        /// </summary>
        public double From { get { return (double)GetValue(FromProperty); } set { SetValue(FromProperty, value); } }

        /// <summary>
        /// <see cref="To"/> 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty ToProperty = DependencyProperty.Register(
            nameof(To), typeof(double), typeof(FadeEffectAction), new PropertyMetadata(1d));

        /// <summary>
        /// フェードの終了値を取得または設定します。
        /// </summary>
        public double To { get { return (double)GetValue(ToProperty); } set { SetValue(ToProperty, value); } }

        /// <summary>
        /// <see cref="Duration"/> 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty DurationProperty = DependencyProperty.Register(
            nameof(Duration), typeof(TimeSpan), typeof(FadeEffectAction), new PropertyMetadata(TimeSpan.FromMilliseconds(300)));

        /// <summary>
        /// フェードする時間を取得または設定します。
        /// </summary>
        public TimeSpan Duration { get { return (TimeSpan)GetValue(DurationProperty); } set { SetValue(DurationProperty, value); } }

        #endregion [Property] Dependency

        /// <summary>
        /// アクションを実行します。
        /// </summary>
        /// <param name="parameter">パラメーター</param>
        protected override void Invoke(object parameter)
        {
            if (AssociatedObject != null && AssociatedObject is FrameworkElement)
            {
                var storyboard = new FadeAnimation(From, To, Duration).ToStoryboard();

                storyboard.Begin((FrameworkElement)AssociatedObject);
            }
        }
    }
}