# File Watcher

Based on .Net Core 3.1


## Build

### Linux

```bash
cd FileWatcherNative
dotnet publish -r linux-x64 -c Release -p:PublishSingleFile=true -p:PublishTrimmed=true
```


### Windows

```bash
cd FileWatcherNative
dotnet publish -r win-x64 -c Release -p:PublishSingleFile=true -p:PublishTrimmed=true
```


## 使用方法

```bash
./FileWatcherNative /path/to/watch [filter]
```

`[filter]`可以省略，格式类似：`*.php`、`*.txt`