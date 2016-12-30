namespace Ark.WPF.Infra.Mvvm
{
    /// <summary>
    /// データ検証の結果を表します。
    /// </summary>
    public class ValidationInfo
    {
        /// <summary>
        /// プロパティ変更処理を実行するかどうか
        /// </summary>
        public bool IsContinue { get; set; }

        /// <summary>
        /// エラーメッセージ
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// <see cref="ValidationInfo"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="errorMessage">エラーメッセージ</param>
        /// <param name="isContinue">継続フラグ</param>
        public ValidationInfo(string errorMessage, bool isContinue = false)
        {
            ErrorMessage = errorMessage;
            IsContinue = isContinue;
        }
    }
}
