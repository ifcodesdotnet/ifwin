dotnet publish ./src/ifcodes.ifwin.Console ^
--runtime win-x64 ^
--configuration Release ^
--self-contained false ^
--output %userprofile%\\software ^
-p:PublishSingleFile=true -p:DebugSymbols=false -p:DebugType=None
@pause