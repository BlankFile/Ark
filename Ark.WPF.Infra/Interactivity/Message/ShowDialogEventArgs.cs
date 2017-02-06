using Ark.WPF.Infra.Mvvm;

namespace Ark.WPF.Infra.Interactivity.Message
{
    /// <summary> 
    /// Messengerの通知イベント用のイベント引数クラス。
    /// </summary> 
    public class ShowDialogEventArgs : MessageEventArgs
    {
        #region プロパティ

        /// <summary>
        /// URI
        /// </summary>
        public string Uri { get; private set; }

        /// <summary>
        /// ViewModel
        /// </summary>
        public ViewModel ViewModel { get; private set; }

        /// <summary>
        /// モーダル表示フラグ
        /// </summary>
        public bool IsModal { get; private set; }

        /// <summary>
        /// 結果
        /// </summary>
        public bool Result { get; set; }

        #endregion

        /// <summary>
        /// ShowDialogEventArgs クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="uri">URI</param>
        /// <param name="viewModel">ViewModel</param>
        /// <param name="isModal">モーダル表示フラグ</param>
        public ShowDialogEventArgs(string uri, ViewModel viewModel, bool isModal = true)
            : base(MessageType.ShowDialog)
        {
            Uri = uri;
            ViewModel = viewModel;
            IsModal = isModal;
        }
    }
}