using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Ark.Core.IO
{
    /// <summary>
    /// XMLを変換する機能を提供します。
    /// </summary>
    public static class XmlConverter
    {
        /// <summary>
        /// オブジェクトに変換します。
        /// </summary>
        /// <typeparam name="T">オブジェクトの型</typeparam>
        /// <param name="path">XMLファイルのパス</param>
        /// <returns>オブジェクト</returns>
        public static T Deserialize<T>(string path) where T : class
        {
            var serializer = new DataContractSerializer(typeof(T));

            using (var reader = XmlReader.Create(path))
            {
                return (T)serializer.ReadObject(reader);
            }
        }

    }
}
