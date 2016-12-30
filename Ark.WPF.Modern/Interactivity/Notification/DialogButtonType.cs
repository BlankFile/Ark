namespace Ark.WPF.Modern.Interactivity.Notification
{
    /// <summary>
    /// ダイアログに表示するボタンの種類を表します。
    /// </summary>
    public enum DialogButtonType
    {
        /// <summary> OK </summary>
        Ok = 0x01,

        /// <summary> Ok, Cancel </summary>
        OkCancel,

        /// <summary> Yes, No </summary>
        YesNo,

        /// <summary> Yes, No, Cancel </summary>
        YesNoCancel,

        /// <summary> Close </summary>
        Close,

        /// <summary> none </summary>
        None
    }
}