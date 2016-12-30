using System;
using System.Windows.Media;
using Ark.WPF.Infra.Converters;

namespace Ark.WPF.Modern.Converters
{
    /// <summary>
    /// 色コード文字列をカラーに変換するコンバーターを表します。
    /// </summary>
    public class CodeToColorConverter : SimpleConverter<string, Color>
    {
        /// <summary>
        /// 変換処理を実行します。
        /// </summary>
        /// <param name="value">変換する値</param>
        /// <returns>変換後の値</returns>
        protected override Color Convert(string value)
        {
            if (!value.IsNullOrEmpty())
                return value.ToColor();

            return Colors.Transparent;
        }
    }
}
