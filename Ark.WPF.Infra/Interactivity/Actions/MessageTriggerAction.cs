using System.Windows;
using System.Windows.Interactivity;

namespace Ark.WPF.Infra.Interactivity.Triggers.Actions
{
    /// <summary>
    /// クライアントへのメッセージ通知処理を実行します。
    /// </summary>
    public abstract class MessageTriggerAction : TriggerAction<FrameworkElement>
    {
        /// <summary>
        /// アクションを実行します。
        /// </summary>
        /// <param name="parameter">パラメーター</param>
        protected override void Invoke(object parameter)
        {
        }
    }
}
