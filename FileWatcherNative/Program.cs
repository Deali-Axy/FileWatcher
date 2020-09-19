using System;
using System.IO;
using System.Threading;

namespace FileWatcherNative {
    class Program {
        static void Main(string[] args) {
            string path;
            var filter = "";

            switch (args.Length) {
                case 1:
                    path = args[0];
                    if (!Directory.Exists(path)) {
                        Console.WriteLine("路径不存在！");
                        return;
                    }

                    break;
                case 2:
                    path = args[0];
                    if (!Directory.Exists(path)) {
                        Console.WriteLine("路径不存在！");
                        return;
                    }

                    filter = args[1];

                    break;
                default:
                    Console.WriteLine("参数错误！");
                    return;
            }


            var watcher = new FileSystemWatcher {
                Path = path,
                IncludeSubdirectories = true,
                NotifyFilter = NotifyFilters.LastAccess |
                               NotifyFilters.LastWrite |
                               NotifyFilters.FileName |
                               NotifyFilters.DirectoryName |
                               NotifyFilters.Size,
                Filter = filter
            };
            
            watcher.Created += (o, e) => {
                Console.WriteLine($"检查到新文件：{e.Name}");

                // 循环删除
                while (true) {
                    try {
                        File.Delete(e.FullPath);
                        break;
                    }
                    catch (IOException ioException) {
                        Console.WriteLine($"删除文件出错：{ioException.Message}");
                        Thread.Sleep(1);
                    }
                }
            };

            watcher.Changed += (o, e) => {
                Console.WriteLine($"检查到文件被修改：{e.Name}");
            };
            watcher.Deleted += (o, e) => {
                Console.WriteLine($"检查到文件被删除：{e.Name}");
            };
            watcher.Renamed += (o, e) => {
                Console.WriteLine($"检查到文件被重命名：{e.OldName} -> {e.Name}");
            };

            //开始监视
            watcher.EnableRaisingEvents = true;
            
            Console.WriteLine($"开始监控目录：{path}，过滤器：{filter}");
            Console.Read();
        }
    }
}