using System;
using System.Security.Cryptography;

namespace Ark.Core.Cryptography
{
    /// <summary>
    /// 暗号に使用する乱数を生成するための機能を提供します。
    /// </summary>
    public static class RandomNumberCreator
    {
        /// <summary>
        /// 暗号乱数ジェネレーターを使用して、ランダムなバイト配列を生成します。
        /// </summary>
        /// <param name="byteLength">配列のサイズ</param>
        /// <returns>ランダムバイト配列</returns>
        public static byte[] CreateByteArray(int byteLength)
        {
            var array = new byte[byteLength];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetNonZeroBytes(array);
            }

            return array;
        }

        /// <summary>
        /// 暗号乱数ジェネレーターを使用して、ランダムな文字列を生成します。
        /// </summary>
        /// <param name="byteLength">文字列の元になる配列のサイズ</param>
        /// <returns>ランダム文字列</returns>
        public static string CreateBase64String(int byteLength)
        {
            return Convert.ToBase64String(CreateByteArray(byteLength));
        }
    }
}