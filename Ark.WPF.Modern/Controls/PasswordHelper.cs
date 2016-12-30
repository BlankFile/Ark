using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Ark.Core.Event;

namespace Ark.WPF.Modern.Controls
{
    /// <summary>
    /// パスワードコントロールを補助する添付プロパティを定義します。
    /// </summary>
    public static class PasswordHelper
    {
        #region [Property] Attached

        /// <summary>
        /// Password 添付プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty PasswordProperty = DependencyProperty.RegisterAttached(
            "Password", typeof(string), typeof(PasswordHelper), new FrameworkPropertyMetadata(null, OnPasswordChanged));

        /// <summary>
        /// 指定された <see cref="DependencyObject"/> から Password 添付プロパティの値を取得します。
        /// </summary>
        /// <param name="obj">オブジェクト</param>
        /// <returns>Password 添付プロパティの値</returns>
        public static string GetPassword(DependencyObject obj) => (string)obj.GetValue(PasswordProperty);

        /// <summary>
        /// Password 添付プロパティの値を、指定された <see cref="DependencyObject"/> に設定します。
        /// </summary>
        /// <param name="obj">オブジェクト</param>
        /// <param name="value">Password 添付プロパティに設定する値</param>
        public static void SetPassword(DependencyObject obj, string value) => obj.SetValue(PasswordProperty, value);

        #endregion [Property] Attached

        private static readonly Dictionary<int, WeakEventListener<RoutedEventHandler, RoutedEventArgs>> _listenerList =
            new Dictionary<int, WeakEventListener<RoutedEventHandler, RoutedEventArgs>>();

        /// <summary>
        /// パスワードを設定します。
        /// </summary>
        /// <param name="sender">イベント発生オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private static void OnPasswordChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;

            if (passwordBox == null)
                return;

            var password = (string)e.NewValue;

            if (passwordBox.Password != password)
                passwordBox.Password = password;

            AddPasswordChangedListner(passwordBox);
        }

        /// <summary>
        /// パスワードボックスに、パスワード変更イベントを追加します。
        /// </summary>
        /// <param name="passwordBox">パスワードボックス</param>
        private static void AddPasswordChangedListner(PasswordBox passwordBox)
        {
            var code = passwordBox.GetHashCode();

            if (_listenerList.ContainsKey(code))
                return;

            var listener = new WeakEventListener<RoutedEventHandler, RoutedEventArgs>(
                PasswordChanged,
                h => passwordBox.PasswordChanged += h,
                h => passwordBox.PasswordChanged -= h);

            _listenerList.Add(code, listener);
        }

        /// <summary>
        /// パスワード変更イベントの処理。
        /// </summary>
        /// <param name="sender">パスワードボックス</param>
        /// <param name="e">変更イベントデータ</param>
        private static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;

            if (GetPassword(passwordBox) != passwordBox.Password)
                SetPassword(passwordBox, passwordBox.Password);
        }
    }
}