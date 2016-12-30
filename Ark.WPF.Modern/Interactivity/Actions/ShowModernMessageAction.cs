using System.Windows;
using System.Windows.Interactivity;
using Ark.WPF.Infra.Interactivity.Actions;
using Ark.WPF.Modern.Controls;
using Ark.WPF.Modern.Interactivity.Notification;

namespace Ark.WPF.Modern.Interactivity.Actions
{
    /// <summary>
    /// モダンメッセージを表示します。
    /// </summary>
    public class ShowModernMessageAction : TriggerAction<DependencyObject>
    {
        #region [InnerClass]

        /// <summary>
        /// <see cref="ShowModernDialogAction"/> に渡すパラメータを表します。
        /// </summary>
        public class ShowModernMessageActionArgs
        {
            /// <summary>
            /// メッセージを取得または設定します。
            /// </summary>
            public string Message { get; set; }

            /// <summary>
            /// アイコンの種類を取得または設定します。
            /// </summary>
            public MessageDialogImage DialogImage { get; set; }

            /// <summary>
            /// ボタンの種類を取得または設定します。
            /// </summary>
            public DialogButtonType ButtonType { get; set; }

            /// <summary>
            /// メッセージを閉じた結果を取得します。
            /// </summary>
            public bool? Result { get; internal set; }
        }

        #endregion

        /// <summary>
        /// アクションを実行します。
        /// </summary>
        /// <param name="parameter">パラメーター</param>
        protected override void Invoke(object parameter)
        {
            var window = AssociatedObject as Window ?? Window.GetWindow(AssociatedObject);

            if (window != null)
            {
                if (parameter is ShowModernMessageActionArgs)
                {
                    var e = parameter as ShowModernMessageActionArgs;

                    var dialog = new ModernMessage
                    {
                        Message = e.Message,
                        DialogImage = e.DialogImage,
                        ButtonType = e.ButtonType
                    };

                    PopupWindow.ShowDialog(window, dialog);

                    e.Result = dialog.Result;
                }
            }
        }
    }
}