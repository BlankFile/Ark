using System.Windows;
using System.Windows.Interactivity;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Ark.WPF.Infra.Interactivity.Actions
{
    /// <summary>
    /// フォルダー選択ダイアログを表示します。
    /// </summary>
    public class OpenFolderDialogAction : TriggerAction<DependencyObject>
    {
        #region [InnerClass]

        /// <summary>
        /// <see cref="OpenFolderDialogAction"/> に渡すパラメータを表します。
        /// </summary>
        public class OpenFolderDialogActionArgs
        {
            /// <summary>
            /// ダイアログのタイトルを取得または設定します。
            /// </summary>
            public string Description { get; set; }

            /// <summary>
            /// 選択したファイルのパスを取得します。
            /// </summary>
            public string SelectedPath { get; internal set; }
        }

        #endregion

        /// <summary>
        /// アクションを実行します。
        /// </summary>
        /// <param name="parameter">パラメーター</param>
        protected override void Invoke(object parameter)
        {
            var window = AssociatedObject as Window ?? Window.GetWindow(AssociatedObject);

            if (window == null)
                return;

            var args = parameter as OpenFolderDialogActionArgs;

            if (args == null)
                return;

            var cfd = new CommonOpenFileDialog
            {
                Title = args.Description,
                EnsureReadOnly = true,
                IsFolderPicker = true,
                AllowNonFileSystemItems = false,
            };

            var result = cfd.ShowDialog(window);

            switch (result)
            {
                case CommonFileDialogResult.Ok:
                    args.SelectedPath = cfd.FileName;
                    break;
            }
        }
    }
}