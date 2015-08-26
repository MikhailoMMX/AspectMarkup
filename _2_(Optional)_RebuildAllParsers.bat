cd GPPG\ParserGenerator
call _GenerateAll.bat
cd ..\..
%windir%\microsoft.net\framework\v4.0.30319\msbuild GPPG\GPPG.csproj

cd System\ParserGenerator
..\..\GPPG\Bin_Original\gplex.exe /unicode /stack /out:ParserGenLexer.cs ParserGen.lex
..\bin\gppg.exe /no-lines /gplex ParserGen.y /out:ParserGenParser.cs
cd ..\..
%windir%\microsoft.net\framework\v4.0.30319\msbuild System\ParserGenerator\ParserGenerator.csproj

cd Parsers\LWCommentParser
call _generateParser.bat
cd ..\..\Parsers\LWParserCS2
call _generateParser.bat
cd ..\..\Parsers\LWParserJava
call _generateParser.bat
cd ..\..\Parsers\LWParserLex
call _generateParser.bat
cd ..\..\Parsers\LWParserLP
call _generateParser.bat
cd ..\..\Parsers\LWParserPascal
call _generateParser.bat
cd ..\..\Parsers\LWParserXML
call _generateParser.bat
cd ..\..\Parsers\LWParserYacc
call _generateParser.bat
cd ..\..\Parsers\TestParser
call _generateParser.bat
cd ..\..

@IF %ERRORLEVEL% NEQ 0 PAUSE