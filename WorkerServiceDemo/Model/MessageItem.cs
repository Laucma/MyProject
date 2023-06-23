using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerServiceDemo.Model
{
    public class MessageItem
    {
        /// <summary>
        /// 消息类型
        /// 1：心跳
        /// 2：登录
        /// </summary>
        public int type;

        /// <summary>
        /// 消息
        /// </summary>
        public string message { get; set; }
    }

    public class LoginMessage
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginItem
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UkeyId { get; set; }
        /// <summary>
        /// yyyyMMddHHmmss
        /// </summary>
        public string TimeStamp { get; set; }
    }
}
