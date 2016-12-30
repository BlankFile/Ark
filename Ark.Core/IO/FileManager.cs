using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ark.Core.IO
{
    /// <summary>
    /// ファイルを操作する機能を提供します。
    /// </summary>
    public static class FileManager
    {
        /// <summary>
        /// 指定したファイルの中に、対象のファイルが含まれているかどうか判断します。
        /// </summary>
        /// <param name="files">ファイルパスのリスト</param>
        /// <param name="target">対象ファイルのパス</param>
        /// <returns>
        /// True ：含まれている
        /// False：含まれていない
        /// </returns>
        public static bool IsDuplicate(List<string> files, string target)
        {
            return files.Where(x => x != target)
                        .Where(x => IsCompare(x, target))
                        .Any();
        }

        /// <summary>
        /// 指定したファイルの中に、対象のファイルが含まれているかどうか判断します。
        /// </summary>
        /// <param name="files">ファイルパスのリスト</param>
        /// <param name="target">対象ファイルのバイナリ</param>
        /// <returns>
        /// True ：含まれている
        /// False：含まれていない
        /// </returns>
        public static bool IsDuplicate(List<string> files, byte[] target)
        {
            return files.Any(x => IsCompare(x, target));
        }

        /// <summary>
        /// 指定した２つのファイルが等しいかどうか判断します。
        /// </summary>
        /// <param name="file1">比較元ファイルのパス</param>
        /// <param name="file2">比較先ファイルのパス</param>
        /// <returns>
        /// True ：等しい
        /// False：等しくない
        /// </returns>
        public static bool IsCompare(string file1, string file2)
        {
            using (var fs1 = File.OpenRead(file1))
            using (var fs2 = File.OpenRead(file2))
            {
                return IsCompare(fs1, fs2);
            }
        }

        /// <summary>
        /// 指定した２つのファイルが等しいかどうか判断します。
        /// </summary>
        /// <param name="file1">比較元ファイルのパス</param>
        /// <param name="file2">比較先ファイルのバイナリ</param>
        /// <returns>
        /// True ：等しい
        /// False：等しくない
        /// </returns>
        public static bool IsCompare(string file1, byte[] file2)
        {
            using (var fs1 = File.OpenRead(file1))
            using (var fs2 = new MemoryStream(file2))
            {
                return IsCompare(fs1, fs2);
            }
        }

        /// <summary>
        /// 指定した２つのファイルが等しいかどうか判断します。
        /// </summary>
        /// <param name="stream1">比較元ファイルのストリーム</param>
        /// <param name="stream2">比較先ファイルのストリーム</param>
        /// <returns>
        /// True ：等しい
        /// False：等しくない
        /// </returns>
        private static bool IsCompare(Stream stream1, Stream stream2)
        {
            if (stream1.Length != stream2.Length)
            {
                return false;
            }

            int stream1byte;
            int stream2byte;

            do
            {
                stream1byte = stream1.ReadByte();
                stream2byte = stream2.ReadByte();

                if (stream1byte != stream2byte)
                {
                    return false;
                }
            }
            while (stream1byte != -1 || stream2byte != -1);

            return true;
        }

    }
}
