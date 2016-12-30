using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
        /// メッセージを表示します。
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <param name="image">アイコンの種類</param>
        /// <param name="button">ボタンの種類</param>
        /// <returns>ボタンの選択結果</returns>
        public bool? Show(string message, MessageDialogImage image = MessageDialogImage.Information, DialogButtonType button = DialogButtonType.Ok)
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
