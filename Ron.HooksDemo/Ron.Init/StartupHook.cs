using System;
using System.Net;

internal class StartupHook
{
    public static void Initialize()
    {
        Console.WriteLine("程序集：Ron.Init.dll");
        Console.WriteLine("正在获取服务器信息.....");
        string[] drives = Environment.GetLogicalDrives();
        Console.WriteLine("machineName:{0},\nOSVersion:{1},\nversion:{2},\nuserName:{3},\nCurrentDirectory:{4}\nCore Count:{5}\nWorkSet:{6}\nDrives:{7}",
            Environment.MachineName,
            Environment.OSVersion,
            Environment.Version,
            Environment.UserName,
            Environment.CurrentDirectory,
            Environment.ProcessorCount,
            Environment.WorkingSet,
            string.Join(",", drives));

        Console.WriteLine("\n\n正在获取网络配置.....");
        var hostName = Dns.GetHostName();
        Console.WriteLine("HostName:{0}", hostName);
        var addresses = Dns.GetHostAddresses(hostName);
        foreach (var item in addresses)
        {
            IPAddress ip = item.MapToIPv4();
            Console.WriteLine("AddressFamily:{0} \tAddress:{1}", ip.AddressFamily, ip);
        }

        Console.WriteLine("\n\n正在上报启动信息.....");
        Console.WriteLine("=========== Ron.Init.dll 结束 ===========");
    }
}