using System.Windows;
using System.Windows.Interactivity;

namespace Ark.WPF.Infra.Interactivity.Actions
{
    /// <summary>
    /// メッセージを表示します。
    /// </summary>
    public class ShowMessageAction : TriggerAction<DependencyObject>
    {
        #region [InnerClass]

        /// <summary>
        /// <see cref="ShowMessageAction"/> に渡すパラメータを表します。
        /// </summary>
        public class ShowMessageActionArgs
        {
            /// <summary>
            /// メッセージを取得または設定します。
            /// </summary>
            public string Message { get; set; }

            /// <summary>
            /// タイトルを取得または設定します。
            /// </summary>
            public string Caption { get; set; }

            /// <summary>
            /// ボタンの種類を取得または設定します。
            /// </summary>
            public MessageBoxButton Button { get; set; }

            /// <summary>
            /// アイコンの種類を取得または設定します。
            /// </summary>
            public MessageBoxImage Image { get; set; }

            /// <summary>
            /// メッセージボックスの選択結果を取得します。
            /// </summary>
            public bool Result { get; internal set; }
        }

        #endregion

        /// <summary>
        /// アクションを実行します。
        /// </summary>
        /// <param name="parameter">パラメーター</param>
        protected override void Invoke(object parameter)
        {
            var window = AssociatedObject as Window;

            if (window == null)
                return;

            var args = parameter as ShowMessageActionArgs;

            if (args == null)
                return;

            var result = MessageBox.Show(window, args.Message, args.Caption, args.Button, args.Image);

            switch (result)
            {
                case MessageBoxResult.OK:
                case MessageBoxResult.Yes:
                    args.Result = true;
                    break;
                default:
                    args.Result = false;
                    break;
            }
        }
    }
}