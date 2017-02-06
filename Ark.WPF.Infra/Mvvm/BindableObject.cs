using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Ark.WPF.Infra.Mvvm
{
    /// <summary>
    /// クライアントにプロパティの変更を通知する機能を持つオブジェクトを定義します。
    /// </summary>
    public abstract class BindableObject : BindableBase, INotifyDataErrorInfo
    {
        #region [Member]

        private Dictionary<string, object> _propertyDic = new Dictionary<string, object>();
        private Dictionary<string, List<string>> _chainPropertyDic = new Dictionary<string, List<string>>();
        private Dictionary<string, Func<ValidationInfo>> _validateDic = new Dictionary<string, Func<ValidationInfo>>();
        private Dictionary<string, Action> _beforeActionDic = new Dictionary<string, Action>();
        private Dictionary<string, Action> _afterActionDic = new Dictionary<string, Action>();
        private Dictionary<string, List<string>> _errorDic = new Dictionary<string, List<string>>();

        #endregion

        #region [Method] 変更通知

        /// <summary>
        /// プロパティと変更後処理をバインドします。
        /// </summary>
        /// <typeparam name="T">プロパティの型</typeparam>
        /// <param name="propName">プロパティ名</param>
        /// <param name="initValue">初期値</param>
        /// <param name="afterAction">変更後処理</param>
        public void Bind<T>(string propName, T initValue, Action afterAction)
        {
            if (!_propertyDic.ContainsKey(propName))
            {
                _propertyDic[propName] = initValue;
            }

            if (afterAction != null && !_afterActionDic.ContainsKey(propName))
            {
                _afterActionDic[propName] = afterAction;
            }
        }

        /// <summary>
        /// プロパティに検証処理と変更後処理をバインドします。
        /// </summary>
        /// <typeparam name="T">プロパティの型</typeparam>
        /// <param name="propName">プロパティ名</param>
        /// <param name="initValue">初期値</param>
        /// <param name="validate">検証処理</param>
        /// <param name="afterAction">変更後処理</param>
        public void Bind<T>(string propName, T initValue, Func<ValidationInfo> validate, Action afterAction = null)
        {
            if (!_propertyDic.ContainsKey(propName))
            {
                _propertyDic[propName] = initValue;
            }

            if (validate != null && !_validateDic.ContainsKey(propName))
            {
                _validateDic[propName] = validate;
            }

            if (afterAction != null && !_afterActionDic.ContainsKey(propName))
            {
                _afterActionDic[propName] = afterAction;
            }
        }

        /// <summary>
        /// プロパティに変更後処理などの情報をバインドします。
        /// </summary>
        /// <typeparam name="T">プロパティの型</typeparam>
        /// <param name="propName">プロパティ名</param>
        /// <param name="info">プロパティ情報</param>
        public void Bind<T>(string propName, PropertyInfo info)
        {
            if (info != null)
            {
                if (!_propertyDic.ContainsKey(propName))
                {
                    _propertyDic[propName] = info.Value;
                }

                if (info.ValidateFunc != null && !_validateDic.ContainsKey(propName))
                {
                    _validateDic[propName] = info.ValidateFunc;
                }

                if (info.BeforeAction != null && !_beforeActionDic.ContainsKey(propName))
                {
                    _beforeActionDic[propName] = info.BeforeAction;
                }

                if (info.AfterAction != null && !_afterActionDic.ContainsKey(propName))
                {
                    _afterActionDic[propName] = info.AfterAction;
                }
            }
        }

        /// <summary>
        /// プロパティの変更を、指定したプロパティに通知します。
        /// </summary>
        /// <param name="propName">プロパティ名</param>
        /// <param name="chainPropNames">通知するプロパティ名</param>
        public void Chain(string propName, params string[] chainPropNames)
        {
            if (!_chainPropertyDic.ContainsKey(propName))
            {
                _chainPropertyDic[propName] = chainPropNames.ToList();
            }
            else
            {
                _chainPropertyDic[propName] = _chainPropertyDic[propName].Union(chainPropNames).ToList();
            }
        }

        /// <summary>
        /// 指定されたプロパティを持つかどうか判断します。
        /// </summary>
        /// <param name="propName">プロパティ名</param>
        /// <returns>
        /// True ：持つ
        /// False：持たない
        /// </returns>
        public bool Contains(string propName)
        {
            return _propertyDic.ContainsKey(propName);
        }

        /// <summary>
        /// プロパティの値を取得します。
        /// </summary>
        /// <typeparam name="T">プロパティの型</typeparam>
        /// <param name="propName">プロパティ名</param>
        /// <returns>値</returns>
        public T Get<T>([CallerMemberName] string propName = "")
        {
            if (!_propertyDic.ContainsKey(propName))
            {
                return default(T);
            }

            return (T)_propertyDic[propName];
        }

        /// <summary>
        /// プロパティの値を設定します。
        /// </summary>
        /// <typeparam name="T">プロパティの型</typeparam>
        /// <param name="value">設定する値</param>
        /// <param name="propName">プロパティ名</param>
        public void Set<T>(T value, [CallerMemberName] string propName = "")
        {
            if (_propertyDic.ContainsKey(propName))
            {
                if (Equals(_propertyDic[propName], value))
                {
                    return;
                }
            }

            if (_beforeActionDic.ContainsKey(propName))
            {
                _beforeActionDic[propName]();
            }

            _propertyDic[propName] = value;
            OnPropertyChanged(propName);

            if (_validateDic.ContainsKey(propName))
            {
                var validateResult = _validateDic[propName]();

                if (validateResult == null)
                {
                    SetError(propName, null);
                }
                else
                {
                    SetError(propName, validateResult.ErrorMessage);

                    if (HasError(propName))
                    {
                        if (!validateResult.IsContinue)
                        {
                            return;
                        }
                    }
                }
            }

            if (_afterActionDic.ContainsKey(propName))
            {
                _afterActionDic[propName]();
            }

            if (_chainPropertyDic.ContainsKey(propName))
            {
                _chainPropertyDic[propName].ForEach(x => OnPropertyChanged(x));
            }
        }

        #endregion

        #region [Method] エラー補助

        /// <summary>
        /// エラー情報の変更をクライアントに通知します。
        /// </summary>
        /// <param name="propName">プロパティ名</param>
        protected void OnErrorsChanged([CallerMemberName] string propName = null)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propName));
        }

        /// <summary>
        /// エラーを削除します。
        /// </summary>
        /// <param name="propName">プロパティ名</param>
        public void RemoveError(string propName)
        {
            _errorDic.Remove(propName);

            OnErrorsChanged(propName);
            OnPropertyChanged(nameof(HasErrors));
        }

        /// <summary>
        /// エラーメッセージを設定します。
        /// </summary>
        /// <param name="propName">プロパティ名</param>
        /// <param name="errorMsg">エラーメッセージ</param>
        public void SetError(string propName, string errorMsg)
        {
            if (string.IsNullOrEmpty(errorMsg))
            {
                _errorDic.Remove(propName);
            }
            else
            {
                _errorDic[propName] = new List<string> { errorMsg };
            }

            OnErrorsChanged(propName);
            OnPropertyChanged(nameof(HasErrors));
        }

        /// <summary>
        /// エラーメッセージを取得します。
        /// </summary>
        /// <param name="propName">プロパティ名</param>
        /// <returns>エラーメッセージ</returns>
        public string GetError(string propName)
        {
            return _errorDic.GetOrDefault(propName)?.First() ?? string.Empty;
        }

        /// <summary>
        /// 指定したプロパティがエラーかどうか判断します。
        /// </summary>
        /// <param name="propName">プロパティ名</param>
        /// <returns>
        /// True ：エラー
        /// False：エラーでない
        /// </returns>
        public bool HasError(string propName)
        {
            return _errorDic.ContainsKey(propName);
        }

        /// <summary>
        /// 検証します。
        /// </summary>
        public void Validate()
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(this);

            Validator.TryValidateObject(this, context, results, true);

            var errors = results.ToDictionary(x => x.MemberNames.First(), x => x.ErrorMessage);
            var clears = _errorDic.Select(x => x.Key).Except(errors.Select(x => x.Key)).ToArray();

            errors.ForEach(x => SetError(x.Key, x.Value));
            clears.ForEach(x => RemoveError(x));
        }

        #endregion

        #region [InterfaceImpl] INotifyDataErrorInfo

        /// <summary>
        /// エラー変更イベント
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <summary>
        /// エラー一覧を取得します。
        /// </summary>
        /// <param name="propName">プロパティ名</param>
        /// <returns>エラー一覧</returns>
        public IEnumerable GetErrors(string propName)
        {
            if (string.IsNullOrEmpty(propName))
            {
                return null;
            }

            if (_errorDic.ContainsKey(propName))
            {
                return _errorDic[propName];
            }

            return null;
        }

        /// <summary>
        /// エラーがあるかどうか判断します。
        /// </summary>
        public bool HasErrors => _errorDic.Any();

        #endregion
    }
}