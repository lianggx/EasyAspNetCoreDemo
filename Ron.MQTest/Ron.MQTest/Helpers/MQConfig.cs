using System;
using System.Collections.Generic;
using System.Text;

namespace Ron.MQTest.Helpers
{
    public class MQConfig
    {
        /// <summary>
        ///  访问消息队列的用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        ///  访问消息队列的密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        ///  消息队列的主机地址
        /// </summary>
        public string HostName { get; set; }
        /// <summary>
        ///  消息队列的主机开放的端口
        /// </summary>
        public int Port { get; set; }
    }
}
