SET publish_project_path=..\src\FileWatcherBackups.Console
SET publish_path=.\publish\

RMDIR /S /Q %publish_path%
dotnet publish %publish_project_path% -c Release -o %publish_path% --self-contained -p:DebugType=None -p:PublishSingleFile=true
PAUSE
