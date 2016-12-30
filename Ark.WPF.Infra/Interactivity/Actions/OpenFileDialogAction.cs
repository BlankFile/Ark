using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Interactivity;
using Ark.Core.RegularExpressions;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Ark.WPF.Infra.Interactivity.Actions
{
    /// <summary>
    /// ファイル選択ダイアログを表示します。
    /// </summary>
    public class OpenFileDialogAction : TriggerAction<DependencyObject>
    {
        #region [InnerClass]

        /// <summary>
        /// <see cref="OpenFileDialogAction"/> に渡すパラメータを表します。
        /// </summary>
        public class OpenFileDialogActionArgs
        {
            /// <summary>
            /// ダイアログのタイトルを取得または設定します。
            /// </summary>
            public string Description { get; set; }

            /// <summary>
            /// 選択したファイルのパスを取得します。
            /// </summary>
            public string SelectedPath { get; internal set; }

            /// <summary>
            /// ファイルの拡張子を取得または設定します。
            /// </summary>
            public string FileExtension { get; set; }
        }

        #endregion [InnerClass]

        /// <summary>
        /// アクションを実行します。
        /// </summary>
        /// <param name="parameter">パラメーター</param>
        protected override void Invoke(object parameter)
        {
            var window = AssociatedObject as Window ?? Window.GetWindow(AssociatedObject);

            if (window == null)
                return;

            var args = parameter as OpenFileDialogActionArgs;

            if (args == null)
                return;

            if (string.IsNullOrEmpty(args.FileExtension))
                return;

            if (!args.FileExtension.Contains('|'))
                return;

            var extensions = args.FileExtension.Split('|');

            if (extensions.Length != 2)
                return;

            var lastExtension = extensions.LastOrDefault();
            var extension = Regex.Replace(lastExtension, RegexPattern.AlphabetNumberOnly, string.Empty);

            if (extension.IsNullOrWhiteSpace())
                return;

            var cfd = new CommonOpenFileDialog
            {
                Title = args.Description,
                DefaultExtension = extension,
                EnsureReadOnly = true,
                IsFolderPicker = false,
                AllowNonFileSystemItems = false,
                AllowPropertyEditing = false,
                ShowPlacesList = true
            };

            cfd.Filters.Add(new CommonFileDialogFilter(extensions.FirstOrDefault(), lastExtension));

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