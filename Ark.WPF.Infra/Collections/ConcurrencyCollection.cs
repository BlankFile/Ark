using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace Ark.WPF.Infra.Collections
{
    /// <summary>
    /// スレッドセーフな <see cref="ObservableCollection{T}"/> を表します。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ConcurrencyCollection<T> : ObservableCollection<T>
    {
        /// <summary>
        /// 排他オブジェクト
        /// </summary>
        private readonly object _lockObj = new object();

        /// <summary>
        /// <see cref="ConcurrencyCollection{T}"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public ConcurrencyCollection()
        {
            BindingOperations.EnableCollectionSynchronization(this, _lockObj);
        }

        /// <summary>
        /// 指定したコレクションのコピーを格納し、<see cref="ConcurrencyCollection{T}"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="source">初期コレクション</param>
        public ConcurrencyCollection(IEnumerable<T> source)
        {
            BindingOperations.EnableCollectionSynchronization(this, _lockObj);

            source.ForEach(x => Add(x));
        }
    }
}