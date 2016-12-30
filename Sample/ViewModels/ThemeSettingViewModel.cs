using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Ark.WPF.Infra.Mvvm;
using Ark.WPF.Modern.Media.Drawing;
using Sample.Properties;

namespace Sample.ViewModels
{
    public class ThemeSettingViewModel : ViewModel
    {
        #region [Constructor]

        public ThemeSettingViewModel()
        {
            ThemeColor = Settings.Default.ThemeColor;
            InverseMainTheme = Settings.Default.InverseThemeColor;
            MainColor = Settings.Default.MainColor;

            LoadedCommand = new ActionCommand(Loaded);
            ChangeThemeCommand = new ActionCommand<string>(ChangeTheme);
            ChangeColorCommand = new ActionCommand<string>(ChangeColor);
        }

        #endregion [Constructor]

        #region [Property]

        public ObservableCollection<string> ColorList { get { return Get<ObservableCollection<string>>(); } set { Set(value); } }
        public string ThemeColor { get { return Get<string>(); } set { Set(value); } }
        public string InverseMainTheme { get { return Get<string>(); } set { Set(value); } }
        public string MainColor { get { return Get<string>(); } set { Set(value); } }

        #endregion [Property]

        #region [Property] Command

        public ICommand LoadedCommand { get; }
        public ICommand ChangeThemeCommand { get; }
        public ICommand ChangeColorCommand { get; }

        #endregion [Property] Command

        #region [Method]

        internal void Loaded()
        {
            Task.Run(() =>
            {
                ColorList = new ObservableCollection<string>(GetColor());
            });
        }

        internal void ChangeTheme(string p)
        {
            ThemeColor = p;
            Save();
        }

        internal void ChangeColor(string p)
        {
            MainColor = p;
            Save();
        }

        internal void Save()
        {
            Settings.Default.ThemeColor = ThemeColor;
            Settings.Default.InverseThemeColor = InverseMainTheme;
            Settings.Default.MainColor = MainColor;
            Settings.Default.Save();
        }

        #endregion [Method]

        #region [Method] private

        private List<string> GetColor()
        {
            return new List<string>
            {
                ColorCode.Navy,
                ColorCode.Blue,
                ColorCode.LightBlue,
                ColorCode.Aqua,
                ColorCode.SeaGreen,
                ColorCode.Green,
                ColorCode.Lime,
                ColorCode.Olive,
                ColorCode.Khaki,
                ColorCode.Beige,
                ColorCode.Yellow,
                ColorCode.DeepYellow,
                ColorCode.Orange,
                ColorCode.DeepOrange,
                ColorCode.LightBrown,
                ColorCode.Brown,
                ColorCode.Maroon,
                ColorCode.Red,
                ColorCode.Crimson,
                ColorCode.Pink,
                ColorCode.Peach,
                ColorCode.Lavender,
                ColorCode.Purple,
                ColorCode.Violet
            };
        }

        #endregion [Method] private
    }
}