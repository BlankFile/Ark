using System.Windows;

namespace Ark.WPF.Infra.Interactivity.Message
{
    /// <summary> 
    /// Messengerの通知イベント用のイベント引数クラス。
    /// </summary> 
    public class ChangeWindowStateEventArgs : MessageEventArgs
    {
        /// <summary>
        /// ウインドウの状態
        /// </summary>
        public WindowState State { get; private set; }

        /// <summary>
        /// ChangeWindowStateEventArgs クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="state"></param>
        public ChangeWindowStateEventArgs(WindowState state)
            : base(MessageType.ChangeWindowState)
        {
            State = state;
        }
    }
}