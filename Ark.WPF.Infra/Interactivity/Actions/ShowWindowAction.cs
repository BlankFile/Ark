using System.Windows;
using System.Windows.Interactivity;

namespace Ark.WPF.Infra.Interactivity.Actions
{
    /// <summary>
    /// ダイアログを表示します。
    /// </summary>
    public class ShowWindowAction : TriggerAction<DependencyObject>
    {
        #region [InnerClass]

        /// <summary>
        /// <see cref="ShowWindowAction"/> に渡すパラメータを表します。
        /// </summary>
        public class ShowWindowActionArgs
        {
            /// <summary>
            /// 表示するウィンドウを取得または設定します。
            /// </summary>
            public Window View { get; set; }

            /// <summary>
            /// モーダルで表示するかどうかを示す値を取得または設定します。
            /// </summary>
            public bool IsModal { get; set; }

            /// <summary>
            /// ウィンドウを閉じた結果を取得します。
            /// </summary>
            public bool Result { get; internal set; }
        }

        #endregion [InnerClass]

        /// <summary>
        /// アクションを実行します。
        /// </summary>
        /// <param name="parameter">パラメーター</param>
        protected override void Invoke(object parameter)
        {
            var window = AssociatedObject as Window ?? Window.GetWindow(AssociatedObject);

            if (window == null)
                return;

            var args = parameter as ShowWindowActionArgs;

            if (args == null)
                return;

            if (args.View != null)
            {
                args.View.Owner = window;

                if (args.IsModal)
                {
                    args.Result = args.View.ShowDialog() ?? false;
                }
                else
                {
                    args.View.Show();
                }
            }
        }
    }
}