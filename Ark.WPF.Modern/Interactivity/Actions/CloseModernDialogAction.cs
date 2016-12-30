using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using Ark.WPF.Infra.Interactivity.Actions;
using Ark.WPF.Modern.Controls;

namespace Ark.WPF.Modern.Interactivity.Actions
{
    /// <summary>
    /// モダンダイアログを閉じます。
    /// </summary>
    public class CloseModernDialogAction : TriggerAction<DependencyObject>
    {
        /// <summary>
        /// アクションを実行します。
        /// </summary>
        /// <param name="parameter">パラメーター</param>
        protected override async void Invoke(object parameter)
        {
            var target = AssociatedObject as FrameworkElement;

            if (target != null)
            {
                var content = parameter as FrameworkElement;

                if (content != null)
                {
                    await PopupWindow.Close(target, content);
                }
                else
                {
                    await PopupWindow.CloseDialog(target);
                }
            }
        }
    }
}