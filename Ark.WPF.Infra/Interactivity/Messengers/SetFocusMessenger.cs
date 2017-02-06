using System.Windows;
using Ark.WPF.Infra.Interactivity.Actions;

namespace Ark.WPF.Infra.Interactivity.Messengers
{
    /// <summary>
    /// フォーカスを設定するためのメッセンジャーを表します。
    /// </summary>
    public class SetFocusMessenger : MessengerBase<DependencyObject, SetFocusAction>
    {
        /// <summary>
        /// フォーカスを設定します。
        /// </summary>
        /// <param name="target">対象のDataContextもしくはName</param>
        /// <param name="isSelectAll">テクストボックスの場合、全選択するかどうか</param>
        public void Run(object target, bool isSelectAll)
        {
            var args = new SetFocusAction.SetFocusActionArgs
            {
                Target = target,
                IsSelectAll = isSelectAll
            };

            Publish(args);
        }
    }
}