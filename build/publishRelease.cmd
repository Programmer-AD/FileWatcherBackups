SET publish_path=.\publish\

RMDIR /S /Q %publish_path%
dotnet publish ../FileWatcherBackups.Console -c Release -o %publish_path% --self-contained -p:DebugType=None -p:PublishSingleFile=true
PAUSE
