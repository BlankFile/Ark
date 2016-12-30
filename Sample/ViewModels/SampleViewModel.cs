using System;
using System.Linq;
using System.Diagnostics.Tracing;
using System.Threading;
using System.Windows.Input;
using Ark.Core.IO;
using Ark.WPF.Infra.Mvvm;
using System.IO;
using System.Text;
using Ark.WPF.Modern.Interactivity.Messengers;
using Ark.WPF.Modern.Interactivity.Notification;
using Sample.Common;

namespace Sample.ViewModels
{
    public class SampleViewModel : ViewModel
    {
        #region [Constructor]

        public SampleViewModel()
        {
            DialogCommand = new ActionCommand(() =>
            {
                DialogMessenger.ShowDialog(Consts.PageUri.Information);
            });

            MessageCommand = new ActionCommand(() =>
            {
                var result = MessageMessenger.Show("今日は晴れてますか？", MessageDialogImage.Question, DialogButtonType.YesNo);

                if (result.HasValue)
                {
                    var message = result.Value ? "ハハッ！" : "そうですか。。";

                    MessageMessenger.Show(message);
                }
            });
        }

        #endregion

        public ShowModernDialogMessenger DialogMessenger { get { return Get<ShowModernDialogMessenger>(); } set { Set(value); } }
        public ShowModernMessageMessenger MessageMessenger { get { return Get<ShowModernMessageMessenger>(); } set { Set(value); } }


        public ICommand DialogCommand { get; }
        public ICommand MessageCommand { get; }

    }
}
