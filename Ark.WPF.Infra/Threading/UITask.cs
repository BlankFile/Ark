using System;
using System.Threading.Tasks;

namespace Ark.WPF.Infra.Threading
{
    /// <summary>
    /// UIスレッド操作を表します。
    /// </summary>
    public static class UITask
    {
        /// <summary>
        /// UI上のスケジューラーを取得します。
        /// </summary>
        public static TaskScheduler TaskScheduler { get; internal set; }

        /// <summary>
        /// UI上のタスクファクトリーを取得します。
        /// </summary>
        public static TaskFactory TaskFactory { get; internal set; }

        /// <summary>
        /// 指定した処理をUIスレッドで実行します。
        /// </summary>
        /// <param name="action">実行する処理</param>
        /// <returns>タスク</returns>
        public static Task Run(Action action)
        {
            return TaskFactory.StartNew(action);
        }

        /// <summary>
        /// 指定した戻り値を持つ処理をUIスレッドで実行します。
        /// </summary>
        /// <typeparam name="T">戻り値の型</typeparam>
        /// <param name="func">実行する処理</param>
        /// <returns>タスク</returns>
        public static Task<T> Run<T>(Func<T> func)
        {
            return TaskFactory.StartNew(func);
        }

    }

}