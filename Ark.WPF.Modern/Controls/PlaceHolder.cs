using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Ark.Core.Event;

namespace Ark.WPF.Modern.Controls
{
    /// <summary>
    /// プレースホルダーを表示する添付プロパティを定義します。
    /// </summary>
    public static class PlaceHolder
    {
        #region [Property] Dependency

        /// <summary>
        /// Text 添付プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.RegisterAttached(
            "Text", typeof(string), typeof(PlaceHolder), new PropertyMetadata(null, OnPropertyChanged));

        /// <summary>
        /// 指定された <see cref="DependencyObject"/> から Text 添付プロパティの値を取得します。
        /// </summary>
        /// <param name="obj">オブジェクト</param>
        /// <returns>Text 添付プロパティの値</returns>
        public static string GetText(DependencyObject obj) => obj.GetValue(TextProperty) as string;

        /// <summary>
        /// Text 添付プロパティの値を、指定された <see cref="DependencyObject"/> に設定します。
        /// </summary>
        /// <param name="obj">オブジェクト</param>
        /// <param name="value">Text 添付プロパティに設定する値</param>
        public static void SetText(DependencyObject obj, string value) => obj.SetValue(TextProperty, value);

        /// <summary>
        /// Color 添付プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty ColorProperty = DependencyProperty.RegisterAttached(
            "Color", typeof(Brush), typeof(PlaceHolder), new PropertyMetadata(new SolidColorBrush(Colors.Black), OnPropertyChanged));

        /// <summary>
        /// 指定された <see cref="DependencyObject"/> から Color 添付プロパティの値を取得します。
        /// </summary>
        /// <param name="obj">オブジェクト</param>
        /// <returns>Color 添付プロパティの値</returns>
        public static Brush GetColor(DependencyObject obj) => obj.GetValue(ColorProperty) as Brush;

        /// <summary>
        /// Color 添付プロパティの値を、指定された <see cref="DependencyObject"/> に設定します。
        /// </summary>
        /// <param name="obj">オブジェクト</param>
        /// <param name="value">Color 添付プロパティに設定する値</param>
        public static void SetColor(DependencyObject obj, Brush value) => obj.SetValue(ColorProperty, value);

        #endregion [Property] Dependency

        private static readonly Dictionary<int, WeakEventListener<TextChangedEventHandler, TextChangedEventArgs>> ListenerList =
            new Dictionary<int, WeakEventListener<TextChangedEventHandler, TextChangedEventArgs>>();

        /// <summary>
        /// プレースホルダーを設定します。
        /// </summary>
        /// <param name="sender">イベント発生オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private static void OnPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var textBox = sender as TextBox;

            if (textBox == null)
                return;

            AddTextChangedListner(textBox);
            CreatePlaceHolder(textBox);
        }

        /// <summary>
        /// テキストボックスに、テキスト変更イベントを追加します。
        /// </summary>
        /// <param name="textBox">テキストボックス</param>
        private static void AddTextChangedListner(TextBox textBox)
        {
            var code = textBox.GetHashCode();

            if (ListenerList.ContainsKey(code))
                return;

            var listener = new WeakEventListener<TextChangedEventHandler, TextChangedEventArgs>(
                TextChanged,
                h => textBox.TextChanged += h,
                h => textBox.TextChanged -= h);

            ListenerList.Add(code, listener);
        }

        /// <summary>
        /// テキスト変更イベントの処理。
        /// </summary>
        /// <param name="sender">テキストボックス</param>
        /// <param name="e">変更イベントデータ</param>
        private static void TextChanged(object sender, TextChangedEventArgs e)
        {
            CreatePlaceHolder((TextBox)sender);
        }

        /// <summary>
        /// プレースホルダーを作成します。
        /// </summary>
        /// <param name="textBox">テキストボックス</param>
        private static void CreatePlaceHolder(TextBox textBox)
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                var visual = new Label
                {
                    Content = GetText(textBox),
                    Padding = new Thickness(5, 1, 1, 1),
                    Foreground = GetColor(textBox),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                };

                textBox.Background = new VisualBrush
                {
                    Visual = visual,
                    Stretch = Stretch.None,
                    TileMode = TileMode.None,
                    AlignmentX = AlignmentX.Left,
                    AlignmentY = AlignmentY.Center,
                };
            }
            else
            {
                textBox.Background = new SolidColorBrush(Colors.Transparent);
            }
        }
    }
}