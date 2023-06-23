using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerServiceDemo
{
    public class MactionCode
    {
        /// <summary>
        /// 加密狗信息
        /// </summary>
        public string Ukey { get; set; }

        /// <summary>
        /// mac地址
        /// </summary>
        public string Mac { get; set; }

        /// <summary>
        /// IP
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 主机名
        /// </summary>
        public string HostName { get; set; }
    }
}
