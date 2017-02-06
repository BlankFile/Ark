using System;
using System.Windows;
using System.Windows.Input;
using Ark.WPF.Infra.Mvvm;
using Sample.Properties;

namespace Sample.ViewModels
{
    /// <summary>
    /// メインウインドウの ViewModel を表します。
    /// </summary>
    public class MainWindowViewModel : ViewModel
    {
        #region [enum]

        /// <summary>
        /// メニューの種類を表します。
        /// </summary>
        public enum MenuType
        {
            /// <summary> なし </summary>
            None = 0,

            /// <summary> メイン </summary>
            Main = 1,

            /// <summary> 設定 </summary>
            Setting = 2,

            /// <summary> 情報 </summary>
            Information = 3
        }

        #endregion

        #region [Constructor]

        /// <summary>
        ///   <see cref="MainWindowViewModel"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public MainWindowViewModel()
        {
            Top = Settings.Default.WindowPositionTop;
            Left = Settings.Default.WindowPositionLeft;
            Width = Settings.Default.WindowSizeWidth;
            Height = Settings.Default.WindowSizeHeight;

            CurrentMenu = MenuType.None;

            LoadedCommand = new ActionCommand(Loaded);
            PreviewKeyDownCommand = new ActionCommand<KeyEventArgs>(PreviewKeyDown);
            ChangePageCommand = new ActionCommand<MenuType>(ChangePage, CanChangePage);
        }

        #endregion

        #region [Property]

        /// <summary>
        /// ウインドウのX座標
        /// </summary>
        public double Left { get { return Get<double>(); } set { Set(value); } }

        /// <summary>
        /// ウインドウのY座標
        /// </summary>
        public double Top { get { return Get<double>(); } set { Set(value); } }

        /// <summary>
        /// ウインドウの横幅
        /// </summary>
        public double Width { get { return Get<double>(); } set { Set(value); } }

        /// <summary>
        /// ウインドウの縦幅
        /// </summary>
        public double Height { get { return Get<double>(); } set { Set(value); } }

        /// <summary>
        /// ウインドウの状態
        /// </summary>
        public WindowState State { get { return Get<WindowState>(); } set { Set(value); } }

        /// <summary>
        /// 読み込み中フラグ
        /// </summary>
        public bool IsLoading { get { return Get<bool>(); } set { Set(value); } }

        /// <summary>
        /// 表示中のページ
        /// </summary>
        public MenuType CurrentMenu { get { return Get<MenuType>(); } set { Set(value); } }

        /// <summary>
        /// 画面遷移（進む）フラグ
        /// </summary>
        public bool Next { get { return Get<bool>(); } set { Set(value); } }

        /// <summary>
        /// 画面遷移（戻る）フラグ
        /// </summary>
        public bool Previous { get { return Get<bool>(); } set { Set(value); } }

        /// <summary>
        /// 画面を遷移する量
        /// </summary>
        public byte SlideValue { get { return Get<byte>(); } set { Set(value); } }

        #endregion

        #region [Property] Command

        /// <summary>
        /// 読み込み時の処理
        /// </summary>
        public ICommand LoadedCommand { get; }

        /// <summary>
        /// キー押下の処理
        /// </summary>
        public ICommand PreviewKeyDownCommand { get; }

        /// <summary>
        /// 終了時の処理
        /// </summary>
        public ICommand ClosingCommand { get; }

        /// <summary>
        /// 画面遷移処理
        /// </summary>
        public ICommand ChangePageCommand { get; }

        #endregion

        #region [Method] Command

        internal void Loaded()
        {
            ChangePage(MenuType.Main);
        }

        internal void PreviewKeyDown(KeyEventArgs e)
        {
        }

        internal bool CanChangePage(MenuType menuType)
        {
            return (menuType != CurrentMenu);
        }

        internal void ChangePage(MenuType menuType)
        {
            var value = CurrentMenu - menuType;

            SlideValue = (byte)Math.Abs(value);

            if (value.IsPlus())
            {
                Previous = true;
            }
            else
            {
                Next = true;
            }

            CurrentMenu = menuType;
        }

        #endregion
    }
}