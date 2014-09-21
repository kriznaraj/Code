using Controls.Compression;
using Controls.Encryption;
using Controls.Serialization;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;

namespace Controls.Types
{
    /// <summary>
    /// Represents Extension methods
    /// </summary>
    public static class Extension
    {
        private const int Buffer_Size = 4096;

        /// <summary>
        /// Adds the Paramerts of the query to the SqlCommand object
        /// </summary>
        /// <param name="command">SqlCommand instance to add paramters</param>
        /// <param name="criteria">Criteria to convert to parameters</param>
        public static void AddParameter(this SqlCommand command, IQueryCriteria criteria)
        {
            command.AddParameter(criteria.Root);
        }

        /// <summary>
        /// Adds the Paramerts of the query to the SqlCommand object
        /// </summary>
        /// <param name="command">SqlCommand instance to add paramters</param>
        /// <param name="criteria">Criteria to convert to parameters</param>
        public static void AddParameter(this SqlCommand command, ICriteria criteria)
        {
            switch (criteria.Glue)
            {
                case Glue.And:
                case Glue.Or:
                    {
                        command.AddParameter(criteria.Left);
                        command.AddParameter(criteria.Right);
                    }
                    break;

                case Glue.None:
                    {
                        command.Parameters.AddWithValue(criteria.Query.ToString(), criteria.Value);
                    }
                    break;
            }
        }

        /// <summary>
        /// Computes the hash value for the stream from 16th Byte and write into the first 16 bytes of the stream
        /// </summary>
        /// <param name="stream">Stream to compute the hash</param>
        public static void ComputeHash(this Stream stream)
        {
            stream.Seek(16, SeekOrigin.Begin);
            byte[] hash = MD5.Create().ComputeHash(stream);
            stream.Seek(0, SeekOrigin.Begin);
            stream.Write(hash, 0, 16);
        }

        /// <summary>
        /// Copies the data from the source stream to destination stream
        /// </summary>
        /// <param name="source">Source Stream</param>
        /// <param name="destination">Destination Stream</param>
        public static void CopyStream(this Stream source, Stream destination)
        {
            byte[] buffer = new byte[Buffer_Size];
            while (true)
            {
                int readCount = source.Read(buffer, 0, Buffer_Size);
                if (readCount > 0)
                {
                    destination.Write(buffer, 0, readCount);
                }

                if (readCount < Buffer_Size)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Converts the string to Bytes array using Unicode Encoding
        /// </summary>
        /// <param name="input">input string</param>
        /// <returns>returns the byte array</returns>
        public static byte[] GetBytes(this string input)
        {
            return Encoding.Unicode.GetBytes(input);
        }

        /// <summary>
        /// Gets the column value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataTable">The data table.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>Column value</returns>
        /// <exception cref="System.ArgumentException">Specified column name {0} is not associated with datatable</exception>
        public static T GetColumnValue<T>(this DataTable dataTable, string columnName)
        {
            if (dataTable != null && dataTable.Columns.Contains(columnName) == false)
            {
                throw new ArgumentException("Specified column name {0} is not associated with datatable", columnName);
            }
            return dataTable.Rows.Count == 0 ? default(T) : dataTable.Rows[0].Field<T>(columnName);
        }

        /// <summary>
        /// Returns true if type is serializable or false.
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="input">Object to use for serialization</param>
        /// <returns>True or false</returns>
        public static bool IsSerializable<T>(this T input)
        {
            return typeof(T).IsSerializable;
        }

        /// <summary>
        /// Write the Hash value for the given stream to the starting of the stream
        /// </summary>
        /// <param name="stream">stream for which the hash has to be written</param>
        public static void PrepareHeaderInformation<TData>(this Stream stream, TData extendedData)
        {
            byte[] hashValue = new byte[16];
            stream.Write(hashValue, 0, 16);
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                formatter.Serialize(ms, extendedData);
                byte[] header = ms.ToArray();
                byte[] length = BitConverter.GetBytes(header.Length);
                stream.Write(length, 0, length.Length);
                stream.Write(header, 0, header.Length);
            }
        }

        /// <summary>
        /// Retruns bytes from the object
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="input">Object to use for serialization</param>
        /// <param name="iSerialization">Serialization instance</param>
        /// <returns>Serialized byte array</returns>
        public static byte[] ToBytes<T>(this T _object, ISerialization _iSerialization)
        {
            if (IsSerializable<T>(_object) == false)
                throw new InvalidOperationException("This object is not serializable");
            using (Stream stream = _iSerialization.Serialize<T>(_object))
            {
                stream.Seek(0, SeekOrigin.Begin);
                byte[] retValue = new byte[stream.Length];
                stream.Read(retValue, 0, retValue.Length);
                return retValue;
            }
        }

        /// <summary>
        /// Check for the Hash value of the Stream for Valid Data
        /// </summary>
        /// <param name="stream">Stream to check against</param>
        /// <returns>return if hash check succeeded or not</returns>
        public static bool TryGetHeader<TData>(this Stream stream, out TData data)
        {
            byte[] dataHash = new byte[16];
            stream.Read(dataHash, 0, dataHash.Length);
            byte[] hash = MD5.Create().ComputeHash(stream);
            stream.Position = 16;
            bool hashSuccess = false;
            data = default(TData);
            if (hash.SequenceEqual(dataHash))
            {
                hashSuccess = true;
                BinaryFormatter formatter = new BinaryFormatter();
                byte[] length = new byte[4];
                stream.Read(length, 0, length.Length);
                byte[] headerContent = new byte[BitConverter.ToInt32(length, 0)];
                stream.Read(headerContent, 0, headerContent.Length);
                data = (TData)formatter.Deserialize(new MemoryStream(headerContent));
            }

            return hashSuccess;
        }
    }
}