using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Ark.WPF.Infra.Interactivity.Actions
{
    /// <summary>
    /// コマンドを実行します。
    /// </summary>
    public class CommandTriggerAction : TriggerAction<DependencyObject>
    {
        #region [Property] Dependency

        /// <summary>
        /// <see cref="Command"/> 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            nameof(Command), typeof(ICommand), typeof(CommandTriggerAction), new PropertyMetadata());

        /// <summary>
        /// 実行するコマンドを取得または設定します。
        /// </summary>
        public ICommand Command { get { return (ICommand)GetValue(CommandProperty); } set { SetValue(CommandProperty, value); } }

        /// <summary>
        /// <see cref="Parameter"/> 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty ParameterProperty = DependencyProperty.Register(
            nameof(Parameter), typeof(object), typeof(CommandTriggerAction), new PropertyMetadata());

        /// <summary>
        /// Commandへ渡すパラメーターを取得または設定表します。
        /// </summary>
        public object Parameter { get { return (object)GetValue(ParameterProperty); } set { SetValue(ParameterProperty, value); } }

        #endregion [Property] Dependency

        /// <summary>
        /// 指定したコマンドを実行します。
        /// </summary>
        /// <param name="parameter">パラメーター</param>
        protected override void Invoke(object parameter)
        {
            if (Command != null)
            {
                if (Parameter == null)
                {
                    Command.Execute(parameter);
                }
                else
                {
                    Command.Execute(new
                    {
                        EventArgs = parameter,
                        Parameter = Parameter
                    });
                }
            }
        }
    }
}