using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ron.JsonTest.Models
{
    public class UserInfo
    {
        /// <summary>
        ///  姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///  性别
        /// </summary>
        public bool Gender { get; set; }
        /// <summary>
        ///  年龄
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        ///  注册时间
        /// </summary>
        public DateTime RegTime { get; set; }
    }
}
