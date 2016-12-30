using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace Ark.Core.Net
{
    /// <summary>
    /// HTTP通信に関する機能を提供します。
    /// </summary>
    public static class HttpManager
    {
        /// <summary>
        /// 指定した URL に POST し、クッキーを取得します。
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="parameters">パラメーター</param>
        /// <param name="timeOut">タイムアウトするまでの期間</param>
        /// <returns>クッキー</returns>
        public static CookieContainer GetCookie(string url, IDictionary<string, string> parameters, TimeSpan timeOut = default(TimeSpan))
        {
            var uri = new Uri(url);
            var cookie = new CookieContainer();

            using (var client = GetClient(timeOut, cookie))
            {
                var content = new FormUrlEncodedContent(parameters);
                var result = client.PostAsync(uri, content).Result;
            }

            return cookie;
        }

        /// <summary>
        /// HTTP 通信を行うためのクライアントを取得します。
        /// </summary>
        /// <param name="timeOut">タイムアウト</param>
        /// <param name="cookie">クッキー</param>
        /// <returns>HTTP クライアント</returns>
        public static HttpClient GetClient(TimeSpan timeOut, CookieContainer cookie = null)
        {
            var handler = new HttpClientHandler();

            if (cookie != null)
            {
                handler.CookieContainer = cookie;
            }

            return new HttpClient(handler)
            {
                Timeout = (timeOut.TotalSeconds < 1) ? TimeSpan.FromSeconds(10) : timeOut
            };
        }
    }
}