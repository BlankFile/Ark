using System;
using System.Security.Cryptography;
using System.Text;

namespace Ark.Core.Cryptography
{
    /// <summary>
    /// AES暗号手法を提供します。
    /// </summary>
    public static class AesMethod
    {
        /// <summary>
        /// 暗号化します。
        /// </summary>
        /// <param name="password">暗号化する文字列</param>
        /// <param name="encryptKey">暗号キー</param>
        /// <param name="salt">ソルト</param>
        /// <param name="stretchCount">ストレッチ回数</param>
        /// <returns>暗号</returns>
        public static string Encrypt(string password, string encryptKey, string salt, int stretchCount)
        {
            var encString = string.Empty;

            if (!password.IsNullOrEmpty())
            {
                using (var aes = CreateAes(encryptKey, salt, stretchCount))
                using (var encryptor = aes.CreateEncryptor())
                {
                    var bytesPassword = Encoding.UTF8.GetBytes(password);
                    var encBytes = encryptor.TransformFinalBlock(bytesPassword, 0, bytesPassword.Length);

                    encString = Convert.ToBase64String(encBytes);
                }
            }

            return encString;
        }

        /// <summary>
        /// 復号化します。
        /// </summary>
        /// <param name="encryptString">暗号</param>
        /// <param name="encryptKey">暗号キー</param>
        /// <param name="salt">ソルト</param>
        /// <param name="stretchCount">ストレッチ回数</param>
        /// <returns>暗号化前の文字列</returns>
        public static string Decrypt(string encryptString, string encryptKey, string salt, int stretchCount)
        {
            var decString = string.Empty;

            if (!encryptString.IsNullOrEmpty())
            {
                using (var aes = CreateAes(encryptKey, salt, stretchCount))
                using (var decryptor = aes.CreateDecryptor())
                {
                    var bytesPassword = Convert.FromBase64String(encryptString);
                    var decBytes = decryptor.TransformFinalBlock(bytesPassword, 0, bytesPassword.Length);

                    decString = Encoding.UTF8.GetString(decBytes);
                }
            }

            return decString;
        }

        /// <summary>
        /// AES暗号オブジェクトを生成します。
        /// </summary>
        /// <param name="encryptKey">暗号キー</param>
        /// <param name="salt">ソルト</param>
        /// <param name="stretchCount">ストレッチ回数</param>
        /// <returns>暗号オブジェクト</returns>
        private static RijndaelManaged CreateAes(string encryptKey, string salt, int stretchCount)
        {
            var aes = new RijndaelManaged();

            try
            {
                aes.BlockSize = 256;
                aes.KeySize = 256;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (var deriveBytes = new Rfc2898DeriveBytes(encryptKey, Convert.FromBase64String(salt)))
                {
                    deriveBytes.IterationCount = stretchCount;

                    aes.Key = deriveBytes.GetBytes(aes.KeySize / 8);
                    aes.IV = deriveBytes.GetBytes(aes.BlockSize / 8);
                }
            }
            catch
            {
                aes.Dispose();
                throw;
            }

            return aes;
        }
    }
}
