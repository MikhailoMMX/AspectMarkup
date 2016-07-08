; The name of the installer
Name "AspectCore"

; The file to write
OutFile ..\Release\AspectCore.exe

; The default installation directory
InstallDir $PROGRAMFILES\AspectCore

; Registry key to check for directory (so if you install again, it will 
; overwrite the old one automatically)
InstallDirRegKey HKLM "Software\AspectCore" "Install_Dir"

; Request application privileges for Windows Vista
RequestExecutionLevel admin

ShowInstDetails show

;Page components
Page directory
Page instfiles

UninstPage uninstConfirm
UninstPage instfiles

;--------------------------------

; The stuff to install
Section "AspectCore"

  SectionIn RO

  SetRegView 32
  WriteRegStr HKLM "Software\AspectCore" "Install_Dir" "$INSTDIR"
  SetRegView 64
  
  WriteRegStr HKLM "Software\AspectCore" "Install_Dir" "$INSTDIR"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\AspectCore" "DisplayName" "Aspect Core"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\AspectCore" "UninstallString" '"$INSTDIR\uninstall.exe"'

  CreateDirectory "$INSTDIR\Parsers"
  SetOutPath "$INSTDIR\Parsers"
  File "..\System\bin\LWParserCS2.dll"
  File "..\System\bin\LWParserCPP.dll"
  File "..\System\bin\LWParserYacc.dll"
  File "..\System\bin\LWParserPascal.dll"
  File "..\System\bin\LWParserXML.dll"
  File "..\System\bin\LWParserJava.dll"
  File "..\System\bin\LWParserLP.dll"
  
  CreateDirectory "$INSTDIR\ParserSources"
  SetOutPath "$INSTDIR\ParserSources"
  File /oname=CSharp.lp "..\Parsers\LWParserCS2\ParserDescriptor.lp"
  File /oname=CPP.lp "..\Parsers\LWParserCPP\ParserDescriptor.lp"
  File /oname=Java.lp "..\Parsers\LWParserJava\ParserDescriptor.lp"
  File /oname=PascalABCNET.lp "..\Parsers\LWParserPascal\ParserDescriptor.lp"
  File /oname=XML.lp "..\Parsers\LWParserXML\ParserDescriptor.lp"
  File /oname=Yacc.lp "..\Parsers\LWParserYacc\ParserDescriptor.lp"  
  File /oname=LightParse.lp "..\Parsers\LWParserLP\ParserDescriptor.lp"  

  CreateDirectory "$INSTDIR\en"
  SetOutPath "$INSTDIR\en"
  File "..\System\bin\en\AspectCore.resources.dll"
  
  SetOutPath $INSTDIR
  File "..\System\bin\AspectCore.dll"
  File "..\System\bin\DLLWrapper.exe"
  File "..\System\bin\ParserGenerator.exe"
  File "..\System\bin\Gppg.exe"
  File "..\GPPG\GPPGcopyright.rtf"
  File /oname=ParserGenDoc.docx "..\Docs\LightParse.docx"
  
  WriteUninstaller "uninstall.exe"
    
SectionEnd

; Uninstaller
Section "Uninstall"
  
  ; Remove registry keys
  SetRegView 32
  DeleteRegKey HKLM "Software\AspectCore"
  SetRegView 64 
  DeleteRegKey HKLM "Software\AspectCore"
  DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\AspectCore"

  ; Remove files and uninstaller
  Delete $INSTDIR\AspectCore.dll
  Delete $INSTDIR\en\AspectCore.resources.dll
  Delete $INSTDIR\DLLWrapper.exe
  Delete $INSTDIR\ParserGenerator.exe
  Delete $INSTDIR\Gppg.exe
  Delete $INSTDIR\Parsers\LWParserCS2.dll
  Delete $INSTDIR\Parsers\LWParserCPP.dll
  Delete $INSTDIR\Parsers\LWParserYacc.dll
  Delete $INSTDIR\Parsers\LWParserPascal.dll
  Delete $INSTDIR\Parsers\LWParserXML.dll
  Delete $INSTDIR\Parsers\LWParserJava.dll
  Delete $INSTDIR\Parsers\LWParserLP.dll
  Delete $INSTDIR\ParserSources\CSharp.lp
  Delete $INSTDIR\ParserSources\CPP.lp
  Delete $INSTDIR\ParserSources\Java.lp
  Delete $INSTDIR\ParserSources\PascalABCNET.lp
  Delete $INSTDIR\ParserSources\XML.lp
  Delete $INSTDIR\ParserSources\Yacc.lp
  Delete $INSTDIR\ParserSources\LightParse.lp
  Delete $INSTDIR\GPPGcopyright.rtf
  Delete $INSTDIR\ParserGenDoc.docx
  Delete $INSTDIR\uninstall.exe

  RMDir "$INSTDIR\Parsers"
  RMDir "$INSTDIR\ParserSources"
  RMDir "$INSTDIR"

SectionEnd