using System.Windows;

namespace Ark.WPF.Infra.Converters
{
    /// <summary>
    /// 真偽値の逆を表示状態に変換するコンバーターを表します。
    /// 真偽値が true の場合、<see cref="Visibility.Hidden"/> を返します。<para/>
    /// 真偽値が false の場合、<see cref="Visibility.Visible"/> を返します。
    /// </summary>
    public class InverseBoolToHiddenConverter : SimpleConverter<bool, Visibility>
    {
        /// <summary>
        /// 変換処理を実行します。
        /// </summary>
        /// <param name="value">変換する値</param>
        /// <returns>変換後の値</returns>
        protected override Visibility Convert(bool value)
        {
            return value ? Visibility.Hidden : Visibility.Visible;
        }

    }
}
