using System.Windows;
using Ark.WPF.Infra.Interactivity.Actions;

namespace Ark.WPF.Infra.Interactivity.Messengers
{
    /// <summary>
    /// ファイル保存ダイアログを表示するためのメッセンジャーを表します。
    /// </summary>
    public class SaveFileDialogMessenger : MessengerBase<DependencyObject, SaveFileDialogAction>
    {
        /// <summary>
        /// ファイル保存ダイアログを表示します。
        /// </summary>
        /// <param name="description">ダイアログのタイトル</param>
        /// <param name="extension">選択可能な拡張子</param>
        /// <returns>ファイルパス</returns>
        public string Show(string description, string extension)
        {
            var args = new SaveFileDialogAction.SaveFileDialogActionArgs
            {
                Description = description,
                FileExtension = extension
            };

            Publish(args);

            return args.SelectedPath;
        }
    }
}