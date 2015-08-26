%windir%\microsoft.net\framework\v4.0.30319\msbuild IDEPlugins\NPPAspectPlugin\NPPAspectPlugin.csproj
@IF %ERRORLEVEL% NEQ 0 GOTO ERROR

copy /y System\bin\NPPAspectPlugin.dll Release\AspectNPPPlugin.dll

GOTO EXIT

:ERROR
PAUSE

:EXIT