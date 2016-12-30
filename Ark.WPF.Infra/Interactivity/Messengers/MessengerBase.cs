using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using Ark.WPF.Infra.Interactivity.Triggers;

namespace Ark.WPF.Infra.Interactivity.Messengers
{
    /// <summary>
    /// ViewModel から View の処理を実行するための基本的な連絡オブジェクトを定義します。
    /// </summary>
    public abstract class MessengerBase : DependencyObject
    {
        #region [Property] Dependency

        /// <summary>
        /// <see cref="Target"/> 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty TargetProperty = DependencyProperty.Register(
            nameof(Target), typeof(DependencyObject), typeof(MessengerBase), new PropertyMetadata(OnTargetChanged));

        /// <summary>
        /// メッセンジャーの通知対象を取得または設定します。
        /// </summary>
        public DependencyObject Target { get { return (DependencyObject)GetValue(TargetProperty); } set { SetValue(TargetProperty, value); } }

        #endregion

        #region [Method] protected

        /// <summary>
        /// メッセンジャーの通知対象が変更された時の処理を記述します。
        /// </summary>
        /// <param name="control">新しい通知対象</param>
        protected virtual void TargetChanged(object control) { }

        #endregion

        #region [Method] private static

        /// <summary>
        /// <see cref="Target"/> 依存関係プロパティ変更処理
        /// </summary>
        /// <param name="sender">イベント発生オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private static void OnTargetChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var messenger = sender as MessengerBase;

            messenger?.TargetChanged(e.NewValue);
        }

        #endregion
    }

    /// <summary>
    /// ViewModel から View の処理を実行するための基本的な連絡オブジェクトを定義します。
    /// </summary>
    /// <typeparam name="TControl">通知対象コントロールの型</typeparam>
    /// <typeparam name="TAction">実行する処理の型</typeparam>
    public abstract class MessengerBase<TControl, TAction> : MessengerBase
        where TControl : DependencyObject
        where TAction : TriggerAction<TControl>, new()
    {
        #region [Member]

        /// <summary>
        /// 実行する処理を持つトリガーオブジェクト
        /// </summary>
        private MessengerTrigger<TControl> _trigger;

        #endregion

        #region [Constructor]

        /// <summary>
        /// <see cref="MessengerBase{TControl, TAction}"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public MessengerBase() { }

        /// <summary>
        /// <see cref="MessengerBase{TControl, TAction}"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="target">通知対象のコントロール</param>
        public MessengerBase(TControl target)
        {
            Subscribe(target);
        }

        #endregion

        #region [Method] public

        /// <summary>
        /// メッセンジャーの購読を解除します。
        /// </summary>
        public void Unsubscribe()
        {
            _trigger?.Detach();
        }

        #endregion

        #region [Method] protected

        /// <summary>
        /// メッセンジャーの購読を設定します。
        /// </summary>
        /// <param name="target">通知対象のコントロール</param>
        protected void Subscribe(TControl target)
        {
            if (target != null && target is TControl)
            {
                _trigger = new MessengerTrigger<TControl>();
                _trigger.Actions.Add(new TAction());
                _trigger.Attach(target);
            }
        }

        /// <summary>
        /// メッセンジャーが購読している処理を実行します。
        /// </summary>
        /// <param name="parameter">実行処理に渡すパラメータ</param>
        protected virtual void Publish(object parameter = null)
        {
            _trigger?.Invoke(parameter);
        }

        #endregion

        #region [Method] protected override

        /// <summary>
        /// 通知対象だったコントロールの購読を解除し、通知対象になったコントロールの購読を設定します。
        /// </summary>
        /// <param name="control"></param>
        protected override void TargetChanged(object control)
        {
            var obj = control as TControl;

            if (obj != null)
            {
                Unsubscribe();
                Subscribe(obj);
            }
        }

        #endregion
    }

}
