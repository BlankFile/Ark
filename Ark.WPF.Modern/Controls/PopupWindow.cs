using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Ark.Core.Event;
using Ark.WPF.Infra.Media;
using Ark.WPF.Infra.Media.Animation;

namespace Ark.WPF.Modern.Controls
{
    /// <summary>
    /// コントロール上に表示するポップアップコントロールを表します。
    /// </summary>
    public class PopupWindow : ItemsControl
    {

        #region [enum]

        /// <summary>
        /// 表示/非表示時のエフェクトの種類を定義します。
        /// </summary>
        public enum EffectType
        {
            /// <summary>
            /// フェードイン/アウト
            /// </summary>
            Fade,

            /// <summary>
            /// ズームイン/アウト
            /// </summary>
            Zoom,

            /// <summary>
            /// カスタム
            /// </summary>
            Custom
        }

        #endregion

        #region [Constructor]

        /// <summary>
        /// <see cref="PopupWindow"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        static PopupWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PopupWindow), new FrameworkPropertyMetadata(typeof(PopupWindow)));
        }

        #endregion

        #region [Member] readonly

        /// <summary>
        /// 表示時のフェードアニメーション
        /// </summary>
        private static readonly Storyboard _fadeOpenAnimation = new FadeAnimation(0, 1, TimeSpan.FromMilliseconds(300)).ToStoryboard();

        /// <summary>
        /// 非表示時のフェードアニメーション
        /// </summary>
        private static readonly Storyboard _fadeCloseAnimation = new FadeAnimation(1, 0, TimeSpan.FromMilliseconds(200)).ToStoryboard();


        /// <summary>
        /// 表示時のズームアニメーション
        /// </summary>
        private static readonly ZoomAnimation _zoomOpenAnimation = new ZoomAnimation(0, 1, TimeSpan.FromMilliseconds(300));

        /// <summary>
        /// 表示時のズームアニメーション
        /// </summary>
        private static readonly ZoomAnimation _zoomCloseAnimation = new ZoomAnimation(1, 0, TimeSpan.FromMilliseconds(200));

        /// <summary>
        /// 排他オブジェクト
        /// </summary>
        private static readonly object _lockObject = new object();

        #endregion

        #region [Member] 

        /// <summary>
        /// アドナーを閉じる時の処理
        /// </summary>
        private Action _adornerClosingAction;

        /// <summary>
        /// アドナーのクリックイベントを保持する弱参照オブジェクト
        /// </summary>
        private WeakEventListener<MouseButtonEventHandler, MouseButtonEventArgs> _listener;

        /// <summary>
        /// 表示時のアニメーション
        /// </summary>
        private Storyboard _openAnimation;

        /// <summary>
        /// 非表示時のアニメーション
        /// </summary>
        private Storyboard _closeAnimation;

        #endregion

        #region [Property] public static

        /// <summary>
        /// 独自の表示時のアニメーションを取得または設定します。
        /// </summary>
        public static Storyboard CustomOpenAnimation { get; set; }

        /// <summary>
        /// 独自の非表示時のアニメーションを取得または設定します。
        /// </summary>
        public static Storyboard CustomCloseAnimation { get; set; }

        #endregion

        #region [Method] override

        /// <summary>
        /// 指定された項目の表示に使用する要素を作成または識別します。
        /// </summary>
        /// <returns><see cref="ContentControl"/></returns>
        protected override DependencyObject GetContainerForItemOverride() => new ContentControl();

        /// <summary>
        /// 指定された項目が自身のコンテナーかどうか (または自身のコンテナーにすることができるかどうか) を判断します。
        /// </summary>
        /// <param name="item">確認する項目</param>
        /// <returns>常に false</returns>
        protected override bool IsItemItsOwnContainerOverride(object item) => false;

        /// <summary>
        /// テンプレート適用時の処理を指定します。
        /// </summary>
        public override async void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _listener = new WeakEventListener<MouseButtonEventHandler, MouseButtonEventArgs>(
                MouseLeftButtonDownAction,
                h => MouseLeftButtonDown += h,
                h => MouseLeftButtonDown -= h);

