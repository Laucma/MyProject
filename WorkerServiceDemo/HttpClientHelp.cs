using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WorkerServiceDemo.Model;

namespace WorkerServiceDemo
{
    public class HttpClientHelp
    {
        public static async Task<string> HttpClientDoGet(string uri, string data)
        {
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.None };

            using (var httpclient = new HttpClient(handler))
            {
                httpclient.BaseAddress = new Uri(uri);
                httpclient.DefaultRequestHeaders.Accept.Clear();
                httpclient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await httpclient.GetAsync(data);

                if (response.IsSuccessStatusCode)
                {
                    Stream myResponseStream = await response.Content.ReadAsStreamAsync();
                    StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                    string retString = myStreamReader.ReadToEnd();
                    myStreamReader.Close();
                    myResponseStream.Close();

                    return retString;
                }
                return "";
            }
        }

        public static async Task<HttpResponseItem> HttpClientDoPost(string uri, NameValueCollection items)
        {
            HttpResponseItem result = new HttpResponseItem();
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.None };

            using (var httpclient = new HttpClient(handler))
            {
                httpclient.BaseAddress = new Uri(uri);

                List<KeyValuePair<string, string>> paras = new List<KeyValuePair<string, string>>();
                for (int i = 0; i < items.Count; i++)
                {
                    paras.Add(new KeyValuePair<string, string>(items.GetKey(i), items.Get(i)));
                }
                var content = new FormUrlEncodedContent(paras);

                var response = await httpclient.PostAsync(uri, content);
                var resonpsemsg = response.Content.ReadAsStringAsync().Result;

                result.statusCode = response.StatusCode;
                result.responseMsg = resonpsemsg;
            }
            return result;
        }

        public void Test() 
        {
//            HttpPost httpPost = new HttpPost("http://192.168.11.11/login.html");
//            httpPost.addHeader("Content-Type", "application/x-www-form-urlencoded;charset=UTF-8"); Map < String, String > params = new HashMap<>();
////注意参数变成中文
//params.put("user", "张三");
//params.put("pswd", "admin");
//            List<NameValuePair> nvps = new ArrayList<>();
//            Set<String> keySet = params.keySet();
//            for (String key : keySet)
//            {
//                nvps.add(new BasicNameValuePair(key, params.get(key)));
//            }//指定字符编码参数  0.0
//            httpPost.setEntity(new UrlEncodedFormEntity(nvps, "utf-8"));
//            httpClient.execute(httpPost);
        }
    }
}
