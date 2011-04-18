using System;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.IO.Compression;
using BergenAmee.Model;

namespace BergenAmee
{
    /// <summary>
    /// Helper for AMEE WS access.
    /// </summary>
    public class AmeeHelper
    {
        // AMEE host, login, password and encoding
        public static String ameeHost;
        public static int ameePort;
        public static String ameeLogin;
        public static String ameePassword;
        public static String ameeCharSetName;
        public static String ameeAccept;
        public static String ameePrefix;

        // the authentification token
        public static String authToken;

        // contains cookies information
        public static Hashtable cookiesMap = new Hashtable();

        // for send request
        public static readonly Encoding DefaultEncoding = Encoding.UTF8;
        private static readonly byte[] LineTerminator = new byte[] { 13, 10 };

        // time in second spent calling AMEE's WS
        public static double totalTimeExecution = 0;

        // used to connect to AMEE
        public static IPHostEntry ipAddress = null;
        public static IPEndPoint ip = null;

        public static String errorAmeeDuplicate = "A POST or PUT request was received which would have resulted in a duplicate resource being created.";

        /*
        public static string getAMEECountryName(string countryName)
        {
            // United Kingdom
            return countryName.Replace(" ", "%20");
        }
         */

        /// <summary>
        /// Get the country name used by AMEE ; now we only replace the " " to "%20", the html code of space
        /// </summary>
        /// <param name="countryName"></param>
        /// <returns></returns>
        public static string getAMEECountryName(string countryName)
        {
            // United Kingdom
            return countryName.Replace(" ", "%20");
        }

        /// <summary>
        /// Login to AMEE and return the token ID to authenticate the next calls
        /// </summary>
        /// <returns>authentification token</returns>
        public static String getAuthToken()
        {
            LogHelper.Log("Getting authentification token for [" + ameeHost + ":" + ameePort + "] using user [" + ameeLogin + "] ...");

            String response = sendRequest("POST /auth", "username=" + ameeLogin + "&password=" + ameePassword
                , "application / x - www - form - urlencoded", "get authentification token");

            return findToken(response);
        }

        /// <summary>
        /// Create a profile by calling AMEE and return the WS response
        /// </summary>
        /// <param name="mySQLConnect"></param>
        /// <returns>WS response</returns>
        public static String createProfile(int meterId)
        {
            return sendRequest("POST /profiles", "profile=true", "application / x - www - form - urlencoded", "create profile");
        }

        /// <summary>
        /// Get data item id using drill down mechanism
        /// </summary>
        /// <param name="dataPath"></param>
        /// <returns></returns>
        public static String getDataItemByDrillDown(String dataPath)
        {
            return sendRequest("GET /data/" + dataPath, "", "application / x - www - form - urlencoded", "get data item id");
        }

        /// <summary>
        /// store profile item value
        /// http://my.amee.com/developers/wiki/ProfileItem#CreateanewProfileItem
        /// </summary>
        /// <param name="profileId"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public static String setProfilItemValues(String profileId, String dataPath, String body)
        {
            return sendRequest("POST /profiles/" + profileId + "/" + dataPath, body, "application/x-www-form-urlencoded", "create profile item value");
        }

        /// <summary>
        /// Get the profile ID of an http response (got from an http create profile request)
        /// </summary>
        /// <param name="httpResponse"></param>
        /// <returns></returns>
        public static String getProfileId(String httpResponse)
        {
            Profiles profiles = JsonHelper.Deserialize<Profiles>(findHttpContent(httpResponse));
            if (profiles.status != null)
            {
                throw new Exception("AMEE call failed : code:" + profiles.status.code + "  description: " + profiles.status.description);
            }
            try
            {
                return profiles.profile.uid;
            }
            catch (Exception e)
            {
                LogHelper.LogError("AMEE call failed : no profile id could be found : " + e.Message + " - " + e.StackTrace);
                throw new Exception("AMEE call failed : no profile id could be found");
            }
        }

        /// <summary>
        /// get the data item id
        /// </summary>
        /// <param name="httpResponse"></param>
        /// <returns></returns>
        public static String getDataItemId(String httpResponse)
        {
            DataItem dataItems = JsonHelper.Deserialize<DataItem>(findHttpContent(httpResponse));
            if (dataItems.status != null)
            {
                throw new Exception("AMEE call failed : code:" + dataItems.status.code + "  description: " + dataItems.status.description);
            }
            try
            {
                return dataItems.choices.choices[0].name;
            }
            catch (Exception e)
            {
                LogHelper.LogError("AMEE call failed : no data item id could be found : " + e.Message + " - " + e.StackTrace);
                throw new Exception("AMEE call failed : no data item id could be found");
            }
        }

