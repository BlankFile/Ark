using System.Windows;
using System.Windows.Data;
using System.Windows.Interactivity;
using System.Windows.Media;
using Ark.WPF.Infra.Interactivity.Actions;

namespace Ark.WPF.Modern.Controls
{
    /// <summary>
    /// リソースに関する機能を提供します。
    /// </summary>
    internal static class ResourceHelper
    {
        #region [Method] 取得・設定

        /// <summary>
        /// リソースを取得します。
        /// </summary>
        /// <param name="key">キー</param>
        /// <returns>リソース</returns>
        public static object GetResource(string key)
        {
            var resource = Application.Current.TryFindResource(key);

            if (resource == null)
            {
                var control = FindResourceControl(Application.Current.MainWindow, key);

                if (control != null)
                    resource = control.FindResource(key);
            }

            return resource;
        }

        /// <summary>
        /// リソースを取得します。
        /// </summary>
        /// <typeparam name="T">リソースの型</typeparam>
        /// <param name="key">キー</param>
        /// <returns>リソース</returns>
        public static T GetResource<T>(string key)
        {
            var resource = Application.Current.TryFindResource(key);

            if (resource == null)
            {
                var control = FindResourceControl(Application.Current.MainWindow, key);

                if (control != null)
                    resource = control.FindResource(key);
            }

            return (T)resource;
        }

        /// <summary>
        /// リソースを設定します。
        /// </summary>
        /// <param name="key">キー</param>
        /// <param name="value">リソースに設定する値</param>
        public static void SetResource(string key, object value)
        {
            Application.Current.Resources[key] = value;
        }

        /// <summary>
        /// コントロールを検索します。
        /// </summary>
        /// <param name="parent">親コントロール</param>
        /// <param name="findValue">検索する値</param>
        /// <returns>コントロール</returns>
        public static FrameworkElement FindResourceControl(DependencyObject parent, object findValue)
        {
            if (parent == null || findValue == null)
                return null;

            var foundChild = (FrameworkElement)null;
            var childrenCount = VisualTreeHelper.GetChildrenCount(parent);

            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                var frameworkElement = child as FrameworkElement;

                if (frameworkElement != null)
                {
                    if (frameworkElement.TryFindResource(findValue) != null)
                    {
                        foundChild = (FrameworkElement)child;
                        break;
                    }
                }

                foundChild = FindResourceControl(child, findValue);

                if (foundChild != null)
                    break;
            }

            return foundChild;
        }

        #endregion [Method] 取得・設定

        #region [Method] バインド

        /// <summary>
        /// イベントリガーを作成します。
        /// </summary>
        /// <param name="element">トリガーの設定対象</param>
        /// <param name="eventName">イベント名</param>
        /// <returns>イベントトリガー</returns>
        public static System.Windows.Interactivity.EventTrigger CreateEventTrigger(FrameworkElement element, string eventName)
        {
            return new System.Windows.Interactivity.EventTrigger(eventName)
            {
                SourceObject = element
            };
        }

        /// <summary>
        /// コマンドをバインドします。
        /// </summary>
        /// <param name="element">バインド対象オブジェクト</param>
        /// <param name="eventName">イベント名</param>
        /// <param name="path">パス</param>
        public static void BindCommand(FrameworkElement element, string eventName, string path)
        {
            var triggerAction = new CommandTriggerAction();

            BindingOperations.SetBinding(triggerAction, CommandTriggerAction.CommandProperty, new Binding(path)
            {
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

            var eventTrigger = CreateEventTrigger(element, eventName);
            eventTrigger.Actions.Add(triggerAction);

            Interaction.GetTriggers(element).Add(eventTrigger);
        }

        #endregion [Method] バインド
    }
}