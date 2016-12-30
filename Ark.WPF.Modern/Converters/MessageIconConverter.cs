using System.Windows.Media.Imaging;
using Ark.WPF.Infra.Converters;
using Ark.WPF.Modern.Interactivity.Notification;
using Ark.WPF.Modern.Media.Imaging;

namespace Ark.WPF.Modern.Converters
{
    /// <summary>
    /// メッセージのアイコンからビットマップに変換するコンバーターを表します。
    /// </summary>
    public class MessageIconConverter : SimpleConverter<MessageDialogImage, BitmapImage>
    {
        /// <summary>
        /// 変換処理を実行します。
        /// </summary>
        /// <param name="value">変換する値</param>
        /// <returns>変換後の値</returns>
        protected override BitmapImage Convert(MessageDialogImage value)
        {
            switch (value)
            {
                case MessageDialogImage.Information:
                    return BitmapResource.Information;

                case MessageDialogImage.Question:
                    return BitmapResource.Question;

                case MessageDialogImage.Warning:
                    return BitmapResource.Warning;

                case MessageDialogImage.Error:
                    return BitmapResource.Error;

                default:
                    return null;
            }
        }
    }
}