        /// <summary>
        /// get the CO2 of the profile item id
        /// </summary>
        /// <param name="httpResponse"></param>
        /// <returns></returns>
        public static ProfileItemResource getProfileItem(String httpResponse)
        {
            ProfileItemResource profileItemRessource = JsonHelper.Deserialize<ProfileItemResource>(findHttpContent(httpResponse));
            if (profileItemRessource.status != null)
            {
                if (profileItemRessource.status.description != errorAmeeDuplicate)
                {
                    throw new Exception("AMEE call failed : code:" + profileItemRessource.status.code + "  description: " + profileItemRessource.status.description);
                } else {
                    // return null because data already sent and we dont want to set this as an error
                    return null;
                }
            }
            return profileItemRessource;
        }

        /// <summary>
        /// Return the parameter date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static String getParamDate(int date)
        {
            String strDate = ("" + date);
            String year = strDate.Substring(0, 4);
            String month = strDate.Substring(4, 2);
            String day = strDate.Substring(6, 2);

            return year + "-" + month + "-" + day + "T18%3A19%3A02Z";
        }


        /// <summary>
        /// http://stackoverflow.com/questions/523930/sockets-in-c-how-to-get-the-response-stream
        /// </summary>
        /// <param name="path"></param>
        /// <param name="body"></param>
        /// <param name="contentType"></param>
        /// <param name="actionMsg">message to be logged to get the time of execution</param>
        /// <returns></returns>
        private static String sendRequest(String path, String body, String contentType, String actionMsg)
        {
            String response = "";
            using (var socket = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp))
            {
                // taking time from socket.connect to socket.close
                DateTime startTime = DateTime.Now;
                socket.Connect(ip);
                using (var n = new NetworkStream(socket))
                {
                    String param = path + " HTTP/1.1";
                    if (authToken != null)
                    {
                        param += "\nAuthToken: " + authToken;
                    }

                    IEnumerable<string> httpParams = new[] {
                        param,
                        "Accept: " + ameeAccept,
                        "Host: " + AmeeHelper.ameeHost,
                        "Connection: Close",                      // this will speed reading last byte (-1) 15 secs -> 0.1 secs
                        "Content-Type: " + contentType,
                        "Content-Length: " + body.Length,
                        "",
                        body};

                    AmeeHelper.writeRequest(n, httpParams);
                    try {
                        var lineBuffer = new List<byte>();
                        int b = n.ReadByte();
                        while (b != -1)
                        {
                            lineBuffer.Add((byte)b);
                            b = n.ReadByte();
                        }
                        response += DefaultEncoding.GetString(lineBuffer.ToArray());
                    }
                    finally
                    {
                        n.Close();
                        double time = (DateTime.Now - startTime).TotalSeconds;
                        totalTimeExecution += time;
                        LogHelper.Log("--AMEE CALL [" + actionMsg + "] in " + time + " s");
                    }
                }
            }
            return response;
        }

        /// <summary>
        /// Writes the request to call the WS
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="request"></param>
        private static void writeRequest(Stream stream, IEnumerable<string> request)
        {
            foreach (var r in request)
            {
                var data = Encoding.UTF8.GetBytes(r);
                stream.Write(data, 0, data.Length);
                stream.Write(LineTerminator, 0, 2);
            }
            stream.Write(LineTerminator, 0, 2);
            ReadLine(stream);
        }

        /// <summary>
        /// Reads the stream response
        /// FIXME : we don't use readLine anymore ...
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        private static string ReadLine(Stream stream)
        {
            DateTime startTime = DateTime.Now;
            var lineBuffer = new List<byte>();
            while (true)
            {
                int b = stream.ReadByte();
                if (b == -1)
                {
                    return null;
                }
                // 10 = new line
                if (b == 10)
                {
                    break;
                }
                // 13 = carriage return
                if (b != 13)
                {
                    lineBuffer.Add((byte)b);
                }
            }
            return DefaultEncoding.GetString(lineBuffer.ToArray());
        }

        /// <summary>
        /// Get the content of an http response (skip the header)
        /// FIXME : very ugly method ! (split and trim)
        /// </summary>
        /// <param name="httpResponse">the http response</param>
        /// <returns>the http content</returns>
        private static String findHttpContent(String httpResponse)
        {
            // get body of the response
            bool foundXml = false;
            String[] lines = httpResponse.Split('\n');
            String content = "";
            for (int i = 0; i < lines.Length; i++)
            {
                if (foundXml || lines[i].IndexOf(ameePrefix) == 0)
                {
                    if (lines[i].Trim() != "")
                    {
                        content += lines[i];
                    }
                    foundXml = true;
                }
            }
            return content;
        }

        /// <summary>
        /// Parse response to get token
        /// FIXME : very ugly method ! (split and trim)
        /// </summary>
        /// <param name="httpResponse"></param>
        /// <returns></returns>
        private static String findToken(String httpResponse)
        {
            String token = null;
            String[] lines = httpResponse.Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].IndexOf("authToken:") >= 0 || lines[i].IndexOf("Authtoken:") >= 0)
                {
                    String[] atv = lines[i].Split(' ');
                    token = atv[1].Trim();
                }
            }
            return token;
        }
    }

    public class AMEERequest
    {
        public String dataPath { get; set; }
        public String dataPathDrillDown { get; set; }
        public String body { get; set; }

    }
}
