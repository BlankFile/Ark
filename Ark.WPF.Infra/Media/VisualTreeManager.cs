using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Ark.WPF.Infra.Media
{
    /// <summary>
    /// ビジュアルツリー周りに関する機能を提供します。
    /// </summary>
    public static class VisualTreeManager
    {
        /// <summary>
        /// 上位にある指定した型のコントロールを探索します。<para/>
        /// 見つからなかった場合は null を返します。
        /// </summary>
        /// <typeparam name="T">探索対象コントロールの型</typeparam>
        /// <param name="child">探索起点となるコントロール</param>
        /// <returns>指定した型のコントロール</returns>
        public static T FindVisualParent<T>(DependencyObject child) where T : DependencyObject
        {
            var parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null)
            {
                return null;
            }

            var parent = parentObject as T;

            if (parent != null)
            {
                return parent;
            }
            else
            {
                return FindVisualParent<T>(parentObject);
            }
        }

    }
}
