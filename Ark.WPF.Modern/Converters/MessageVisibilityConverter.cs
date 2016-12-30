using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Ark.WPF.Modern.Interactivity.Notification;

namespace Ark.WPF.Modern.Converters
{
    /// <summary>
    /// メッセージダイアログのボタンの表示を設定するコンバーターを表します。
    /// </summary>
    public class MessageVisibilityConverter : IValueConverter
    {
        private const string OK = "Ok";
        private const string YES = "Yes";
        private const string NO = "No";
        private const string CANCEL = "Cancel";
        private const string CLOSE = "Close";

        /// <summary>
        /// 変換処理を実行します。
        /// </summary>
        /// <param name="value">変換する値</param>
        /// <param name="targetType">型</param>
        /// <param name="parameter">パラメーター</param>
        /// <param name="culture">カルチャ</param>
        /// <returns>変換後の値</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
                return Visibility.Collapsed;

            var buttonType = parameter.ToString();

            switch ((DialogButtonType)value)
            {
                case DialogButtonType.Ok:
                    return (buttonType == OK) ? Visibility.Visible : Visibility.Collapsed;

                case DialogButtonType.OkCancel:
                    return (buttonType == OK || buttonType == CANCEL) ? Visibility.Visible : Visibility.Collapsed;

                case DialogButtonType.YesNo:
                    return (buttonType == YES || buttonType == NO) ? Visibility.Visible : Visibility.Collapsed;

                case DialogButtonType.YesNoCancel:
                    return (buttonType == YES || buttonType == NO || buttonType == CANCEL) ? Visibility.Visible : Visibility.Collapsed;

                case DialogButtonType.Close:
                    return (buttonType == CLOSE) ? Visibility.Visible : Visibility.Collapsed;

                default:
                    return Visibility.Collapsed;
            }
        }

        /// <summary>
        /// 逆変換処理を実行します。
        /// </summary>
        /// <param name="value">逆変換する値</param>
        /// <param name="targetType">型</param>
        /// <param name="parameter">パラメーター</param>
        /// <param name="culture">カルチャ</param>
        /// <returns>逆変換後の値</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception();
        }

    }
}
