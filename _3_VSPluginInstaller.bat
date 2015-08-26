%windir%\microsoft.net\framework\v4.0.30319\msbuild IDEPlugins\AspectVSPackage\AspectVSPackage.csproj /p:VisualStudioVersion=12.0
@IF %ERRORLEVEL% NEQ 0 GOTO ERROR

move /y System\bin\AspectVSPackage.vsix Release\AspectVSPackage.vsix

GOTO EXIT

:ERROR
PAUSE

:EXIT