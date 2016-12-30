using Ark.WPF.Infra.Mvvm;

namespace Sample.ViewModels
{
    public class SettingViewModel : ViewModel
    {
        public SettingViewModel()
        {
            ThemeSetting = new ThemeSettingViewModel();
        }

        public ThemeSettingViewModel ThemeSetting { get { return Get<ThemeSettingViewModel>(); } set { Set(value); } }
    }
}