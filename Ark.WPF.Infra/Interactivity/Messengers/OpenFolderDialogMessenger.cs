using System.Windows;
using Ark.WPF.Infra.Interactivity.Actions;

namespace Ark.WPF.Infra.Interactivity.Messengers
{
    /// <summary>
    /// フォルダ選択ダイアログを表示するためのメッセンジャーを表します。
    /// </summary>
    public class OpenFolderDialogMessenger : MessengerBase<DependencyObject, OpenFolderDialogAction>
    {
        /// <summary>
        /// フォルダ選択ダイアログを表示します。
        /// </summary>
        /// <param name="description">ダイアログのタイトル</param>
        /// <returns>フォルダパス</returns>
        public string Show(string description)
        {
            var args = new OpenFolderDialogAction.OpenFolderDialogActionArgs
            {
                Description = description
            };

            Publish(args);

            return args.SelectedPath;
        }
    }
}