using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using Ark.WPF.Infra.Interactivity;
using Ark.WPF.Infra.Interactivity.Actions;
using Ark.WPF.Modern.Controls;

namespace Ark.WPF.Modern.Interactivity.Actions
{
    /// <summary>
    /// モダンダイアログを表示します。
    /// </summary>
    public class ShowModernDialogAction : TriggerAction<DependencyObject>
    {
        #region [InnerClass]

        /// <summary>
        /// <see cref="ShowModernDialogAction"/> に渡すパラメータを表します。
        /// </summary>
        public class ShowModernDialogActionArgs
        {
            /// <summary>
            /// 表示するコンテンツを取得または設定します。
            /// </summary>
            public object View { get; set; }

            /// <summary>
            /// 表示するコンテンツに渡すパラメータを取得または設定します。
            /// </summary>
            public object Parameter { get; set; }

            /// <summary>
            /// モーダルで表示するかどうかを示す値を取得または設定します。
            /// </summary>
            public bool IsModal { get; set; }

            /// <summary>
            /// 表示/表示時のエフェクトを取得または設定します。
            /// </summary>
            public PopupWindow.EffectType Effect { get; set; }
        }

        #endregion

        /// <summary>
        /// アクションを実行します。
        /// </summary>
        /// <param name="parameter">パラメーター</param>
        protected override void Invoke(object parameter)
        {
            var window = AssociatedObject as Window ?? Window.GetWindow(AssociatedObject);
            var args = parameter as ShowModernDialogActionArgs;

            if (window != null && args != null)
            {
                var content = args.View as FrameworkElement;

                InteractionSupport.SetTransferObject(content, args.Parameter);

                if (args.IsModal)
                {
                    PopupWindow.ShowDialog(window, content, args.Effect);
                }
                else
                {
                    PopupWindow.Show(window, content, args.Effect);
                }
            }
        }
    }
}