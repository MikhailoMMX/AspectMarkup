cd ReleaseGenerators
VersionInfoIncrementRev.exe
move /y VersionInfo.cs ..\VersionInfo.cs
@IF %ERRORLEVEL% NEQ 0 PAUSE