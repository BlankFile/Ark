
namespace Ark.WPF.Infra.Interactivity.Message
{
    /// <summary> 
    /// Messengerの通知イベント用のイベント引数クラス。
    /// </summary> 
    public class SaveFileEventArgs : MessageEventArgs
    {
        #region プロパティ

        /// <summary>
        /// タイトル
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// 拡張子
        /// </summary>
        public string FileExtension { get; private set; }

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
        /// SaveFileEventArgs クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="description">タイトル</param>
        /// <param name="fileExtension">拡張子</param>
        public SaveFileEventArgs(string description, string fileExtension)
            : base(MessageType.SaveFileDialog)
        {
            Description = description;
            FileExtension = fileExtension;
        }
    }
}