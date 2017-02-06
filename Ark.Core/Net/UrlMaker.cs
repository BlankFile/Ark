using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web;

namespace Ark.Core.Net
{
    /// <summary>
    /// HTTP通信で使用するパラメータを作成する機能を提供します。
    /// </summary>
    public static class UrlMaker
    {
        /// <summary>
        /// GET 通信でのパラメータを作成します。
        /// </summary>
        /// <param name="parameters">パラメータを表すキー/バリューコレクション</param>
        /// <returns>パラメータを表す文字列</returns>
        public static string CreateGetParameter(IDictionary<string, object> parameters)
        {
            var contents = HttpUtility.ParseQueryString(string.Empty);

            foreach (var parameter in parameters)
            {
                if (parameter.Key.IsNullOrWhiteSpace())
                {
                    continue;
                }

                var value = parameter.Value;

                if (value == null)
                {
                    continue;
                }

                contents[parameter.Key] = HttpUtility.UrlEncode(value.ToString());
            }

            return contents.ToString();
        }

        /// <summary>
        /// POST 通信でのパラメータを作成します。
        /// </summary>
        /// <param name="parameters">パラメータを表すキー/バリューコレクション</param>
        /// <returns>パラメータのコンテナー</returns>
        public static FormUrlEncodedContent CreatePostContent(IDictionary<string, object> parameters)
        {
            var contents = new Dictionary<string, string>();

            foreach (var parameter in parameters)
            {
                if (parameter.Key.IsNullOrWhiteSpace())
                {
                    continue;
                }

                var value = parameter.Value;

                if (value == null)
                {
                    continue;
                }

                if (value is byte[])
                {
                    contents[parameter.Key] = HttpUtility.UrlEncode((byte[])value);
                }
                else
                {
                    contents[parameter.Key] = value.ToString();
                }
            }

            return new FormUrlEncodedContent(contents);
        }
    }
}