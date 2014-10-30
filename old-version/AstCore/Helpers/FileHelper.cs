using System.IO;
using System.Web;

namespace AstCore.Helpers
{
    public class FileHelper
    {
        /// <summary>
        ///     Writes binary file (such as Images) to persistant storage.
        /// </summary>
        /// <param name="fileData">Specify the binrary file content.</param>
        /// <param name="filePath">Specify the relative file path.</param>
        public static void WriteBinaryStorage(byte[] fileData, string filePath)
        {
            // Create directory if not exists.
            var fileInfo = new FileInfo(HttpContext.Current.Server.MapPath(filePath));
            if (!fileInfo.Directory.Exists)
            {
                fileInfo.Directory.Create();
            }

            // Write the binary content.
            File.WriteAllBytes(HttpContext.Current.Server.MapPath(filePath), fileData);
        }
    }
}
