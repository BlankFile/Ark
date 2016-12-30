using System.Windows;
using System.Windows.Interactivity;

namespace Ark.WPF.Infra.Interactivity.Actions
{
    /// <summary>
    /// 現在アクティブなWindowのサイズを最大化・最小化・通常に戻します。
    /// </summary>
    public class ChangeWindowStateAction : TriggerAction<DependencyObject>
    {
        #region [Constructor]

        /// <summary>
        /// <see cref="ChangeWindowStateAction"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public ChangeWindowStateAction() { }

        /// <summary>
        /// <see cref="ChangeWindowStateAction"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="state">ウインドウの状態</param>
        public ChangeWindowStateAction(WindowState state)
        {
            State = state;
        }

        #endregion [Constructor]

        #region [Property] Dependency

        /// <summary>
        /// <see cref="State"/> 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty StateProperty = DependencyProperty.Register(
            nameof(State), typeof(WindowState), typeof(ChangeWindowStateAction), new PropertyMetadata(WindowState.Normal));

        /// <summary>
        /// ウインドウの状態を表します。
        /// </summary>
        public WindowState State { get { return (WindowState)GetValue(StateProperty); } set { SetValue(StateProperty, value); } }

        #endregion [Property] Dependency

        /// <summary>
        /// アクションを実行します。
        /// </summary>
        /// <param name="parameter">パラメーター</param>
        protected override void Invoke(object parameter)
        {
            var window = AssociatedObject as Window ?? Window.GetWindow(AssociatedObject);

            if (window != null)
            {
                var state = State;

                if (parameter is WindowState)
                {
                    state = (WindowState)parameter;
                }

                window.WindowState = state;
            }
        }
    }
}