﻿using System.Windows;
using Ark.WPF.Infra.Interactivity.Messengers;
using Ark.WPF.Modern.Interactivity.Actions;

namespace Ark.WPF.Modern.Interactivity.Messengers
{
    /// <summary>
    /// モダンダイアログを閉じるためのメッセンジャーを表します。
    /// </summary>
    public class CloseModernDialogMessenger : MessengerBase<DependencyObject, CloseModernDialogAction>
    {
        /// <summary>
        /// ダイアログの指定したコントロールを閉じます。
        /// </summary>
        /// <param name="content">コントロール</param>
        public void Close(DependencyObject content = null)
        {
            Publish(content ?? Target);
        }

        /// <summary>
        /// ダイアログを閉じます。
        /// </summary>
        public void CloseDialog()
        {
            Publish();
        }
    }
}