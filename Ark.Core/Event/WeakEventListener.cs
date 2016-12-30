using System;

namespace Ark.Core.Event
{
    /// <summary>
    /// イベントを"弱い参照"として設定するための機能を提供します。
    /// </summary>
    /// <typeparam name="TDelegate">イベントデリゲート</typeparam>
    /// <typeparam name="TEventArgs">イベント引数</typeparam>
    public class WeakEventListener<TDelegate, TEventArgs> : IDisposable
        where TDelegate : class
        where TEventArgs : EventArgs
    {
        #region [Member]

        /// <summary>
        /// イベントハンドラー
        /// </summary>
        private EventHandler<TEventArgs> _handler;

        /// <summary>
        /// イベント解除処理
        /// </summary>
        private Action _removeAction;

        #endregion [Member]

        #region [Constructor]

        /// <summary>
        /// イベントハンドラーを指定して、<see cref="WeakEventListener{TDelegate, TEventArgs}"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="handler">イベントハンドラー</param>
        /// <param name="add">ハンドラー追加処理</param>
        /// <param name="remove">ハンドラー解除処理</param>
        public WeakEventListener(EventHandler<TEventArgs> handler, Action<TDelegate> add, Action<TDelegate> remove)
        {
            _handler = handler;

            var createHandler = new Func<EventHandler<TEventArgs>, TDelegate>(h => Delegate.CreateDelegate(typeof(TDelegate), h, "Invoke") as TDelegate);
            var weakHandler = createHandler((sender, e) => ReceiveEvent(new WeakReference(this), sender, e));

            add(weakHandler);

            _removeAction = () => remove(weakHandler);
        }

        #endregion [Constructor]

        #region [Method] private static

        /// <summary>
        /// イベントを受信し、"弱い参照"に設定したイベントハンドラーを実行します。
        /// </summary>
        /// <param name="listenerWeakReference">弱参照オブジェクト</param>
        /// <param name="sender">イベント発生オブジェクト</param>
        /// <param name="args">イベント引数</param>
        private static void ReceiveEvent(WeakReference listenerWeakReference, object sender, TEventArgs args)
        {
            var listener = listenerWeakReference.Target as WeakEventListener<TDelegate, TEventArgs>;

            listener?._handler?.Invoke(sender, args);
        }

        #endregion [Method] private static

        #region [InterfaceImpl] IDisposable

        /// <summary>
        /// 開放フラグ
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// リソースを解放します。
        /// </summary>
        /// <param name="disposing">マネージドリソース解放フラグ</param>
        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            _removeAction?.Invoke();

            _disposed = true;
        }

        /// <summary>
        /// リソースを解放します。
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// アンマネージドリソースを解放します。
        /// </summary>
        ~WeakEventListener()
        {
            Dispose(false);
        }

        #endregion [InterfaceImpl] IDisposable
    }
}