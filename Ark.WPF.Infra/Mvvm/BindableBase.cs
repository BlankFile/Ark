using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Ark.WPF.Infra.Mvvm
{
    /// <summary>
    /// クライアントにプロパティの変更を通知するための基本的な機能を提供します。
    /// </summary>
    public abstract class BindableBase : INotifyPropertyChanged
    {
        /// <summary>
        /// プロパティ変更イベント
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// クライアントにプロパティの変更を通知します。
        /// </summary>
        /// <param name="propName">変更したプロパティ</param>
        public void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}