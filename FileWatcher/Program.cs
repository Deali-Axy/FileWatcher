using System;
using System.IO;
using System.Threading;

namespace FileWatcher {
    class Program {
        static void Main(string[] args) {
            var path = args[0];
            if (!Directory.Exists(path)) {
                Console.WriteLine("路径不存在！");
                return;
            }

            var fileSystemWather = new MyFileSystemWather(path, "");
            fileSystemWather.OnChanged += new FileSystemEventHandler(OnChanged);
            fileSystemWather.OnCreated += new FileSystemEventHandler(OnCreated);
            fileSystemWather.OnRenamed += new RenamedEventHandler(OnRenamed);
            fileSystemWather.OnDeleted += new FileSystemEventHandler(OnDeleted);
            fileSystemWather.Start();
            //由于是控制台程序，加个输入避免主线程执行完毕，看不到监控效果
            Console.ReadKey();
        }


        private static void OnCreated(object source, FileSystemEventArgs e) {
            while (true) {
                try {
                    Console.WriteLine($"文件新建：{e.Name}");
                    File.Delete(e.FullPath);
                    break;
                }
                catch (IOException ex) {
                    Console.WriteLine($"删除文件错误：{ex.Message}");
                    Thread.Sleep(1);
                }
            }
        }

        private static void OnChanged(object source, FileSystemEventArgs e) {
            Console.WriteLine("文件改变事件处理逻辑");
        }

        private static void OnDeleted(object source, FileSystemEventArgs e) {
            Console.WriteLine($"文件 {e.Name} 已删除");
        }

        private static void OnRenamed(object source, RenamedEventArgs e) {
            Console.WriteLine("文件重命名事件处理逻辑");
        }
    }
}