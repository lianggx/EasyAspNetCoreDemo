using System;
using System.Threading;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.Extensions.DependencyModel;
using System.Linq;

internal class StartupHook
{
    public static void Initialize()
    {
        Console.WriteLine("\n\n程序集：Ron.License.dll");
        Console.WriteLine("作者：Ron.liang");
        Console.WriteLine("博客地址：https://www.cnblogs.com/viter/\n\n");

        string path = @"C:\Users\Administrator\Source\Repos\Ron.HooksDemo\Ron.Service\bin\Debug\netcoreapp2.2\Ron.Service.dll";
        var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(path);
        dynamic obj = assembly.CreateInstance("Ron.Service.UserService");
        obj.Dispose();

        Console.WriteLine("=========== Ron.License.dll 结束 ===========");
    }
}
