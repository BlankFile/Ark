using System;

namespace Ark.WPF.Infra.Mvvm
{
    /// <summary>
    /// プロパティに付随する情報を表します。
    /// </summary>
    public class PropertyInfo
    {
        /// <summary>
        /// プロパティの値
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 検証処理
        /// </summary>
        public Func<ValidationInfo> ValidateFunc { get; set; }

        /// <summary>
        /// 変更前処理
        /// </summary>
        public Action BeforeAction { get; set; }

        /// <summary>
        /// 変更後処理
        /// </summary>
        public Action AfterAction { get; set; }
    }
}