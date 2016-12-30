using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ark.WPF.Infra.Mvvm
{
    /// <summary>
    /// パラメーターを持たないコマンドを表します。
    /// </summary>
    public class ActionCommand : ICommand
    {
        private Action _action;
        private Func<bool> _canExecute;

        /// <summary>
        /// <see cref="ActionCommand"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public ActionCommand() { }

        /// <summary>
        /// <see cref="ActionCommand"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="action">実行する処理</param>
        /// <param name="canExecute">処理の実行可否</param>
        public ActionCommand(Action action, Func<bool> canExecute = null)
        {
            _action = action;
            _canExecute = canExecute;
        }

        /// <summary>
        /// コマンド実行可否処理の変更イベント
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// コマンドが実行可能かどうか判断します。
        /// </summary>
        /// <param name="parameter">パラメーター</param>
        /// <returns>コマンド実行の可否</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke() ?? true;
        }

        /// <summary>
        /// コマンドを実行します。
        /// </summary>
        /// <param name="parameter">パラメーター</param>
        public void Execute(object parameter)
        {
            if (CanExecute(null))
            {
                _action?.Invoke();
            }
        }
    }

    /// <summary>
    /// パラメーターを１つ持つコマンドを表します。
    /// </summary>
    /// <typeparam name="T">EventArgsの型 もしくは、Parameterの型</typeparam>
    public class ActionCommand<T> : ICommand
    {
        private Action<T> _action;
        private Func<T, bool> _canExecute;
        private Type _parameterType;
        private bool _isEnum;
        private Dictionary<string, T> _enumDic;

        /// <summary>
        /// <see cref="ActionCommand{T}"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="action">実行する処理</param>
        /// <param name="canExecute">処理の実行可否</param>
        public ActionCommand(Action<T> action, Func<T, bool> canExecute = null)
        {
            _action = action;
            _canExecute = canExecute;
            _parameterType = typeof(T);
            _isEnum = _parameterType.IsEnum;

            if (_isEnum)
            {
                _enumDic = ((T[])_parameterType.GetEnumValues()).ToDictionary(x => x.ToString());
            }
        }

        /// <summary>
        /// コマンド実行可否処理の変更イベント
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// コマンドが実行可能かどうか判断します。
        /// </summary>
        /// <param name="parameter">パラメーター</param>
        /// <returns>コマンド実行の可否</returns>
        public bool CanExecute(object parameter)
        {
            var args = ConvertToType(parameter);

            return CanExecute(args);
        }

        /// <summary>
        /// コマンドが実行可能かどうか判断します。
        /// </summary>
        /// <param name="parameter">パラメーター</param>
        /// <returns>コマンド実行の可否</returns>
        private bool CanExecute(T parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }

        /// <summary>
        /// コマンドを実行します。
        /// </summary>
        /// <param name="parameter">パラメーター</param>
        public void Execute(object parameter)
        {
            if (_action == null)
            {
                return;
            }

            var args = ConvertToType(parameter);

            if (CanExecute(args))
            {
                _action(args);
            }
        }

        /// <summary>
        /// パラメータを <typeparamref name="T"/> 型に変換します。
        /// </summary>
        /// <param name="parameter">パラメータ</param>
        /// <returns><typeparamref name="T"/> 型に変換されたパラメータ</returns>
        private T ConvertToType(object parameter)
        {
            var args = default(T);

            if (parameter is EventArgs)
            {
                args = (T)parameter;
            }
            else
            {
                var value = ((dynamic)parameter).Parameter;

                if (_isEnum)
                {
                    var valueString = value.ToString();

                    if (_enumDic.ContainsKey(valueString))
                    {
                        args = _enumDic[valueString];
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    args = (T)value;
                }
            }

            return args;
        }
    }

    /// <summary>
    /// パラメーターを２つ持つコマンドを表します。
    /// </summary>
    /// <typeparam name="T1">EventArgsの型</typeparam>
    /// <typeparam name="T2">Parameterの型</typeparam>
    public class ActionCommand<T1, T2> : ICommand
    {
        private Action<T1, T2> _action;
        private Func<T1, T2, bool> _canExecute;
        private Type _parameterType;
        private bool _isEnum;
        private Dictionary<string, T2> _enumDic;

        /// <summary>
        /// <see cref="ActionCommand{T1,T2}"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="action">実行する処理</param>
        /// <param name="canExecute">処理の実行可否</param>
        public ActionCommand(Action<T1, T2> action, Func<T1, T2, bool> canExecute = null)
        {
            _action = action;
            _canExecute = canExecute;
            _parameterType = typeof(T2);
            _isEnum = _parameterType.IsEnum;

            if (_isEnum)
            {
                _enumDic = ((T2[])_parameterType.GetEnumValues()).ToDictionary(x => x.ToString());
            }
        }

        /// <summary>
        /// コマンド実行可否処理の変更イベント
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// コマンドが実行可能かどうか判断します。
        /// </summary>
        /// <param name="parameter">パラメーター</param>
        /// <returns>コマンド実行の可否</returns>
        public bool CanExecute(object parameter)
        {
            var args = ConvertToType(parameter);
            var args1 = args.args1;
            var args2 = args.args2;

            return CanExecute(args1, args2);
        }

        /// <summary>
        /// コマンドが実行可能かどうか判断します。
        /// </summary>
        /// <param name="args1">イベント引数</param>
        /// <param name="args2">任意のパラメーター</param>
        /// <returns>コマンド実行の可否</returns>
        private bool CanExecute(T1 args1, T2 args2)
        {
            return _canExecute?.Invoke(args1, args2) ?? true;
        }

        /// <summary>
        /// コマンドを実行します。
        /// </summary>
        /// <param name="parameter">パラメーター</param>
        public void Execute(object parameter)
        {
            if (_action == null)
            {
                return;
            }

            var args = ConvertToType(parameter);
            var args1 = args.args1;
            var args2 = args.args2;

            if (CanExecute(args1, args2))
            {
                _action(args1, args2);
            }
        }

        /// <summary>
        /// パラメータを <typeparamref name="T1"/>, <typeparamref name="T2"/> 型に変換します。
        /// </summary>
        /// <param name="parameter">パラメータ</param>
        /// <returns><typeparamref name="T1"/>, <typeparamref name="T2"/> 型に変換されたパラメータ</returns>
        private dynamic ConvertToType(object parameter)
        {
            var args1 = default(T1);
            var args2 = default(T2);

            if (parameter is EventArgs)
            {
                args1 = (T1)parameter;
                args2 = default(T2);
            }
            else
            {
                var dynamicParameter = (dynamic)parameter;

                var eventValue = dynamicParameter.EventArgs;
                var paramValue = dynamicParameter.Parameter;

                if (_isEnum)
                {
                    var paramValueString = paramValue.ToString();

                    if (_enumDic.ContainsKey(paramValueString))
                    {
                        args1 = (T1)eventValue;
                        args2 = _enumDic[paramValueString];
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    args1 = (T1)eventValue;
                    args2 = (T2)paramValue;
                }
            }

            return new { args1, args2 };
        }
    }

}
