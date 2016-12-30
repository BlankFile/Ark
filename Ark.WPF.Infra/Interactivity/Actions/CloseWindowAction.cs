using System.Windows;
using System.Windows.Interactivity;

namespace Ark.WPF.Infra.Interactivity.Actions
{
    /// <summary>
    /// Windowを「終了」します。
    /// </summary>
    public class CloseWindowAction : TriggerAction<DependencyObject>
    {
        /// <summary>
        /// アクションを実行します。
        /// </summary>
        /// <param name="parameter">パラメーター</param>
        protected override void Invoke(object parameter)
        {
            var window = AssociatedObject as Window ?? Window.GetWindow(AssociatedObject);

            if (window != null)
            {
                SystemCommands.CloseWindow(window);
            }
        }
    }
}