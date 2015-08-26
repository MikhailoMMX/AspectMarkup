..\..\GPPG\Bin_Original\gplex.exe /unicode /stack /out:LWLexer.cs Grammar.lex
..\..\System\bin\gppg.exe /no-lines /gplex Grammar.y

%windir%\microsoft.net\framework\v4.0.30319\msbuild LWParserCS.csproj

@IF %ERRORLEVEL% NEQ 0 GOTO ERROR
GOTO EXIT
:ERROR
PAUSE

:EXIT