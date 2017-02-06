
namespace Ark.WPF.Infra.Interactivity.Message
{
    /// <summary> 
    /// Messengerの通知イベント用のイベント引数クラス。
    /// </summary> 
    public class SetFocusEventArgs : MessageEventArgs
    {
        #region プロパティ

        /// <summary>
        /// フォーカスを設定するコントロールの名称または DataContext
        /// </summary>
        public object Target { get; private set; }

        /// <summary>
        /// フォーカスの際に全選択するかどうか
        /// </summary>
        public bool IsSelectAll { get; private set; }

        #endregion

        /// <summary>
        /// SetFocusEventArgs クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="target">フォーカスを設定するコントロールの名称または DataContext</param>
        /// <param name="isSelectAll">フォーカスの際に全選択するかどうか</param>
        public SetFocusEventArgs(object target, bool isSelectAll = false)
            : base(MessageType.SetFocus)
        {
            Target = target;
            IsSelectAll = isSelectAll;
        }
    }
}