using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace Ark.WPF.Infra.Interactivity.Actions
{
    /// <summary>
    /// 任意のコントロールにフォーカスを設定します。
    /// </summary>
    public class SetFocusAction : TriggerAction<DependencyObject>
    {
        #region [InnerClass]

        /// <summary>
        /// <see cref="SetFocusAction"/> に渡すパラメータを表します。
        /// </summary>
        public class SetFocusActionArgs
        {
            /// <summary>
            /// フォーカスの対象を取得または設定します。
            /// </summary>
            public object Target { get; set; }

            /// <summary>
            /// テキストを全選択するかどうかを示す値を取得または設定します。
            /// </summary>
            public bool IsSelectAll { get; set; }
        }

        #endregion

        /// <summary>
        /// アクションを実行します。
        /// </summary>
        /// <param name="parameter">パラメーター</param>
        protected override void Invoke(object parameter)
        {
            var window = AssociatedObject as Window ?? Window.GetWindow(AssociatedObject);

            if (window == null)
                return;

            var args = parameter as SetFocusActionArgs;

            if (args == null)
                return;

            if (args.Target == null)
                return;

            var target = FindControl(window, args.Target);

            if (target == null)
                return;

            if (!target.Focus())
                return;

            if (args.IsSelectAll && target is TextBox)
            {
                ((TextBox)target).SelectAll();
            }
        }

        /// <summary>
        /// コントロールを検索します。
        /// </summary>
        /// <param name="parent">親要素</param>
        /// <param name="findValue">検索する値</param>
        /// <returns>コントロール</returns>
        private static FrameworkElement FindControl(DependencyObject parent, object findValue)
        {
            if (parent == null || findValue == null)
            {
                return null;
            }

            var foundChild = (FrameworkElement)null;
            var childrenCount = VisualTreeHelper.GetChildrenCount(parent);

            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                var frameworkElement = child as FrameworkElement;

                if (frameworkElement != null)
                {
                    if (frameworkElement.Focusable)
                    {
                        if ((frameworkElement.DataContext == findValue || frameworkElement.Name == findValue.ToString()))
                        {
                            foundChild = (FrameworkElement)child;
                            break;
                        }
                    }
                }

                foundChild = FindControl(child, findValue);

                if (foundChild != null)
                {
                    break;
                }
            }

            return foundChild;
        }
    }
}
