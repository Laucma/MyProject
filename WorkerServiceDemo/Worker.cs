using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkerServiceDemo.Model;

namespace WorkerServiceDemo
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

       // EMSSmart1000 eMSSmart1000 = new EMSSmart1000();
        private string ukeyIndentity = "";
        private string getUkeyError = "";
        private string refreshToken = "";
        private string baseUrl = "";

        public Worker(ILogger<Worker> logger, IConfiguration config)
        {
            var item = new MessageItem
            {
                type = 1,
                message = JsonConvert.SerializeObject(new LoginMessage()
                {
                    UserName = "admin",
                    Password = "1q2w3E*"
                })
            };
            var js = JsonConvert.SerializeObject(item);
            _logger = logger;
            baseUrl = config["Logging:WebApi:BaseUrl"];
        }
        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            var server = new Fleck.WebSocketServer("ws://0.0.0.0:5555");
            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    _logger.LogInformation("Client Connected.");
                };
                socket.OnClose = () =>
                {
                    _logger.LogInformation("Client DisConnected.");
                    //清token
                };
                socket.OnMessage = message =>
                {
                    _logger.LogInformation("Client Message:" + message);
                    try
                    {
                        MessageItem item = JsonConvert.DeserializeObject<MessageItem>(message);
                        if (item != null)
                        {
                            switch (item.type)
                            {
                                case 1://心跳
                                    MessageItem ret = new MessageItem()
                                    {
                                        type = string.IsNullOrEmpty(getUkeyError) ? 200 : 400,
                                        message = JsonConvert.SerializeObject(new ErrorMessage()
                                        {
                                            error = "heartbeat",
                                            error_description = getUkeyError
                                        })
                                    };
                                    socket.Send(JsonConvert.SerializeObject(ret));
                                    break;
                                case 2:
                                    var login = JsonConvert.DeserializeObject<LoginMessage>(item.message);
                                    var result = RemoteLogin(login);
                                    socket.Send(JsonConvert.SerializeObject(result));
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    catch { }
                };
            });
            await base.StartAsync(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                CheckUkeyStatus();
                await Task.Delay(1000);
            }
        }

        private MessageItem RemoteLogin(LoginMessage login)
        {
            //获取Ukey里面的用户名
            ErrorMessage error = null;

            try
            {
                var userName = GetUserName();
                var un = Md5Encript(login.UserName, "");
                if (userName != un)
                {
                    error = new ErrorMessage()
                    {
                        error = "invalid_grant",
                        error_description = "用户名无效"
                    };
                }
            }
            catch (Exception ex)
            {
                error = new ErrorMessage()
                {
                    error = "invalid_grant",
                    error_description = ex.Message
                };
            }

            if (error != null)
            {
                return new MessageItem()
                {
                    type = 400,
                    message = JsonConvert.SerializeObject(error)
                };
            }

            //基于httpclient请求url登录
            var timeStamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            System.Collections.Specialized.NameValueCollection PostVars = new System.Collections.Specialized.NameValueCollection();
            PostVars.Add("client_id", "Xinning_Api");
            PostVars.Add("client_secret", "Xinning@2021*");
            PostVars.Add("grant_type", "password");
            PostVars.Add("username", login.UserName);
            PostVars.Add("password", login.Password);
            PostVars.Add("timeStamp", timeStamp);
            PostVars.Add("keyId", Md5Encript(ukeyIndentity, timeStamp));

            var ret = HttpClientHelp.HttpClientDoPost($"{baseUrl}connect/token", PostVars).Result;

            MessageItem result = new MessageItem()
            {
                type = (int)ret.statusCode,
                message = ret.responseMsg
            };

            result.type = (int)ret.statusCode;

            if (ret.statusCode == HttpStatusCode.OK)
            {
                var tokens = JsonConvert.DeserializeObject<LoginTokens>(ret.responseMsg);
                refreshToken = tokens.refresh_token;
            }
            return result;
        }

        /// <summary>
        /// 检测Ukey状态
        /// </summary>
        private void CheckUkeyStatus()
        {
            string key = $"{Constants.ClientID}:{Constants.ClientSecret}";
          


            var timeStamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            System.Collections.Specialized.NameValueCollection PostVars = new System.Collections.Specialized.NameValueCollection();
            PostVars.Add("Authorization", $"Basic {Convert.ToBase64String(Encoding.GetEncoding("UTF-8").GetBytes(key))}");
            PostVars.Add("grant_type", "client_credentials");
            PostVars.Add("User-Agent", "ClubOffApp");
            PostVars.Add("Content-Type", "application/x-www-form-urlencoded");
            //PostVars.Add("Content-Type", "application/json");
            //PostVars.Add("charset", "UTF-8");
            //PostVars.Add("grant_type", "client_credentials");
            //PostVars.Add("client_id", "TESTAPPS01");
            //PostVars.Add("client_secret", "TESTFKRAPPS01");
            //PostVars.Add("client_name", "TestApps");
            //PostVars.Add("username", login.UserName);
            //PostVars.Add("password", login.Password);
            //PostVars.Add("timeStamp", timeStamp);
            //PostVars.Add("keyId", Md5Encript(ukeyIndentity, timeStamp));

            var ret = HttpClientHelp.HttpClientDoPost($"https://test2.rcapi.jp/oauth/token", PostVars).Result;

            MessageItem result = new MessageItem()
            {
                type = (int)ret.statusCode,
                message = ret.responseMsg
            };

            result.type = (int)ret.statusCode;
            //getUkeyError = "";
            //eMSSmart1000.GetUID(ref ukeyIndentity, ref getUkeyError);
            //if (!string.IsNullOrEmpty(getUkeyError))
            //{
            //    //基于httpclient请求url  刷新token
            //}
            //else
            //{

            //}
        }

        public string Md5Encript(string msg, string key)
        {
            string pwd = "";
            MD5 md5 = MD5.Create();
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(msg + key));

            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                pwd = pwd + s[i].ToString("x2");
            }
            return pwd;
        }

        public string GetUserName()
        {
            string strRegistCode = "";
            //string strError = "";
            //byte[] data = new byte[128];
            //StringBuilder sb = new StringBuilder();
            //if (eMSSmart1000.GetUserName(ref data, ref strError))
            //{
            //    strRegistCode = System.Text.Encoding.UTF8.GetString(data);
            //}
            //else
            //{
            //    throw new Exception(strError);
            //}
            return strRegistCode;
        }

        public override void Dispose()
        {

        }
    }
}
