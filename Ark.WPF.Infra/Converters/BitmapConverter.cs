using System.Windows.Media.Imaging;
using Ark.WPF.Infra.Converters;
using Ark.WPF.Infra.Media;

namespace Ark.WPF.Infra.Converters
{
    /// <summary>
    /// URIをビットマップに変換するコンバーターを表します。
    /// </summary>
    public class BitmapConverter : SimpleConverter<string, BitmapImage>
    {
        /// <summary>
        /// 変換処理を実行します。
        /// </summary>
        /// <param name="value">変換する値</param>
        /// <returns>変換後の値</returns>
        protected override BitmapImage Convert(string value)
        {
            return ImageManager.CreateBitmap(value);
        }
    }
}
