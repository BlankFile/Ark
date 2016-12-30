using System;
using System.Globalization;
using System.Windows.Data;

namespace Ark.WPF.Infra.Converters
{
    /// <summary>
    /// 値だけを使用する簡易なコンバーターを表します。
    /// </summary>
    /// <typeparam name="T1">変換前の型</typeparam>
    /// <typeparam name="T2">変換後の型</typeparam>
    public abstract class SimpleConverter<T1, T2> : IValueConverter
    {
        #region [Method] protected virtual

        /// <summary>
        /// 変換処理を実行します。
        /// </summary>
        /// <param name="value">変換する値</param>
        /// <returns>変換後の値</returns>
        protected virtual T2 Convert(T1 value)
        {
            return default(T2);
        }

        /// <summary>
        /// 逆変換処理を実行します。
        /// </summary>
        /// <param name="value">逆変換する値</param>
        /// <returns>逆変換後の値</returns>
        protected virtual T1 ConvertBack(T2 value)
        {
            return default(T1);
        }

        #endregion

        #region [InterfaceImpl] IValueConverter

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert((T1)value);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertBack((T2)value);
        }

        #endregion

    }
}
