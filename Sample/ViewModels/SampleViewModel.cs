using System.Threading.Tasks;
using System.Windows.Input;
using Ark.WPF.Infra.Mvvm;
using Ark.WPF.Modern.Interactivity.Messengers;
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
                var result = MessageMessenger.Question("ゴリラの学名はゴリラゴリラである。");

                if (result.HasValue)
                {
                    var message = result.Value ? "正解！！" : "ハズレ。。";

                    MessageMessenger.Info(message);
                }
            });

            BusyCommand = new ActionCommand(async () =>
            {
                BusyMessenger.IsBusy = true;

                await Task.Delay(2000);

                BusyMessenger.IsBusy = false;
            });
        }

        #endregion

        public ShowModernDialogMessenger DialogMessenger { get { return Get<ShowModernDialogMessenger>(); } set { Set(value); } }
        public ShowModernMessageMessenger MessageMessenger { get { return Get<ShowModernMessageMessenger>(); } set { Set(value); } }
        public ShowBusyDialogMessenger BusyMessenger { get { return Get<ShowBusyDialogMessenger>(); } set { Set(value); } }

        public ICommand DialogCommand { get; }
        public ICommand MessageCommand { get; }
        public ICommand BusyCommand { get; }
    }
}