namespace Ark.WPF.Modern.Controls
{
    /// <summary>
    /// ポップアップコントロールの設定情報を表します。
    /// </summary>
    public class PopupWindowOption
    {
        #region [Property] public

        /// <summary>
        /// 背景クリック時に閉じるかどうかを取得または設定します。
        /// </summary>
        public bool CanClickClose { get; set; } = true;

        /// <summary>
        /// 表示時のアニメーションを使用するかどうかを取得または設定します。
        /// </summary>
        public bool UseOpenAnimation { get; set; } = true;

        /// <summary>
        /// 閉じる時のアニメーションを使用するかどうかを取得または設定します。
        /// </summary>
        public bool UseCloseAnimation { get; set; } = true;

        #endregion
    }
}