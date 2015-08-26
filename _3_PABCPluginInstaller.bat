%windir%\microsoft.net\framework\v4.0.30319\msbuild IDEPlugins\PascalABCAspects\PascalABCAspects.csproj
@IF %ERRORLEVEL% NEQ 0 GOTO ERROR

cd ReleaseGenerators
makensis PascalABCNETPlugin.nsi
cd ..
@IF %ERRORLEVEL% NEQ 0 GOTO ERROR

GOTO EXIT

:ERROR
PAUSE

:EXIT