using System;
using System.Windows.Media.Imaging;
using Ark.WPF.Infra.Media;

namespace Ark.WPF.Modern.Media.Imaging
{
    /// <summary>
    /// ビットマップのリソースを表します。
    /// </summary>
    public static class BitmapResource
    {
        private const string IconUrl = "pack://application:,,,/Ark.WPF.Modern;component/Content/Icon/Message/{0}.png";

        /// <summary>
        /// 「情報」を意味するアイコン。
        /// </summary>
        public static BitmapImage Information { get; } = ImageManager.CreateBitmap(IconUrl.Put("Information"));

        /// <summary>
        /// 「質問」を意味するアイコン。
        /// </summary>
        public static BitmapImage Question { get; } = ImageManager.CreateBitmap(IconUrl.Put("Question"));

        /// <summary>
        /// 「警告」を意味するアイコン。
        /// </summary>
        public static BitmapImage Warning { get; } = ImageManager.CreateBitmap(IconUrl.Put("Warning"));

        /// <summary>
        /// 「エラー」を意味するアイコン。
        /// </summary>
        public static BitmapImage Error { get; } = ImageManager.CreateBitmap(IconUrl.Put("Error"));

    }
}
