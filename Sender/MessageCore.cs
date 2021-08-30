using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Sender.Models;

namespace Sender
{
    public class MessageCore
    {
        private string apiKey;
        private string apiSecret;
        private string prefix;
        private string protocol;
        private string domain;

        public MessageCore(
            string apiKey,
            string apiSecret,
            string domain = "api.solapi.com",
            string protocol = "https",
            string prefix = ""
            )
        {
            this.apiKey = apiKey;
            this.apiSecret = apiSecret;
            this.domain = domain;
            this.protocol = protocol;
            this.prefix = prefix;
        }

        private JsonSerializerSettings JsonSettings = new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver()//프로퍼티 대문자를 소문자로 변경
        };

        public static string GetSignature(string apiKey, string data, string apiSecret)
        {
            System.Security.Cryptography.HMACSHA256 sha256 = new System.Security.Cryptography.HMACSHA256(System.Text.Encoding.UTF8.GetBytes(apiSecret));
            byte[] hashValue = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(data));
            string hash = Strings.Replace(BitConverter.ToString(hashValue), "-", "");
            return hash.ToLower();
        }

        public string GetSalt(int len = 32)
        {
            string s = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random r = new Random();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 1; i <= len; i++)
            {
                int idx = r.Next(0, 35);
                sb.Append(s.Substring(idx, 1));
            }
            return sb.ToString();
        }

        public string GetAuth(string apiKey, string apiSecret)
        {
            string salt = GetSalt();
            string dateStr = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
            string data = dateStr + salt;

            return "HMAC-SHA256 apiKey=" + apiKey + ", date=" + dateStr + ", salt=" + salt + ", signature=" + GetSignature(apiKey, data, apiSecret);
        }

        public string GetUrl(object path)
        {
            string url = protocol + "://" + domain;
            if (!string.IsNullOrEmpty(prefix))
            {
                url += prefix;
            }
            url += path;
            return url;
        }

        public Response Request(string path, string method, string data = Constants.vbNullString)
        {
            string auth = GetAuth(apiKey, apiSecret);

            try
            {
                System.Net.WebRequest req = System.Net.WebRequest.Create(GetUrl(path));
                req.Method = method;
                req.Headers.Add("Authorization", auth);
                req.Headers.Add("Content-type", "application/json; charset=utf-8");

                if (!string.IsNullOrEmpty(data))
                {
                    using (var writer = new System.IO.StreamWriter(req.GetRequestStream()))
                    {
                        writer.Write(data);
                        writer.Close();
                    }
                }

                using (System.Net.WebResponse response = req.GetResponse())
                {
                    using (System.IO.StreamReader streamReader = new System.IO.StreamReader(response.GetResponseStream()))
                    {
                        var jsonResponseText = streamReader.ReadToEnd();
                        JObject jsonObj = JObject.Parse(jsonResponseText);
                        return new Response()
                        {
                            StatusCode = System.Net.HttpStatusCode.OK,
                            Data = jsonObj,
                            ErrorCode = null,
                            ErrorMessage = null
                        };
                    }
                }
            }
            catch (System.Net.WebException ex)
            {
                using (System.IO.StreamReader streamReader = new System.IO.StreamReader(ex.Response.GetResponseStream()))
                {
                    var jsonResponseText = streamReader.ReadToEnd();
                    JObject jsonObj = JObject.Parse(jsonResponseText);
                    string ErrorCode = jsonObj.SelectToken("errorCode").ToString();
                    string ErrorMessage = jsonObj.SelectToken("errorMessage").ToString();
                    System.Net.HttpWebResponse httpResp = (System.Net.HttpWebResponse)ex.Response;
                    return new Response()
                    {
                        StatusCode = httpResp.StatusCode,
                        Data = jsonObj,
                        ErrorCode = ErrorCode,
                        ErrorMessage = ErrorMessage
                    };
                }
            }
            catch (Exception ex)
            {
                string ErrorCode = "Unknown Exception";
                string ErrorMessage = ex.Message;

                return new Response()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Data = null,
                    ErrorCode = ErrorCode,
                    ErrorMessage = ErrorMessage
                };
            }
        }

        public Response SendMessages(MessageContainer messages)
        {
            return Request("/messages/v4/send-many", "POST", JsonConvert.SerializeObject(messages, Formatting.None, JsonSettings));
        }

        public Response GetBalance()
        {
            return Request("/cash/v1/balance", "GET");
        }

        public Response GetGroupList()
        {
            return Request("/messages/v4/groups", "GET");
        }
    }
}
