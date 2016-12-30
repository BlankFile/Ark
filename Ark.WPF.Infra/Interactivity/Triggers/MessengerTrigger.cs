using System.Windows;
using System.Windows.Interactivity;

namespace Ark.WPF.Infra.Interactivity.Triggers
{
    /// <summary>
    /// メッセンジャーによって処理を呼び出すことのできるオブジェクトを表します。
    /// </summary>
    /// <typeparam name="TControl">メッセンジャーが通知するオブジェクトの型</typeparam>
    public class MessengerTrigger<TControl> : TriggerBase<TControl>
        where TControl : DependencyObject
    {
        /// <summary>
        /// このトリガーに関連する処理を呼び出します。
        /// </summary>
        /// <param name="parameter">引数</param>
        public void Invoke(object parameter)
        {
            InvokeActions(parameter);
        }
    }
}