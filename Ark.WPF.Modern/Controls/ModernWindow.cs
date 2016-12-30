using System;
using System.Threading.Tasks;
using System.Windows;

namespace Ark.WPF.Modern.Controls
{
    /// <summary>
    /// モダンスタイルのウインドウ。
    /// </summary>
    public class ModernWindow : Window
    {
        /// <summary>
        /// <see cref="ModernWindow"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public ModernWindow() { }

        #region [Property] Dependency

        /// <summary>
        /// <see cref="IsVisibleTitle"/> 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty IsVisibleTitleProperty = DependencyProperty.Register(
            nameof(IsVisibleTitle), typeof(bool), typeof(ModernWindow), new PropertyMetadata(true));

        /// <summary>
        /// ウインドウ左上部のタイトルを表示するかどうかを示す値を取得または設定します。
        /// </summary>
        public bool IsVisibleTitle { get { return (bool)GetValue(IsVisibleTitleProperty); } set { SetValue(IsVisibleTitleProperty, value); } }

        /// <summary>
        /// <see cref="IsEnabledControlBox"/> 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty IsEnabledControlBoxProperty = DependencyProperty.Register(
            nameof(IsEnabledControlBox), typeof(bool), typeof(ModernWindow), new PropertyMetadata(true));

        /// <summary>
        /// ウインドウ右上部のコントロールボックスを使用するかどうかを示す値を取得または設定します。
        /// </summary>
        public bool IsEnabledControlBox { get { return (bool)GetValue(IsEnabledControlBoxProperty); } set { SetValue(IsEnabledControlBoxProperty, value); } }

        /// <summary> <see cref="IsVisibleControlBox"/> 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty IsVisibleControlBoxProperty = DependencyProperty.Register(
            nameof(IsVisibleControlBox), typeof(bool), typeof(ModernWindow), new PropertyMetadata(true));

        /// <summary>
        /// ウインドウ右上部のコントロールボックスを表示するかどうかを示す値を取得または設定します。
        /// </summary>
        public bool IsVisibleControlBox { get { return (bool)GetValue(IsVisibleControlBoxProperty); } set { SetValue(IsVisibleControlBoxProperty, value); } }

        /// <summary>
        /// <see cref="WindowClosingAction"/> 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty WindowClosingActionProperty = DependencyProperty.Register(
            nameof(WindowClosingAction), typeof(Func<Task<bool>>), typeof(ModernWindow));

        /// <summary>
        /// ウインドウ終了時の処理を取得または設定します。
        /// </summary>
        public Func<Task<bool>> WindowClosingAction { get { return (Func<Task<bool>>)GetValue(WindowClosingActionProperty); } set { SetValue(WindowClosingActionProperty, value); } }

        #endregion [Property] Dependency
    }
}