; The name of the installer
Name "AspectCoreInstall"

; The file to write
OutFile "_AspectCoreOnlyInstall.exe"

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
  SetOutPath $INSTDIR
  File "..\System\bin\AspectCore.dll"    
SectionEnd