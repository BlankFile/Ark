using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Ark.WPF.Modern.Controls
{
    /// <summary>
    /// スライドで画面を遷移するための基幹コントロールを表します。
    /// </summary>
    internal class SlideTransitionCore : Control
    {
        #region [enum]

        /// <summary>
        /// スライドの方向を定義します。
        /// </summary>
        public enum Direction
        {
            /// <summary>
            /// 進む
            /// </summary>
            Next = 1,

            /// <summary>
            /// 戻る
            /// </summary>
            Previous = -1
        }

        #endregion [enum]

        #region [Member]

        /// <summary>
        /// スライドするコンテナー
        /// </summary>
        private FrameworkElement _container;

        /// <summary>
        /// 表示中の画面
        /// </summary>
        private FrameworkElement _currentDisplay;

        /// <summary>
        /// 表示画面のインデックス
        /// </summary>
        private int _index = -1;

        #endregion [Member]

        #region [Property] Dependency

        /// <summary>
        /// <see cref="Items"/> 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register(
            nameof(Items), typeof(ICollection), typeof(SlideTransitionCore), new FrameworkPropertyMetadata());

        /// <summary>
        /// スライド対象の画面一覧を取得または設定します。
        /// </summary>
        public ICollection Items { get { return (ICollection)GetValue(ItemsProperty); } set { SetValue(ItemsProperty, value); } }

        /// <summary>
        /// <see cref="Display1"/> 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty Display1Property = DependencyProperty.Register(
            nameof(Display1), typeof(object), typeof(SlideTransitionCore), new FrameworkPropertyMetadata());

        /// <summary>
        /// Display1を取得または設定します。
        /// </summary>
        public object Display1 { get { return GetValue(Display1Property); } set { SetValue(Display1Property, value); } }

        /// <summary>
        /// <see cref="Display2"/> 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty Display2Property = DependencyProperty.Register(
            nameof(Display2), typeof(object), typeof(SlideTransitionCore), new FrameworkPropertyMetadata());

        /// <summary>
        /// Display2を取得または設定します。
        /// </summary>
        public object Display2 { get { return GetValue(Display2Property); } set { SetValue(Display2Property, value); } }

        /// <summary>
        /// <see cref="ContainerWidth"/> 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty ContainerWidthProperty = DependencyProperty.Register(
            nameof(ContainerWidth), typeof(double), typeof(SlideTransitionCore), new FrameworkPropertyMetadata());

        /// <summary>
        /// コンテナーの幅を取得または設定します。
        /// </summary>
        public double ContainerWidth { get { return (double)GetValue(ContainerWidthProperty); } set { SetValue(ContainerWidthProperty, value); } }

        /// <summary>
        /// <see cref="TranslateX"/> 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty TranslateXProperty = DependencyProperty.Register(
            nameof(TranslateX), typeof(double), typeof(SlideTransitionCore), new FrameworkPropertyMetadata());

        /// <summary>
        /// コンテナーのX座標の位置を取得または設定します。
        /// </summary>
        public double TranslateX { get { return (double)GetValue(TranslateXProperty); } set { SetValue(TranslateXProperty, value); } }

        /// <summary>
        /// <see cref="Display1TranslateX"/> 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty Display1TranslateXProperty = DependencyProperty.Register(
            nameof(Display1TranslateX), typeof(double), typeof(SlideTransitionCore), new FrameworkPropertyMetadata());

        /// <summary>
        /// Display1のX座標の位置を取得または設定します。
        /// </summary>
        public double Display1TranslateX { get { return (double)GetValue(Display1TranslateXProperty); } set { SetValue(Display1TranslateXProperty, value); } }

        /// <summary>
        /// <see cref="Display2TranslateX"/> 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty Display2TranslateXProperty = DependencyProperty.Register(
            nameof(Display2TranslateX), typeof(double), typeof(SlideTransitionCore), new FrameworkPropertyMetadata());

        /// <summary>
        /// Display2のX座標の位置を取得または設定します。
        /// </summary>
        public double Display2TranslateX { get { return (double)GetValue(Display2TranslateXProperty); } set { SetValue(Display2TranslateXProperty, value); } }

        #endregion [Property] Dependency

        #region [Method] override

        /// <summary>
        /// テンプレート適用時の処理を指定します。
        /// </summary>
        public override void OnApplyTemplate()
        {
            _container = GetTemplateChild("container") as FrameworkElement;

            base.OnApplyTemplate();
        }

        #endregion [Method] override

        #region [Method]

        /// <summary>
        /// 画面遷移します。
        /// </summary>
        /// <param name="direction">スライドする方向</param>
        /// <param name="value">遷移する量</param>
        /// <param name="animation">アニメーション</param>
        /// <returns>非同期オブジェクト</returns>
        internal async Task Translate(Direction direction, byte value, DoubleAnimation animation)
        {
            var index = (int)direction * value;

            var nextDisplay = Items?.OfType<FrameworkElement>().ElementAtOrDefault(_index + index);

            if (nextDisplay != null)
            {
                TranslateX = (double.IsNaN(ContainerWidth) ? ActualWidth : ContainerWidth) * -index;

                if (_currentDisplay == Display1)
                {
                    Display2 = nextDisplay;
                    Display2TranslateX = -TranslateX;
                    Display1TranslateX = 0;
                }
                else
                {
                    Display1 = nextDisplay;
                    Display1TranslateX = -TranslateX;
                    Display2TranslateX = 0;
                }

                _currentDisplay = nextDisplay;
                _index = _index + index;
            }

            var display1 = (Display1 as FrameworkElement) ?? new FrameworkElement();
            var display2 = (Display2 as FrameworkElement) ?? new FrameworkElement();

            display1.Visibility = Visibility.Visible;
            display2.Visibility = Visibility.Visible;

            Storyboard.SetTargetProperty(animation, new PropertyPath("RenderTransform.(TranslateTransform.X)"));

            await animation.ToStoryboard().BeginAsync(_container);

            if (_currentDisplay == display1)
            {
                display2.Visibility = Visibility.Hidden;
            }
            else
            {
                display1.Visibility = Visibility.Hidden;
            }
        }

        #endregion [Method]
    }
}