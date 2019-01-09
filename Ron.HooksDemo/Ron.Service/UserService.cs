using System;

namespace Ron.Service
{
    public class UserService : IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine("程序集：Ron.Service.dll");
            Console.WriteLine("动态加载程序集，执行清理任务已完成\n\n");
            Console.WriteLine("=========== Ron.Service.dll 结束 ===========");
        }
    }
}
