using System.Windows;
using System.Windows.Interactivity;
using Ark.WPF.Modern.Controls;

namespace Ark.WPF.Modern.Interactivity.Actions
{
    /// <summary>
    /// ビジーダイアログを表示します。
    /// </summary>
    public class ShowBusyDialogAction : TriggerAction<DependencyObject>
    {
        private LoadingCircle _busy;

        /// <summary>
        /// アクションを実行します。
        /// </summary>
        /// <param name="parameter">パラメーター</param>
        protected async override void Invoke(object parameter)
        {
            var window = AssociatedObject as Window ?? Window.GetWindow(AssociatedObject);

            if (window != null && parameter is bool)
            {
                var show = (bool)parameter;

                if (show)
                {
                    _busy = new LoadingCircle();

                    PopupWindow.Show(window, new LoadingCircle(), PopupWindow.EffectType.Fade, new PopupWindowOption
                    {
                        CanClickClose = false,
                        UseCloseAnimation = false
                    });
                }
                else
                {
                    await PopupWindow.CloseDialog(window);
                }
            }
        }
    }
}