using System.Windows;

namespace Ark.WPF.Infra.Interactivity.Message
{
    /// <summary> 
    /// Messengerの通知イベント用のイベント引数クラス。
    /// </summary> 
    public class ShowMessageEventArgs : MessageEventArgs
    {
        #region プロパティ

        /// <summary>
        /// タイトル
        /// </summary>
        public string Caption { get; internal set; }

        /// <summary>
        /// メッセージ
        /// </summary>
        public string Message { get; internal set; }

        /// <summary>
        /// ボタンの種類
        /// </summary>
        public MessageBoxButton Button { get; internal set; }

        /// <summary>
        /// アイコンの種類
        /// </summary>
        public MessageBoxImage Image { get; internal set; }

        /// <summary>
        /// 結果
        /// </summary>
        public bool Result { get; set; }

        #endregion

        /// <summary>
        /// ShowMessageEventArgs クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="caption">タイトル</param>
        /// <param name="message">メッセージ</param>
        /// <param name="image">ボタンの種類</param>
        /// <param name="button">アイコンの種類</param>
        public ShowMessageEventArgs(string caption, string message, MessageBoxImage image, MessageBoxButton button)
            : base(MessageType.ShowMessage)
        {
            Caption = caption;
            Message = message;
            Image = image;
            Button = button;
        }
    }
}