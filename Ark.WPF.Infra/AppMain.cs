using System.Threading.Tasks;
using System.Windows;
using Ark.WPF.Infra.Threading;

namespace Ark.WPF.Infra
{
    /// <summary>
    /// アプリケーションのエントリポイントを表します。
    /// </summary>
    public class AppMain : Application
    {
        /// <summary>
        /// アプリケーション起動時の処理。
        /// </summary>
        /// <param name="e">イベントデータ</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            UITask.TaskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            UITask.TaskFactory = new TaskFactory(UITask.TaskScheduler);
        }
    }
}
