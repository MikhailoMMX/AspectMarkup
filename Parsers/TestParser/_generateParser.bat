..\..\System\bin\ParserGenerator.exe ParserDescriptor.lp /l LWLexer.lex /y LWParser.y /cs LWParserPart.cs

..\..\GPPG\Bin_Original\gplex.exe /unicode /stack /out:LWLexer.cs LWLexer.lex 
..\..\System\bin\gppg.exe /no-lines /gplex LWParser.y

%windir%\microsoft.net\framework\v4.0.30319\msbuild TestParser.csproj

@IF %ERRORLEVEL% NEQ 0 GOTO ERROR
GOTO EXIT
:ERROR
PAUSE

:EXIT