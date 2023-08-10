dotnet publish ./src/ifcodes.ifwin.Console `
--runtime win-x64 `
--configuration Release `
--self-contained false `
--output $env:USERPROFILE\\software `
-p:PublishSingleFile=true -p:DebugSymbols=false -p:DebugType=None
$null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown')