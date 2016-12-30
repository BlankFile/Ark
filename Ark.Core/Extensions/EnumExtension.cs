using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace System
{
    /// <summary>
    /// <see cref="Enum"/> クラスの拡張メソッドを定義します。
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// UI でのフィールドの表示に使用される値を返します。
        /// </summary>
        /// <param name="value"><see cref="DisplayAttribute"/> が付与されている列挙値</param>
        /// <returns><see cref="DisplayAttribute"/> に設定した内容</returns>
        public static string GetName(this Enum value)
        {
            var attr = GetDisplayAttribute(value);

            return attr?.GetName() ?? string.Empty;
        }

        /// <summary>
        /// UI でのフィールドの簡略表示に使用される値を返します。
        /// </summary>
        /// <param name="value"><see cref="DisplayAttribute"/> が付与されている列挙値</param>
        /// <returns><see cref="DisplayAttribute"/> に設定した内容</returns>
        public static string GetShortName(this Enum value)
        {
            var attr = GetDisplayAttribute(value);

            return attr?.GetShortName() ?? string.Empty;
        }

        /// <summary>
        ///  UI に説明を表示するために使用される値を返します。
        /// </summary>
        /// <param name="value"><see cref="DisplayAttribute"/> が付与されている列挙値</param>
        /// <returns><see cref="DisplayAttribute"/> に設定した内容</returns>
        public static string GetDescription(this Enum value)
        {
            var attr = GetDisplayAttribute(value);

            return attr?.GetDescription() ?? string.Empty;
        }

        /// <summary>
        /// 列挙値に付与された <see cref="DisplayAttribute"/> を取得します。
        /// </summary>
        /// <param name="value">列挙値</param>
        /// <returns><see cref="DisplayAttribute"/></returns>
        private static DisplayAttribute GetDisplayAttribute(Enum value)
        {
            return value?.GetType()?.GetCustomAttribute<DisplayAttribute>();
        }
    }
}
