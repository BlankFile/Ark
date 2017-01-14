using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Ark.WPF.Modern.Controls
{
    /// <summary>
    /// <see cref="PopupWindow"/> を表示するためのアドナーを定義します。
    /// </summary>
    public class PopupWindowAdorner : Adorner
    {
        /// <summary>
        /// 監視する <see cref="PopupWindow"/> を取得します。
        /// </summary>
        public PopupWindow Window { get; }

        /// <summary>
        /// <see cref="PopupWindowAdorner"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="adornedElement">装飾対象のコントロール</param>
        /// <param name="window"><see cref="PopupWindow"/></param>
        public PopupWindowAdorner(UIElement adornedElement, PopupWindow window) : base(adornedElement)
        {
            Window = window;
            AddVisualChild(window);
        }

        #region [Method] override

        /// <summary>
        /// この要素内の子ビジュアル要素の数を取得します。
        /// </summary>
        protected override int VisualChildrenCount => Convert.ToInt32(Window != null);

        /// <summary>
        /// <see cref="PopupWindow"/> を返します。
        /// </summary>
        /// <param name="index">インデックス</param>
        /// <returns><see cref="PopupWindow"/></returns>
        protected override Visual GetVisualChild(int index) => Window;

        /// <summary>
        /// 測定したサイズを返します。
        /// </summary>
        /// <param name="constraint">サイズ</param>
        /// <returns>測定サイズ</returns>
        protected override Size MeasureOverride(Size constraint)
        {
            var size = AdornedElement.RenderSize;

            Window.Width = size.Width;
            Window.Height = size.Height;
            Window.Measure(size);

            return Window.DesiredSize;
        }

        /// <summary>
        /// 配置したサイズを返します。
        /// </summary>
        /// <param name="finalSize">サイズ</param>
        /// <returns>配置サイズ</returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            Window.Arrange(new Rect(new Point(0, 0), finalSize));

            return new Size(Window.ActualWidth, Window.ActualHeight);
        }

        #endregion
    }
}