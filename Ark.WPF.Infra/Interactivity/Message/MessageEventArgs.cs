
namespace Ark.WPF.Infra.Interactivity.Message
{
    /// <summary> 
    /// Messengerの通知イベント用のイベント引数クラス。
    /// </summary> 
    public abstract class MessageEventArgs : System.EventArgs
    {
        /// <summary>
        /// メッセージの種類
        /// </summary>
        public MessageType Type { get; protected set; }

        /// <summary>
        /// MessageEventArgs クラスの新しいインスタンスを初期化します。
        /// </summary>
        public MessageEventArgs()
        {

        }

        /// <summary>
        /// MessageEventArgs クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="type">メッセージの種類</param>
        public MessageEventArgs(MessageType type)
        {
            Type = type;
        }
    }
}