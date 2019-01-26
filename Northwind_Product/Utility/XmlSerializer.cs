using System.IO;
using System.Xml.Serialization;

namespace Northwind_Product.Utility
{
    /// <summary>
    /// XML serializer utility
    /// </summary>
    public class XmlSerialization
    {
        /// <summary>
        /// Read from XML file path
        /// </summary>
        /// <typeparam name="T">Type of object to read</typeparam>
        /// <param name="filePath">File path</param>
        /// <returns>Type of object</returns>
        public static T ReadFromXmlFile<T>(string filePath) where T : new()
        {
            TextReader reader = null;
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                reader = new StreamReader(filePath);
                return (T)serializer.Deserialize(reader);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        /// <summary>
        /// Write to XML file path
        /// </summary>
        /// <typeparam name="T">Type of object to write</typeparam>
        /// <param name="filePath">File path</param>
        /// <param name="objectToWrite">Object to write</param>
        /// <param name="append"></param>
        public static void WriteToXmlFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
        {
            TextWriter writer = null;
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                writer = new StreamWriter(filePath, append);
                serializer.Serialize(writer, objectToWrite);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }
    }
}