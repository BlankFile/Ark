using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Ark.Core.Event;

namespace Ark.WPF.Infra.Collections
{
    /// <summary>
    /// 更新イベントを持つ <see cref="ObservableCollection{T}"/> を表します。
    /// <para>更新イベントは弱参照として設定されます。</para>
    /// </summary>
    /// <typeparam name="T">コレクションの型</typeparam>
    public class NotifyObservableCollection<T> : ObservableCollection<T>
    {
        private WeakEventListener<NotifyCollectionChangedEventHandler, NotifyCollectionChangedEventArgs> _notifyWeakEvent;
        private Action<NotifyCollectionChangedEventArgs> _collectionChangedAction;

        /// <summary>
        /// <see cref="NotifyObservableCollection{T}"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public NotifyObservableCollection() : base() { }

        /// <summary>
        /// 指定したコレクションのコピーを格納し、<see cref="NotifyObservableCollection{T}"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="collection">初期コレクション</param>
        public NotifyObservableCollection(IEnumerable<T> collection) : base(collection) { }

        /// <summary>
        /// 指定したリストのコピーを格納し、<see cref="NotifyObservableCollection{T}"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="list">初期コレクション</param>
        public NotifyObservableCollection(List<T> list) : base(list) { }

        /// <summary>
        /// コレクション操作時の処理を設定します。
        /// </summary>
        /// <param name="action">コレクション操作時の処理</param>
        public void SetCollectionChanged(Action<NotifyCollectionChangedEventArgs> action)
        {
            _notifyWeakEvent?.Dispose();

            _collectionChangedAction = action;

            _notifyWeakEvent = new WeakEventListener<NotifyCollectionChangedEventHandler, NotifyCollectionChangedEventArgs>(
                OnCollectionChanged,
                h => CollectionChanged += h,
                h => CollectionChanged -= h);
        }

        /// <summary>
        /// コレクション操作時の処理。
        /// </summary>
        /// <param name="sender">イベント発生オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            _collectionChangedAction?.Invoke(e);
        }
    }
}