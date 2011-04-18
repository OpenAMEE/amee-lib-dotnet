using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace BergenAmee.Model
{
    /// <summary>
    /// Helper for JSON serialization and deserialization.
    /// </summary>
    public class JsonHelper
    {
        // time in second spent calling JSON serial/deserial
        public static double totalTimeExecution = 0;

        /// <summary>
        /// Serialize object to String
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serialize<T>(T obj)
        {
            string result = null;
            MemoryStream ms = new MemoryStream();
            try
            {
                DateTime startTime = DateTime.Now;
                /*System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = 
                    new System.Runtime.Serialization.Json.DataContractJsonSerializer(obj.GetType(), new List<Type>(), Int16.MaxValue, true,
                        new ZentoSurrogate(), false);*/
                System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(obj.GetType());
                serializer.WriteObject(ms, obj);
                result = Encoding.UTF8.GetString(ms.ToArray());
                double time = (DateTime.Now - startTime).TotalSeconds;
                totalTimeExecution += time;
            }
            catch (Exception e)
            {
                LogHelper.LogError("exception while serializing : " + e.Message + " - " + e.StackTrace);
                throw e;
            }
            finally
            {
                ms.Dispose();
            }
            return result;
        }

        /// <summary>
        /// Deserialize String to Object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string json)
        {
            MemoryStream ms = null;
            T result = result = Activator.CreateInstance<T>();
            try
            {
                DateTime startTime = DateTime.Now;
                ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
                System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(result.GetType());
                /*System.Runtime.Serialization.Json.DataContractJsonSerializer serializer =
                    new System.Runtime.Serialization.Json.DataContractJsonSerializer(obj.GetType(), new List<Type>(), Int16.MaxValue, false,
                        new ZentoSurrogate(), false);*/
                result = (T)serializer.ReadObject(ms);
                double time = (DateTime.Now - startTime).TotalSeconds;
                totalTimeExecution += time;
            }
            catch (Exception e)
            {
                LogHelper.LogError("exception while deserializing : " + e.Message + " - " + e.StackTrace);
                throw e;
            }
            finally
            {
                ms.Close();
                ms.Dispose();
            }
            return result;
        }
    }
}
