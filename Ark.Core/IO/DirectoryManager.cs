using System.IO;

namespace Ark.Core.IO
{
    /// <summary>
    /// ディレクトリを操作する機能を提供します。
    /// </summary>
    public static class DirectoryManager
    {
        /// <summary>
        /// ディレクトリを作成します。
        /// </summary>
        /// <param name="path">パス</param>
        public static void Create(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// ディレクトリをコピーします。
        /// </summary>
        /// <param name="sourceDirPath">コピー元ディレクトリのパス</param>
        /// <param name="destDirPath">コピー先ディレクトリのパス</param>
        public static void Copy(string sourceDirPath, string destDirPath)
        {
            if (!Directory.Exists(destDirPath))
            {
                Directory.CreateDirectory(destDirPath);
                File.SetAttributes(destDirPath, File.GetAttributes(sourceDirPath));
            }

            var files = Directory.GetFiles(sourceDirPath);

            foreach (var file in files)
            {
                File.Copy(file, Path.Combine(destDirPath, Path.GetFileName(file)), true);
            }

            var dirs = Directory.GetDirectories(sourceDirPath);

            foreach (var dir in dirs)
            {
                Copy(dir, Path.Combine(destDirPath, Path.GetFileName(dir)));
            }
        }
    }
}