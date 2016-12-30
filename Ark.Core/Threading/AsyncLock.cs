using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ark.Core.Threading
{
    /// <summary>
    /// async/await ステートメントをロック可能なオブジェクトを表します。
    /// </summary>
    public sealed class AsyncLock
    {
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        private readonly Task<IDisposable> _releaser;

        /// <summary>
        /// <see cref="AsyncLock"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public AsyncLock()
        {
            _releaser = Task.FromResult((IDisposable)new Releaser(this));
        }

        /// <summary>
        /// ロックオブジェクトを生成します。
        /// </summary>
        /// <returns>スレッドロックオブジェクト</returns>
        public Task<IDisposable> LockAsync()
        {
            var wait = _semaphore.WaitAsync();

            if (wait.IsCompleted)
            {
                return _releaser;
            }

            return wait.ContinueWith(
                (_, state) => (IDisposable)state,
                _releaser.Result,
                CancellationToken.None,
                TaskContinuationOptions.ExecuteSynchronously,
                TaskScheduler.Default);
        }

        /// <summary>
        /// <see cref="AsyncLock"/> を解放する役割を担います。
        /// </summary>
        private sealed class Releaser : IDisposable
        {
            private readonly AsyncLock _toRelease;

            /// <summary>
            /// <see cref="Releaser"/> クラスの新しいインスタンスを初期化します。
            /// </summary>
            /// <param name="toRelease">開放対象の <see cref="AsyncLock"/></param>
            internal Releaser(AsyncLock toRelease)
            {
                _toRelease = toRelease;
            }

            #region [InterfaceImpl] IDisposable

            private bool _disposed = false;

            private void Dispose(bool disposing)
            {
                if (!_disposed)
                {
                    if (disposing)
                    {
                    }

                    _toRelease?._semaphore?.Release();

                    _disposed = true;
                }
            }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            ~Releaser()
            {
                Dispose(false);
            }

            #endregion [InterfaceImpl] IDisposable
        }
    }
}