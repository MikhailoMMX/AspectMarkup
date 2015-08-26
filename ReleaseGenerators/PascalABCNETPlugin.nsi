; The name of the installer
Name "Aspect Plug-in for PascalABC.NET"

; The file to write
OutFile "..\Release\AspectPABCPlugin.exe"

; The default installation directory
InstallDir "$PROGRAMFILES\PascalABC.NET"

;Get installation folder from registry if available
InstallDirRegKey HKCU ${REGDIR} ""

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
  File "..\System\bin\PascalABCAspects.dll"
  WriteUninstaller "uninstallAspectPlugIn.exe"
SectionEnd

; Uninstaller
Section "Uninstall"
  
  ; Remove files and uninstaller
  Delete $INSTDIR\AspectCore.dll
  Delete $INSTDIR\PascalABCAspects.dll
  Delete $INSTDIR\uninstallAspectPlugIn.exe

SectionEnd