
namespace Ark.WPF.Infra.Interactivity.Message
{
    /// <summary> 
    /// Messengerの通知イベント用のイベント引数クラス。
    /// </summary> 
    public class OpenFolderEventArgs : MessageEventArgs
    {
        #region プロパティ

        /// <summary>
        /// タイトル
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// パス
        /// </summary>
        public string SelectedPath { get; set; }

        /// <summary>
        /// 結果
        /// </summary>
        public bool Response { get; set; }

        #endregion

        /// <summary>
        /// OpenFolderEventArgs クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="description">タイトル</param>
        public OpenFolderEventArgs(string description)
            : base(MessageType.OpenFolderDialog)
        {
            Description = description;
        }
    }
}