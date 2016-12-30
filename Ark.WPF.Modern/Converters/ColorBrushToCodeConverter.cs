using System.Windows.Media;
using Ark.WPF.Infra.Converters;
using Ark.WPF.Modern.Media.Drawing;

namespace Ark.WPF.Modern.Converters
{
    /// <summary>
    /// ブラシを色コード文字列に変換するコンバーターを表します。
    /// </summary>
    public class ColorBrushToCodeConverter : SimpleConverter<SolidColorBrush, string>
    {
        /// <summary>
        /// 変換処理を実行します。
        /// </summary>
        /// <param name="value">変換する値</param>
        /// <returns>変換後の値</returns>
        protected override string Convert(SolidColorBrush value)
        {
            if (value == null)
            {
                return ColorCode.Transparent;
            }

            return value.Color.ToString();
        }
    }
}