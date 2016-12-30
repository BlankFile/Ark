using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Ark.WPF.Infra.Mvvm;

namespace Ark.WPF.Modern.Controls
{
    /// <summary>
    /// DropDownButton.xaml の相互作用ロジック
    /// </summary>
    public partial class DropDownButton : UserControl
    {
        /// <summary>
        /// <see cref="DropDownButton"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public DropDownButton()
        {
            DropDownMenu.Clear();

            InitializeComponent();

            ClickCommand = new ActionCommand(() =>
            {
                StaysOpen = true;
                IsOpen = true;
            });

            OpenedCommand = new ActionCommand(() =>
            {
                StaysOpen = false;
            });
        }

        #region [Property] Dependency

        /// <summary>
        /// <see cref="Command"/> 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            nameof(Command), typeof(ICommand), typeof(DropDownButton), new PropertyMetadata(null));

        /// <summary>
        /// コマンドを取得または設定します。
        /// </summary>
        public ICommand Command { get { return (ICommand)GetValue(CommandProperty); } set { SetValue(CommandProperty, value); } }

        /// <summary>
        /// <see cref="Parameter"/> 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty ParameterProperty = DependencyProperty.Register(
            nameof(Parameter), typeof(object), typeof(DropDownButton), new PropertyMetadata(null));

        /// <summary>
        /// パラメーターを取得または設定します。
        /// </summary>
        public object Parameter { get { return (object)GetValue(ParameterProperty); } set { SetValue(ParameterProperty, value); } }

        /// <summary>
        /// <see cref="DropDownMenu"/> 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty DropDownMenuProperty = DependencyProperty.Register(
            nameof(DropDownMenu), typeof(IList), typeof(DropDownButton), new PropertyMetadata(new List<MenuItem>()));

        /// <summary>
        /// メニューアイテムを取得または設定します。
        /// </summary>
        public IList DropDownMenu { get { return (IList)GetValue(DropDownMenuProperty); } set { SetValue(DropDownMenuProperty, value); } }

        /// <summary>
        /// <see cref="IsOpen"/> 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register(
            nameof(IsOpen), typeof(bool), typeof(DropDownButton), new PropertyMetadata(false));

        /// <summary>
        /// ドロップダウンが開いているかどうかを示す値を取得または設定します。
        /// </summary>
        public bool IsOpen { get { return (bool)GetValue(IsOpenProperty); } set { SetValue(IsOpenProperty, value); } }

        /// <summary>
        /// <see cref="StaysOpen"/> 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty StaysOpenProperty = DependencyProperty.Register(
            nameof(StaysOpen), typeof(bool), typeof(DropDownButton), new PropertyMetadata(false));

        /// <summary>
        /// ドロップダウン非アクティブ時にドロップダウンを開き続けるかどうかを示す値を取得または設定します。
        /// </summary>
        public bool StaysOpen { get { return (bool)GetValue(StaysOpenProperty); } set { SetValue(StaysOpenProperty, value); } }

        /// <summary>
        /// <see cref="MainButtonWidth"/> 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty MainButtonWidthProperty = DependencyProperty.Register(
            nameof(MainButtonWidth), typeof(int), typeof(DropDownButton), new PropertyMetadata(0));

        /// <summary>
        /// メインボタンの横幅を取得または設定します。
        /// </summary>
        public int MainButtonWidth { get { return (int)GetValue(MainButtonWidthProperty); } set { SetValue(MainButtonWidthProperty, value); } }

        #endregion

        #region [Propert] Command

        /// <summary> ドロップダウン表示ボタン押下時の処理 </summary>
        public ICommand ClickCommand { get; }

        /// <summary> ドロップダウン表示時の処理 </summary>
        public ICommand OpenedCommand { get; }

        #endregion

        #region [Method] private

        /// <summary>
        /// メニューアイテム押下時の処理。
        /// <para>ドロップダウンを閉じます。</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickItem(object sender, RoutedEventArgs e)
        {
            IsOpen = false;
        }

        #endregion
    }
}
