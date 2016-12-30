using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Ark.Core.Event;
using Ark.WPF.Modern.ViewModels;

namespace Ark.WPF.Modern.Controls
{
    /// <summary>
    /// ウインドウと密なナビゲーションコントロールです。
    /// </summary>
    public class BindingFrame : Frame
    {
        #region 依存関係プロパティ

        /// <summary>
        /// 添付プロパティ
        /// </summary>
        public static readonly DependencyProperty ViewUriProperty = DependencyProperty.Register(
            "ViewUri", typeof(string), typeof(BindingFrame), new PropertyMetadata(null, OnUriChanged));

        /// <summary>
        /// フレーム表示コンテンツのURI
        /// </summary>
        public string ViewUri
        {
            get { return GetValue(ViewUriProperty) as string; }
            set { SetValue(ViewUriProperty, value); }
        }

        /// <summary>
        /// 添付プロパティ
        /// </summary>
        public static readonly DependencyProperty ContentViewModelProperty = DependencyProperty.Register(
            "ContentViewModel", typeof(object), typeof(BindingFrame));

        /// <summary>
        /// フレーム表示コンテンツの ViewModel
        /// </summary>
        public object ContentViewModel
        {
            get { return GetValue(ContentViewModelProperty) as object; }
            set { SetValue(ContentViewModelProperty, value); }
        }

        #endregion

        private WeakEventListener<NavigatedEventHandler, NavigationEventArgs> WeakEvent { get; set; }

        /// <summary>
        /// フレーム初期処理。
        /// <para>ナビゲーション時のイベントを設定します。</para>
        /// </summary>
        /// <param name="e">イベントデータ</param>
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            WeakEvent = new WeakEventListener<NavigatedEventHandler, NavigationEventArgs>(
                OnNavigated,
                h => Navigated += h,
                h => Navigated -= h);
        }

        /// <summary>
        /// ナビゲーション時の処理。
        /// <para>フレーム表示コンテンツの DataCotext を設定します。</para>
        /// </summary>
        /// <param name="sender">イベント発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            var page = e.Content as FrameworkElement;

            if (page != null)
            {
                if (page.DataContext != null)
                {
                    ContentViewModel = page.DataContext;

                    var vmType = page.DataContext.GetType();

                    if (IsContentViewModel(vmType, typeof(WindowContentViewModel<>)))
                    {
                        vmType.GetProperty("WindowViewModel")
                              .SetValue(page.DataContext, DataContext);
                    }
                    else if (IsContentViewModel(vmType, typeof(DialogContentViewModel<>)))
                    {
                        vmType.GetProperty("DialogViewModel")
                              .SetValue(page.DataContext, DataContext);
                    }
                }
                else
                {
                    if (ContentViewModel != null)
                    {
                        page.DataContext = ContentViewModel;
                    }
                }
            }
        }

        /// <summary>
        /// フレーム表示コンテンツの DataContext が、指定した ViewModel と同型かどうか判断します。
        /// </summary>
        /// <param name="vmType">DataContext のタイプ</param>
        /// <param name="contentVmType">フレーム表示コンテンツのタイプ</param>
        /// <returns>同型かどうか</returns>
        private bool IsContentViewModel(Type vmType, Type contentVmType)
        {
            for (Type type = vmType; type != null; type = type.BaseType)
            {
                if (!type.IsGenericType)
                    continue;

                if (type.GetGenericTypeDefinition() == contentVmType)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// URI変更時の処理。
        /// <para>変更されたURIへナビゲーションします。</para>
        /// </summary>
        /// <param name="sender">イベント発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private static void OnUriChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var frame = sender as BindingFrame;

            if (frame == null)
                return;
            if (frame.ViewUri.IsNullOrEmpty())
                return;

            try
            {
                frame.Navigate(new Uri(frame.ViewUri));
            }
            catch
            {

            }
        }

    }
}
