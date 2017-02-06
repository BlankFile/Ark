using System.Windows;
using Ark.WPF.Infra.Interactivity.Messengers;
using Ark.WPF.Modern.Interactivity.Actions;

namespace Ark.WPF.Modern.Interactivity.Messengers
{
    /// <summary>
    /// ビジーダイアログを表示するためのメッセンジャーを表します。
    /// </summary>
    public class ShowBusyDialogMessenger : MessengerBase<DependencyObject, ShowBusyDialogAction>
    {
        private bool _isBusy;

        /// <summary>
        /// ビジー状態かどうかを取得または設定します。
        /// </summary>
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;

                Publish(value);
            }
        }
    }
}