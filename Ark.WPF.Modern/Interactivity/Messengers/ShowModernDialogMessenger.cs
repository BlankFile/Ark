using System;
using System.Windows;
using Ark.WPF.Infra.Interactivity.Messengers;
using Ark.WPF.Modern.Controls;
using Ark.WPF.Modern.Interactivity.Actions;

namespace Ark.WPF.Modern.Interactivity.Messengers
{
    /// <summary>
    /// モダンダイアログを表示するためのメッセンジャーを表します。
    /// </summary>
    public class ShowModernDialogMessenger : MessengerBase<DependencyObject, ShowModernDialogAction>
    {
        #region [Property] Dependency

        /// <summary>
        /// <see cref="Effect"/> 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty EffectProperty = DependencyProperty.Register(
            nameof(Effect),
            typeof(PopupWindow.EffectType),
            typeof(ShowModernDialogMessenger),
            new PropertyMetadata(PopupWindow.EffectType.Fade));

        /// <summary>
        /// ダイアログに使用するエフェクトを取得または設定します。
        /// </summary>
        public PopupWindow.EffectType Effect { get { return (PopupWindow.EffectType)GetValue(EffectProperty); } set { SetValue(EffectProperty, value); } }

        #endregion [Property] Dependency

        /// <summary>
        /// ダイアログを表示します。
        /// </summary>
        /// <param name="view">コントロール</param>
        /// <param name="parameter">パラメータ</param>
        public void Show(object view, object parameter = null)
        {
            Show(view, parameter, false);
        }

        /// <summary>
        /// ダイアログを表示します。
        /// </summary>
        /// <param name="viewUri">コントロールのURI</param>
        /// <param name="kind">URIの探索基準</param>
        /// <param name="parameter">パラメータ</param>
        public void Show(string viewUri, UriKind kind = UriKind.Relative, object parameter = null)
        {
            var component = Application.LoadComponent(new Uri(viewUri, kind));

            Show(component, parameter, false);
        }

        /// <summary>
        /// ダイアログをモーダル表示します。
        /// </summary>
        /// <param name="view">コントロール</param>
        /// <param name="parameter">パラメータ</param>
        public void ShowDialog(object view, object parameter = null)
        {
            Show(view, parameter, true);
        }

        /// <summary>
        /// ダイアログをモーダル表示します。
        /// </summary>
        /// <param name="viewUri">コントロールのURI</param>
        /// <param name="kind">URIの探索基準</param>
        /// <param name="parameter">パラメータ</param>
        public void ShowDialog(string viewUri, UriKind kind = UriKind.Relative, object parameter = null)
        {
            var component = Application.LoadComponent(new Uri(viewUri, kind));

            Show(component, parameter, true);
        }

        /// <summary>
        /// ダイアログを表示します。
        /// </summary>
        /// <param name="view">コントロール</param>
        /// <param name="parameter">パラメータ</param>
        /// <param name="isModal">モーダル表示するかどうか</param>
        private void Show(object view, object parameter, bool isModal)
        {
            var args = new ShowModernDialogAction.ShowModernDialogActionArgs
            {
                View = view,
                Parameter = parameter,
                IsModal = isModal,
                Effect = Effect
            };

            Publish(args);
        }
    }
}