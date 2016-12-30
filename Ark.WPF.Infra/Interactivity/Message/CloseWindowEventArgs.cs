
namespace Ark.WPF.Infra.Interactivity.Message
{
    /// <summary> 
    /// Messengerの通知イベント用のイベント引数クラス。
    /// </summary> 
    public class CloseWindowEventArgs : MessageEventArgs
    {
        /// <summary>
        /// CloseWindowEventArgs クラスの新しいインスタンスを初期化します。
        /// </summary>
        public CloseWindowEventArgs() : base(MessageType.CloseWindow) { }
    }
}