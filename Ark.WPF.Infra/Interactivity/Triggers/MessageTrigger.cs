using System;
using Ark.WPF.Infra.Interactivity.Message;
using Ark.WPF.Infra.Interactivity.Triggers.Actions;

namespace Ark.WPF.Infra.Interactivity.Triggers
{
    /// <summary>
    /// メッセンジャーイベントを受信し、指定したアクションを実行します。
    /// </summary>
    public class MessageTrigger : System.Windows.Interactivity.EventTrigger
    {
        /// <summary>
        /// MessageTrigger クラスの新しいインスタンスを初期化します。
        /// </summary>
        public MessageTrigger() : base("MessageEvent") { }

        /// <summary>
        /// 指定したパラメーターに基づくアクションを実行します。
        /// </summary>
        /// <param name="e">パラメーター</param>
        protected override void OnEvent(EventArgs e)
        {
            if (AssociatedObject == null)
                return;

            var action = GetAction(e);

            if (action == null)
                return;

            try
            {
                Actions.Add(action);

                InvokeActions(e);
            }
            finally
            {
                Actions.Remove(action);
            }
        }

        /// <summary>
        /// 指定されたパラメーターから、実行するアクションを取得します。
        /// </summary>
        /// <param name="e">パラメーター</param>
        /// <returns>アクション</returns>
        protected virtual MessageTriggerAction GetAction(object e)
        {
            var args = e as MessageEventArgs;

            if (args == null)
                return null;

            switch (args.Type)
            {
                case MessageType.CloseWindow:
                    return new CloseWindowAction();
                case MessageType.ChangeWindowState:
                    return new ChangeWindowStateAction();
                case MessageType.SetFocus:
                    return new SetFocusAction();
                case MessageType.OpenFileDialog:
                    return new OpenFileDialogAction();
                case MessageType.OpenFolderDialog:
                    return new OpenFolderDialogAction();
                case MessageType.SaveFileDialog:
                    return new SaveFileDialogAction();
                case MessageType.ShowMessage:
                    return new ShowMessageAction();
                case MessageType.ShowDialog:
                    return new ShowDialogAction();
            }

            return null;
        }
    }
}