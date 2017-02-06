using System.Windows;
using Ark.WPF.Infra.Interactivity.Actions;

namespace Ark.WPF.Infra.Interactivity.Messengers
{
    /// <summary>
    /// ウィンドウを閉じるためのメッセンジャーを表します。
    /// </summary>
    public class CloseWindowMessenger : MessengerBase<DependencyObject, CloseWindowAction>
    {
        /// <summary>
        /// ウィンドウを閉じます。
        /// </summary>
        public void Run() => Publish();
    }
}