            await _openAnimation?.BeginAsync(this);
        }

        #endregion

        #region [Method] public static

        /// <summary>
        /// <see cref="PopupWindow"/> をモードレス表示します。
        /// </summary>
        /// <param name="owner">親コントロール</param>
        /// <param name="content">ポップアップするコントロール</param>
        /// <param name="type">エフェクトの種類</param>
        public static void Show(UIElement owner, FrameworkElement content, EffectType type = EffectType.Fade)
        {
            var target = GetAdornerTarget(owner);
            var adorner = default(PopupWindowAdorner);

            lock (_lockObject)
            {
                adorner = GetPopupWindowAdorner(target) ?? CreateAdorner(target);
            }

            if (adorner.Window != null)
            {
                adorner.Window._openAnimation = adorner.Window.GetOpenStoryboard(type);
                adorner.Window._closeAnimation = adorner.Window.GetCloseStoryboard(type);

                adorner.Window.AddContent(content);
            }
        }

        /// <summary>
        /// <see cref="PopupWindow"/> をモーダル表示します。
        /// </summary>
        /// <param name="owner">親コントロール</param>
        /// <param name="content">ポップアップするコントロール</param>
        /// <param name="type">エフェクトの種類</param>
        public static void ShowDialog(UIElement owner, FrameworkElement content, EffectType type = EffectType.Fade)
        {
            var target = GetAdornerTarget(owner);
            var adorner = GetPopupWindowAdorner(target);

            if (adorner != null)
            {
                return;
            }

            adorner = CreateAdorner(target);

            if (adorner.Window != null)
            {
                adorner.Window._openAnimation = adorner.Window.GetOpenStoryboard(type);
                adorner.Window._closeAnimation = adorner.Window.GetCloseStoryboard(type);

                var frame = new DispatcherFrame();

                var closingAction = default(Action);
                closingAction = () =>
                {
                    adorner.Window._adornerClosingAction -= closingAction;

                    frame.Continue = false;
                };

                adorner.Window._adornerClosingAction += closingAction;

                adorner.Window.AddContent(content);

                Dispatcher.PushFrame(frame);
            }
        }

        /// <summary>
        /// <see cref="PopupWindow"/> を閉じます。
        /// </summary>
        /// <param name="owner">親コントロール</param>
        /// <param name="content">ポップアップするコントロール</param>
        /// <returns>非同期オブジェクト</returns>
        public static async Task Close(UIElement owner, FrameworkElement content)
        {
            var target = GetAdornerTarget(owner);
            var adorner = GetPopupWindowAdorner(target);

            if (adorner?.Window != null)
            {
                await adorner.Window.RemoveContent(content);
            }
            else
            {
                var popup = VisualTreeManager.FindVisualParent<PopupWindow>(content);

                await popup?.RemoveContent(content);
            }
        }

        /// <summary>
        /// すべての <see cref="PopupWindow"/> を閉じます。
        /// </summary>
        /// <param name="content">ポップアップするコントロール</param>
        /// <returns>非同期オブジェクト</returns>
        public static async Task CloseDialog(FrameworkElement content)
        {
            var target = GetAdornerTarget(content);
            var adorner = GetPopupWindowAdorner(target);

            if (adorner?.Window != null)
            {
                await adorner.Window.CloseAdorner();
            }
            else
            {
                var popup = VisualTreeManager.FindVisualParent<PopupWindow>(content);

                await popup?.CloseAdorner();
            }
        }

        #endregion

        #region [Method] protected

        /// <summary>
        /// 表示時の <see cref="Storyboard"/> を取得します。
        /// </summary>
        /// <param name="type">エフェクトの種類</param>
        /// <returns></returns>
        protected virtual Storyboard GetOpenStoryboard(EffectType type)
        {
            switch (type)
            {
                case EffectType.Fade:
                default:
                    return _fadeOpenAnimation;

                case EffectType.Zoom:
                    _zoomOpenAnimation.InitTransform(this);
                    return _zoomOpenAnimation.MakeStoryboard();

                case EffectType.Custom:
                    return CustomOpenAnimation;
            }
        }

        /// <summary>
        /// 非表示時の <see cref="Storyboard"/> を取得します。
        /// </summary>
        /// <param name="type">エフェクトの種類</param>
        /// <returns></returns>
        protected virtual Storyboard GetCloseStoryboard(EffectType type)
        {
            switch (type)
            {
                case EffectType.Fade:
                default:
                    return _fadeCloseAnimation;

                case EffectType.Zoom:
                    _zoomCloseAnimation.InitTransform(this);
                    return _zoomCloseAnimation.MakeStoryboard();

                case EffectType.Custom:
                    return CustomCloseAnimation;
            }
        }

        #endregion

        #region [Method] private static

        /// <summary>
        /// アドナーの対象を取得します。
        /// </summary>
        /// <param name="element">コントロール</param>
        /// <returns>アドナーの対象</returns>
        private static UIElement GetAdornerTarget(UIElement element)
        {
            if (element is Window)
            {
                var window = element as Window;

                return window.Content as UIElement;
            }

            return element;
        }

        /// <summary>
        /// アドナーレイヤーを取得します。
        /// </summary>
        /// <param name="element">コントロール</param>
        /// <returns>アドナーレイヤー</returns>
        private static AdornerLayer GetAdornerLayer(UIElement element)
        {
            if (element == null)
            {
                return null;
            }

            return AdornerLayer.GetAdornerLayer(element);
        }

        /// <summary>
        /// <see cref="PopupWindowAdorner"/> を取得します。
        /// </summary>
        /// <param name="element">コントロール</param>
        /// <returns><see cref="PopupWindowAdorner"/></returns>
        private static PopupWindowAdorner GetPopupWindowAdorner(UIElement element)
        {
            var layer = GetAdornerLayer(element);

            if (layer == null)
            {
                return null;
            }

            var layers = layer.GetAdorners(element);

            return layers?.OfType<PopupWindowAdorner>().FirstOrDefault();
        }

        /// <summary>
        /// アドナーを作成します。
        /// </summary>
        /// <param name="element">コントロール</param>
        /// <returns><see cref="PopupWindowAdorner"/></returns>
        private static PopupWindowAdorner CreateAdorner(UIElement element)
        {
            var layer = GetAdornerLayer(element);

            if (layer == null)
            {
                return null;
            }

            var window = new PopupWindow();
            var adorner = new PopupWindowAdorner(element, window);

            var closingAction = default(Action);
            closingAction = () =>
            {
                window._adornerClosingAction -= closingAction;

                layer?.Remove(adorner);
            };

            window._adornerClosingAction += closingAction;
            layer.Add(adorner);

            return adorner;
        }

        #endregion

        #region [Method] private

        /// <summary>
        /// コントロールを追加します。
        /// </summary>
        /// <param name="content">コントロール</param>
        private void AddContent(FrameworkElement content)
        {
            var handler = default(RoutedEventHandler);

            handler = async (sender, args) =>
            {
                content.Loaded -= handler;

                var container = ContainerFromElement(content) as FrameworkElement;

                container.Focus();

                await _openAnimation?.BeginAsync(container);
            };

            content.Loaded += handler;

            Items.Add(content);
        }

        /// <summary>
        /// コントロールを削除します。
        /// </summary>
        /// <param name="content">コントロール</param>
        /// <returns>非同期オブジェクト</returns>
        private async Task RemoveContent(FrameworkElement content)
        {
            if (0 <= Items.IndexOf(content))
            {
                var container = ContainerFromElement(content) as FrameworkElement;

                if (Items.Count == 1)
                {
                    await CloseAdorner();
                }
                else if (1 < Items.Count)
                {
                    await _closeAnimation.BeginAsync(container);

                    Items.Remove(content);
                }
            }
        }

        /// <summary>
        /// <see cref="PopupWindow"/> を閉じます。
        /// </summary>
        /// <returns></returns>
        private async Task CloseAdorner()
        {
            await _closeAnimation?.BeginAsync(this);

            Items.Clear();

            _adornerClosingAction?.Invoke();
        }

        /// <summary>
        /// <see cref="PopupWindow"/> がクリックされた場合、ダイアログを終了します。
        /// </summary>
        /// <param name="sender">イベント発生オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private async void MouseLeftButtonDownAction(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is PopupWindow)
            {
                await CloseAdorner();
            }
        }

        #endregion


    }
}
