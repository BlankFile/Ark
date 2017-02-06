using System.Windows;
using Ark.WPF.Infra.Interactivity.Messengers;
using Ark.WPF.Modern.Interactivity.Actions;
using Ark.WPF.Modern.Interactivity.Notification;

namespace Ark.WPF.Modern.Interactivity.Messengers
{
    /// <summary>
    /// モダンメッセージを表示するためのメッセンジャーを表します。
    /// </summary>
    public class ShowModernMessageMessenger : MessengerBase<DependencyObject, ShowModernMessageAction>
    {
        /// <summary>
        /// 情報メッセージを表示します。
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <param name="button">ボタンの種類</param>
        /// <returns>ボタンの選択結果</returns>
        public bool? Info(string message, DialogButtonType button = DialogButtonType.Ok) => Show(message, MessageDialogImage.Information, button);

        /// <summary>
        /// 質問メッセージを表示します。
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <param name="button">ボタンの種類</param>
        /// <returns>ボタンの選択結果</returns>
        public bool? Question(string message, DialogButtonType button = DialogButtonType.YesNo) => Show(message, MessageDialogImage.Question, button);

        /// <summary>
        /// 警告メッセージを表示します。
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <param name="button">ボタンの種類</param>
        /// <returns>ボタンの選択結果</returns>
        public bool? Warn(string message, DialogButtonType button = DialogButtonType.Ok) => Show(message, MessageDialogImage.Warning, button);

        /// <summary>
        /// エラーメッセージを表示します。
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <param name="button">ボタンの種類</param>
        /// <returns>ボタンの選択結果</returns>
        public bool? Error(string message, DialogButtonType button = DialogButtonType.Ok) => Show(message, MessageDialogImage.Error, button);

        /// <summary>
        /// メッセージを表示します。
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <param name="image">アイコンの種類</param>
        /// <param name="button">ボタンの種類</param>
        /// <returns>ボタンの選択結果</returns>
        private bool? Show(string message, MessageDialogImage image, DialogButtonType button)
        {
            var args = new ShowModernMessageAction.ShowModernMessageActionArgs
            {
                Message = message,
                DialogImage = image,
                ButtonType = button
            };

            Publish(args);

            return args.Result;
        }
    }
}