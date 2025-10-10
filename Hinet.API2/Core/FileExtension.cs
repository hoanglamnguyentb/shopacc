using System.IO;

namespace Hinet.API2
{
    /// <summary>
    /// File extension
    /// </summary>

    public class FileExtension
    {
        /// <summary>
        /// Save file byte[]
        /// </summary>
        /// <param name="Data">Dữ liệu byte[]</param>
        /// <param name="Path"> Đường dẫn tuyệt đôi + name</param>
        public static bool SaveData(byte[] Data, string Path)
        {
            BinaryWriter Writer = null;
            try
            {
                // Create a new stream to write to the file
                Writer = new BinaryWriter(File.OpenWrite(Path));

                // Writer raw data
                Writer.Write(Data);
                Writer.Flush();
                Writer.Close();
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}