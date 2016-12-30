using System;
using System.Windows.Media;
using Ark.WPF.Infra.Converters;

namespace Ark.WPF.Modern.Converters
{
    /// <summary>
    /// 色コード文字列をブラシに変換するコンバーターを表します。
    /// </summary>
    public class CodeToColorBrushConverter : SimpleConverter<string, SolidColorBrush>
    {
        /// <summary>
        /// 変換処理を実行します。
        /// </summary>
        /// <param name="value">変換する値</param>
        /// <returns>変換後の値</returns>
        protected override SolidColorBrush Convert(string value)
        {
            if (!value.IsNullOrEmpty())
            {
                return value.ToBrush();
            }

            return Colors.Transparent.ToBrush();
        }
    }
}