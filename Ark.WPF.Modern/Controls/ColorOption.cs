using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Ark.WPF.Modern.Media.Design;
using Ark.WPF.Modern.Media.Drawing;

namespace Ark.WPF.Modern.Controls
{
    /// <summary>
    /// モダンスタイルの色に関する設定を定義します。
    /// </summary>
    public static class ColorOption
    {
        #region [Member]

        private static Duration duration = TimeSpan.FromMilliseconds(500).ToDuration();

        #endregion

        #region [Property] Attached

        /// <summary>
        /// ThemeColor 添付プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty ThemeColorProperty = DependencyProperty.RegisterAttached(
            "ThemeColor", typeof(string), typeof(ColorOption), new PropertyMetadata(null, OnThemeColorChanged));

        /// <summary>
        /// 指定された <see cref="DependencyObject"/> から ThemeColor 添付プロパティの値を取得します。
        /// </summary>
        /// <param name="obj">オブジェクト</param>
        /// <returns>ThemeColor 添付プロパティの値</returns>
        public static string GetThemeColor(DependencyObject obj) => (string)obj.GetValue(ThemeColorProperty);

        /// <summary>
        /// ThemeColor 添付プロパティの値を、指定された <see cref="DependencyObject"/> に設定します。
        /// </summary>
        /// <param name="obj">オブジェクト</param>
        /// <param name="value">ThemeColor 添付プロパティに設定する値</param>
        public static void SetThemeColor(DependencyObject obj, string value) => obj.SetValue(ThemeColorProperty, value);

        /// <summary>
        /// InverseThemeColor 添付プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty InverseThemeColorProperty = DependencyProperty.RegisterAttached(
            "InverseThemeColor", typeof(string), typeof(ColorOption), new PropertyMetadata(null));

        /// <summary>
        /// 指定された <see cref="DependencyObject"/> から InverseThemeColor 添付プロパティの値を取得します。
        /// </summary>
        /// <param name="obj">オブジェクト</param>
        /// <returns>InverseThemeColor 添付プロパティの値</returns>
        public static string GetInverseThemeColor(DependencyObject obj) => (string)obj.GetValue(InverseThemeColorProperty);

        /// <summary>
        /// InverseThemeColor 添付プロパティの値を、指定された <see cref="DependencyObject"/> に設定します。
        /// </summary>
        /// <param name="obj">オブジェクト</param>
        /// <param name="value">InverseThemeColor 添付プロパティに設定する値</param>
        public static void SetInverseThemeColor(DependencyObject obj, string value) => obj.SetValue(InverseThemeColorProperty, value);

        /// <summary>
        /// MainColor 添付プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty MainColorProperty = DependencyProperty.RegisterAttached(
            "MainColor", typeof(string), typeof(ColorOption), new PropertyMetadata(null, OnMainColorChanged));

        /// <summary>
        /// 指定された <see cref="DependencyObject"/> から MainColor 添付プロパティの値を取得します。
        /// </summary>
        /// <param name="obj">オブジェクト</param>
        /// <returns>MainColor 添付プロパティの値</returns>
        public static string GetMainColor(DependencyObject obj) => (string)obj.GetValue(MainColorProperty);

        /// <summary>
        /// MainColor 添付プロパティの値を、指定された <see cref="DependencyObject"/> に設定します。
        /// </summary>
        /// <param name="obj">オブジェクト</param>
        /// <param name="value">MainColor 添付プロパティに設定する値</param>
        public static void SetMainColor(DependencyObject obj, string value) => obj.SetValue(MainColorProperty, value);

        /// <summary>
        /// Duration 添付プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty DurationProperty = DependencyProperty.RegisterAttached(
            "Duration", typeof(Duration), typeof(ColorOption), new PropertyMetadata(OnDurationChanged));

        /// <summary>
        /// 指定された <see cref="DependencyObject"/> から Duration 添付プロパティの値を取得します。
        /// </summary>
        /// <param name="obj">オブジェクト</param>
        /// <returns>Duration 添付プロパティの値</returns>
        public static Duration GetDuration(DependencyObject obj) => (Duration)obj.GetValue(DurationProperty);

        /// <summary>
        /// Duration 添付プロパティの値を、指定された <see cref="DependencyObject"/> に設定します。
        /// </summary>
        /// <param name="obj">オブジェクト</param>
        /// <param name="value">Duration 添付プロパティに設定する値</param>
        public static void SetDuration(DependencyObject obj, Duration value) => obj.SetValue(DurationProperty, value);

        #endregion

        #region [Method] private static

        /// <summary>
        /// ThemeColor 添付プロパティ変更時の処理。
        /// </summary>
        /// <param name="sender">イベント発生オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private static void OnThemeColorChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var newColorCode = e.NewValue as string;
            var oldColorCode = e.OldValue as string;

            if (newColorCode.IsNullOrEmpty())
            {
                return;
            }

            ChangeColor(oldColorCode, newColorCode, ColorResourceKey.Theme);

            if (newColorCode == ColorCode.White)
            {
                ChangeColor(ColorCode.White, ColorCode.Black, ColorResourceKey.InverseTheme);

                ResourceHelper.SetResource(OpacityResourceKey.Normal, OpacityValue.Normal);
                ResourceHelper.SetResource(OpacityResourceKey.Focus, OpacityValue.Focus);
            }
            else
            {
                ChangeColor(ColorCode.Black, ColorCode.White, ColorResourceKey.InverseTheme);

                ResourceHelper.SetResource(OpacityResourceKey.Normal, OpacityValue.Focus);
                ResourceHelper.SetResource(OpacityResourceKey.Focus, OpacityValue.Normal);
            }

            SetInverseThemeColor(sender, ResourceHelper.GetResource<SolidColorBrush>(ColorResourceKey.InverseTheme).Color.ToString());
        }

        /// <summary>
        /// MainColor 添付プロパティ変更時の処理。
        /// </summary>
        /// <param name="sender">イベント発生オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private static void OnMainColorChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var newColorCode = e.NewValue as string;
            var oldColorCode = e.OldValue as string;

            if (newColorCode.IsNullOrEmpty())
            {
                return;
            }

            ChangeColor(oldColorCode, newColorCode, ColorResourceKey.Main);
        }

        /// <summary>
        /// Duration 添付プロパティ変更時の処理。
        /// </summary>
        /// <param name="sender">イベント発生オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private static void OnDurationChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is Duration)
            {
                duration = (Duration)e.NewValue;
            }
        }

        /// <summary>
        /// 指定した色でカラーリソースを変更します。
        /// </summary>
        /// <param name="oldColorCode">変更前の色</param>
        /// <param name="newColorCode">変更後の色</param>
        /// <param name="resourceKey">設定対象のリソースキー</param>
        private static void ChangeColor(string oldColorCode, string newColorCode, string resourceKey)
        {
            if (!oldColorCode.IsNullOrEmpty())
            {
                var brush = oldColorCode.ToBrush();
                var animation = new ColorAnimation(newColorCode.ToColor(), duration);

                animation.Completed += (s, e) =>
                {
                };

                brush.BeginAnimation(SolidColorBrush.ColorProperty, animation, HandoffBehavior.SnapshotAndReplace);

                ResourceHelper.SetResource(resourceKey, brush);

                Task.Delay(duration.TimeSpan).ContinueWith(t =>
                {
                    ResourceHelper.SetResource(resourceKey, newColorCode.ToBrush());
                });
            }
            else
            {
                ResourceHelper.SetResource(resourceKey, newColorCode.ToBrush());
            }
        }

        #endregion
    }
}