using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Ark.Core.DataAnnotations
{
    /// <summary>
    /// データフィールド値がフォルダパスであることを指定します。
    /// </summary>
    public class FolderAttribute : ValidationAttribute
    {
        /// <summary>
        /// 空の文字列を使用できるかどうかを示す値を取得または設定します。
        /// </summary>
        public bool AllowEmptyStrings { get; set; }

        /// <summary>
        /// <see cref="FolderAttribute"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="allowEmptyStrings">空の文字列を使用できるかどうかを示す値</param>
        public FolderAttribute(bool allowEmptyStrings = true)
        {
            AllowEmptyStrings = allowEmptyStrings;
        }

        /// <summary>
        /// エラーが発生したデータ フィールドに基づいて、エラー メッセージに書式を適用します。
        /// </summary>
        /// <param name="name">書式設定されたメッセージに含める名前</param>
        /// <returns>書式設定されたエラー メッセージのインスタンス</returns>
        public override string FormatErrorMessage(string name)
        {
            if (ErrorMessage.IsNullOrEmpty() && ErrorMessageResourceName.IsNullOrEmpty())
            {
                return "Folder does not exist.";
            }

            return base.FormatErrorMessage(name);
        }

        /// <summary>
        /// 現在の検証属性に対して、指定した値を検証します。
        /// </summary>
        /// <param name="value">検証する値</param>
        /// <returns>検証結果</returns>
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            if (!(value is string))
            {
                return false;
            }

            var path = value as string;

            if (AllowEmptyStrings)
            {
                if (path.IsNullOrEmpty())
                {
                    return true;
                }
            }

            return Directory.Exists(path);
        }
    }
}