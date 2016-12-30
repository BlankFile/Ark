
namespace Ark.WPF.Infra.Converters
{
    /// <summary>
    /// 真偽値を反転するコンバーターを表します。
    /// </summary>
    public class InverseBoolConverter : SimpleConverter<bool, bool>
    {
        /// <summary>
        /// 変換処理を実行します。
        /// </summary>
        /// <param name="value">変換する値</param>
        /// <returns>変換後の値</returns>
        protected override bool Convert(bool value)
        {
            return !value;
        }

    }
}
