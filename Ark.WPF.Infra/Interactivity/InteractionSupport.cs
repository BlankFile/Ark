using System.Windows;

namespace Ark.WPF.Infra.Interactivity
{
    /// <summary>
    /// オブジェクトの相互作用を補助するための機能を提供します。
    /// </summary>
    public static class InteractionSupport
    {
        /// <summary>
        /// TransferObject 添付プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty TransferObjectProperty = DependencyProperty.RegisterAttached(
            "TransferObject", typeof(object), typeof(InteractionSupport), new PropertyMetadata());

        /// <summary>
        /// 指定された <see cref="DependencyObject"/> から TransferObject 添付プロパティの値を取得します。
        /// </summary>
        /// <param name="obj">オブジェクト</param>
        /// <returns>TransferObject 添付プロパティの値</returns>
        public static object GetTransferObject(DependencyObject obj) => (obj.GetValue(TransferObjectProperty));

        /// <summary>
        /// TransferObject 添付プロパティの値を、指定された <see cref="DependencyObject"/> に設定します。
        /// </summary>
        /// <param name="obj">オブジェクト</param>
        /// <param name="value">TransferObject 添付プロパティに設定する値</param>
        public static void SetTransferObject(DependencyObject obj, object value) => obj.SetValue(TransferObjectProperty, value);
    }
}