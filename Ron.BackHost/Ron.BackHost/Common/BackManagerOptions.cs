using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ron.BackHost.Common
{
    public class BackManagerOptions
    {
        /// <summary>
        ///  任务名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///  获取或者设置检查时间间隔，单位：毫秒，默认 10 秒
        /// </summary>
        public int CheckTime { get; set; } = 10 * 1000;
        /// <summary>
        ///  回调委托
        /// </summary>
        public Action Callback { get; set; }
        /// <summary>
        ///  执行细节传递委托
        /// </summary>
        public Action<BackHandler> Handler { get; set; }

        /// <summary>
        ///  传递内部信息到外部组件中，以方便处理扩展业务
        /// </summary>
        /// <param name="level">0=Info，1=Debug，2=Error,3=exit</param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <param name="state"></param>
        public void OnHandler(int level, string message, Exception ex = null, object state = null)
        {
            Handler?.Invoke(new BackHandler() { Level = level, Message = message, Exception = ex, State = state });
        }
    }
}
