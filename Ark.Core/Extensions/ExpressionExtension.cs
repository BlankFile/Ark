using System.Reflection;

namespace System.Linq.Expressions
{
    /// <summary>
    /// <see cref="Expression"/> クラスの拡張メソッドを定義します。
    /// </summary>
    public static class ExpressionExtension
    {
        /// <summary>
        /// 左辺のプロパティ名を取得します。
        /// </summary>
        /// <param name="e">式ツリー</param>
        /// <returns>左辺のプロパティ名</returns>
        public static string GetMemberName(this Expression e)
        {
            var me = e as MemberExpression;

            if (me == null)
            {
                return string.Empty;
            }

            var pi = me.Member as PropertyInfo;

            if (pi == null)
            {
                return string.Empty;
            }

            return pi.Name;
        }
    }
}
