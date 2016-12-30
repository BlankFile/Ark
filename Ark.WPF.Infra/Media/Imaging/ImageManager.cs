using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Ark.WPF.Infra.Media
{
    /// <summary>
    /// 画像を管理する機能を提供します。
    /// </summary>
    public static class ImageManager
    {
        private static object _lockObject = new object();

        /// <summary>
        /// ビットマップを生成します。
        /// </summary>
        /// <param name="uri">URI</param>
        /// <returns>ビットマップ</returns>
        public static BitmapImage CreateBitmap(string uri)
        {
            var image = (BitmapImage)null;

            using (var stream = CreateBitmapStream(uri))
            {
                if (stream != null)
                {
                    image = CreateBitmapFromStream(stream);
                }
            }

            return image;
        }

        /// <summary>
        /// ビットマップのストリームを生成します。
        /// </summary>
        /// <param name="uri">URI</param>
        /// <returns>ビットマップのストリーム</returns>
        public static Stream CreateBitmapStream(string uri)
        {
            if (uri.IsNullOrEmpty())
                return null;

            var isResource = uri.Contains("pack://application");

            if (isResource)
            {
                lock (_lockObject)
                {
                    return Application.GetResourceStream(new Uri(uri)).Stream;
                }
            }

            if (!File.Exists(uri))
                return null;

            if (new FileInfo(uri).Length <= 1000)
                return null;

            return File.OpenRead(uri);
        }

        /// <summary>
        /// ストリームからビットマップを生成します。
        /// </summary>
        /// <param name="stream">ストリーム</param>
        /// <returns>ビットマップ</returns>
        private static BitmapImage CreateBitmapFromStream(Stream stream)
        {
            var bi = new BitmapImage();

            bi.BeginInit();
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.StreamSource = stream;
            bi.EndInit();
            bi.Freeze();

            return bi;
        }
    }
}
