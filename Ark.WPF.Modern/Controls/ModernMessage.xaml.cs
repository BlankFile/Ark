using System.Windows;
using System.Windows.Controls;
using Ark.WPF.Modern.Interactivity.Notification;

namespace Ark.WPF.Modern.Controls
{
    /// <summary>
    /// ModernMessage.xaml の相互作用ロジック
    /// </summary>
    public partial class ModernMessage : UserControl
    {
        /// <summary>
        /// <see cref="ModernMessage"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public ModernMessage()
        {
            InitializeComponent();
        }

        #region [Property] Dependency

        /// <summary>
        /// <see cref="Message"/> 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(
            nameof(Message), typeof(string), typeof(ModernMessage), new PropertyMetadata(string.Empty));

        /// <summary>
        /// メッセージを取得または設定します。
        /// </summary>
        public string Message { get { return GetValue(MessageProperty) as string; } set { SetValue(MessageProperty, value); } }

        /// <summary>
        /// <see cref="DialogImage"/> 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty DialogImageProperty = DependencyProperty.Register(
            nameof(DialogImage), typeof(MessageDialogImage), typeof(ModernMessage), new PropertyMetadata(MessageDialogImage.Information));

        /// <summary>
        /// アイコンの種類を取得または設定します。
        /// </summary>
        public MessageDialogImage DialogImage { get { return (MessageDialogImage)GetValue(DialogImageProperty); } set { SetValue(DialogImageProperty, value); } }

        /// <summary>
        /// <see cref="ButtonType"/> 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty ButtonTypeProperty = DependencyProperty.Register(
            nameof(ButtonType), typeof(DialogButtonType), typeof(ModernMessage), new PropertyMetadata(DialogButtonType.Ok));

        /// <summary>
        /// ボタンの種類を取得または設定します。
        /// </summary>
        public DialogButtonType ButtonType { get { return (DialogButtonType)GetValue(ButtonTypeProperty); } set { SetValue(ButtonTypeProperty, value); } }

        /// <summary>
        /// <see cref="Result"/> 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty ResultProperty = DependencyProperty.Register(
            nameof(Result), typeof(bool?), typeof(ModernMessage), new PropertyMetadata(null));

        /// <summary>
        /// 選択したボタンの結果を取得または設定します。
        /// </summary>
        public bool? Result { get { return (bool?)GetValue(ResultProperty); } set { SetValue(ResultProperty, value); } }

        #endregion [Property] Dependency
    }
}