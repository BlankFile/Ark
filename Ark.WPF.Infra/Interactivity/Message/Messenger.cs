using System;

namespace Ark.WPF.Infra.Interactivity.Message
{
    /// <summary> 
    /// 画面に処理を委譲するためのオブジェクト。 
    /// </summary> 
    public class Messenger
    {
        /// <summary>
        /// メッセージ通知イベント
        /// </summary>
        public event EventHandler<MessageEventArgs> MessageEvent;

        /// <summary>
        /// 画面に指定した処理を委譲します。 
        /// </summary>
        /// <param name="e">パラメーター</param>
        public void Send<T>(T e) where T : MessageEventArgs
        {
            var h = MessageEvent;

            if (h != null)
            {
                h(this, e);
            }
        }

        /// <summary>
        /// 画面に指定した処理を委譲し、結果を返却します。 
        /// </summary>
        /// <param name="e">パラメーター</param>
        public T SendReceive<T>(T e) where T : MessageEventArgs
        {
            Send(e);

            return e;
        }

    }
}