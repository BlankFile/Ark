
namespace Ark.WPF.Infra.Interactivity.Message
{
    /// <summary>
    /// メッセージの種類を示します。
    /// </summary>
    public enum MessageType
    {
        /// <summary> 何もしません。 </summary>
        None = 0x00,

        /// <summary> メッセージを表示します。 </summary>
        ShowMessage,

        /// <summary> ダイアログを表示します。 </summary>
        ShowDialog,

        /// <summary> 現在アクティブなWindowを終了します。 </summary>
        CloseWindow,

        /// <summary> 現在アクティブなWindowのサイズを最大化・最小化・通常に戻します。 </summary>
        ChangeWindowState,

        /// <summary> フォルダー選択ダイアログを表示します。 </summary>
        OpenFolderDialog,

        /// <summary> ファイル選択ダイアログを表示します。 </summary>
        OpenFileDialog,

        /// <summary> ファイル保存ダイアログを表示します。 </summary>
        SaveFileDialog,

        /// <summary> 任意のコントロールにフォーカスを設定します。 </summary>
        SetFocus,

    }

}
