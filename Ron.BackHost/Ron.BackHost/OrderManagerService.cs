using Ron.BackHost.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ron.BackHost
{
    public class OrderManagerService
    {
        public void CheckOrder()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("==业务执行完成==");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void OnBackHandler(BackHandler handler)
        {
            switch (handler.Level)
            {
                default:
                case 0: break;
                case 1:
                case 3: Console.ForegroundColor = ConsoleColor.Yellow; break;
                case 2: Console.ForegroundColor = ConsoleColor.Red; break;
            }
            Console.WriteLine("{0} | {1} | {2} | {3}", handler.Level, handler.Message, handler.Exception, handler.State);
            Console.ForegroundColor = ConsoleColor.Gray;

            if (handler.Level == 2)
            {
                // 服务执行出错，进行补偿等工作
            }
            else if (handler.Level == 3)
            {
                // 退出事件，清理你的业务
                CleanUp();
            }
        }

        public void CleanUp()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("==清理完成==");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
