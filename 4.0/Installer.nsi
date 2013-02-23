;--------------------------------
;Include Modern UI

  !include "MUI.nsh"


;--------------------------------
; The name of the installer
Name "InformationBox"

; The file to write
OutFile "InformationBoxSetup-4.0.exe"

; The default installation directory
InstallDir $PROGRAMFILES\InformationBox

; Registry key to check for directory (so if you install again, it will 
; overwrite the old one automatically)
InstallDirRegKey HKLM "Software\InformationBox" "Install_Dir"

;--------------------------------
;Interface Configuration

  !define MUI_HEADERIMAGE
  !define MUI_HEADERIMAGE_BITMAP "logo.bmp" ; optional
  !define MUI_ABORTWARNING

;--------------------------------
;Pages

  !insertmacro MUI_PAGE_LICENSE "license.txt"
  !insertmacro MUI_PAGE_COMPONENTS
  !insertmacro MUI_PAGE_DIRECTORY
  !insertmacro MUI_PAGE_INSTFILES

  !insertmacro MUI_UNPAGE_CONFIRM
  !insertmacro MUI_UNPAGE_INSTFILES

;--------------------------------
;Languages

  !insertmacro MUI_LANGUAGE "English"

;--------------------------------

; The stuff to install
Section "Assemblies and Designer (required)" SecBin

  SectionIn RO
  
  ; Set output path to the installation directory.
  SetOutPath $INSTDIR\bin
  
  ; Put file there
  File "InfoBox.Designer\bin\Release\InfoBox.Designer.exe"
  File "InfoBox.Designer\bin\Release\InfoBox.dll"
  SetOutPath $INSTDIR\bin\fr
  File "InfoBox.Designer\bin\Release\fr\InfoBox.resources.dll"
  SetOutPath $INSTDIR\bin\fr
  File "InfoBox.Designer\bin\Release\fr\InfoBox.resources.dll"
  SetOutPath $INSTDIR\bin\pt
  File "InfoBox.Designer\bin\Release\pt\InfoBox.resources.dll"
  SetOutPath $INSTDIR\bin\ar
  File "InfoBox.Designer\bin\Release\ar\InfoBox.resources.dll"
  SetOutPath $INSTDIR\bin\fa
  File "InfoBox.Designer\bin\Release\fa\InfoBox.resources.dll"
  SetOutPath $INSTDIR\bin\nl
  File "InfoBox.Designer\bin\Release\nl\InfoBox.resources.dll"
  
  SetOutPath $INSTDIR
  
  ; Write the installation path into the registry
  WriteRegStr HKLM SOFTWARE\InformationBox "Install_Dir" "$INSTDIR"
  
  ; Write the uninstall keys for Windows
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\InformationBox" "DisplayName" "InformationBox"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\InformationBox" "UninstallString" '"$INSTDIR\uninstall.exe"'
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\InformationBox" "NoModify" 1
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\InformationBox" "NoRepair" 1
  WriteUninstaller "uninstall.exe"
  
SectionEnd

Section "Source Files" SecSrc

  CreateDirectory "$INSTDIR\source"
  SetOutPath $INSTDIR\source
  File "InfoBox.Sln"
  
  CreateDirectory "$INSTDIR\source\InfoBox"
  SetOutPath $INSTDIR\source\InfoBox

  File /r /x bin /x *.user /x obj /x *.suo InfoBox\*.*
  CreateDirectory "$INSTDIR\source\InfoBox.Designer"
  SetOutPath $INSTDIR\source\InfoBox.Designer

  File /r /x bin /x *.user /x obj /x *.suo InfoBox.Designer\*.*

  CreateDirectory "$SMPROGRAMS\InformationBox"
  CreateShortCut "$SMPROGRAMS\InformationBox\InformationBox Source.lnk" "$INSTDIR\source\InfoBox.sln" "" "$INSTDIR\source\InfoBox.sln" 0

SectionEnd

Section "Help File" SecHlp

  CreateDirectory "$INSTDIR\help"
  SetOutPath $INSTDIR\help
  File "Help\InfoBox.chm"

  CreateDirectory "$SMPROGRAMS\InformationBox"
  CreateShortCut "$SMPROGRAMS\InformationBox\InformationBox Help.lnk" "$INSTDIR\help\InfoBox.chm" "" "$INSTDIR\help\InfoBox.chm" 0

SectionEnd

; Optional section (can be disabled by the user)
Section "Start Menu Shortcuts" SecShc

  CreateDirectory "$SMPROGRAMS\InformationBox"
  CreateShortCut "$SMPROGRAMS\InformationBox\Uninstall.lnk" "$INSTDIR\uninstall.exe" "" "$INSTDIR\uninstall.exe" 0
  CreateShortCut "$SMPROGRAMS\InformationBox\InformationBox Designer.lnk" "$INSTDIR\bin\InfoBox.Designer.exe" "" "$INSTDIR\bin\InfoBox.Designer.exe" 0
  
SectionEnd

;--------------------------------
;Descriptions

  ;Language strings
  LangString DESC_SecBin ${LANG_ENGLISH} "Installs the required assemblies and the designer."
  LangString DESC_SecSrc ${LANG_ENGLISH} "Installs the source files (requires a C# IDE)."
  LangString DESC_SecHlp ${LANG_ENGLISH} "Installs the help file (CHM)."
  LangString DESC_SecShc ${LANG_ENGLISH} "Creates shortcuts in the start menu."

  ;Assign language strings to sections
  !insertmacro MUI_FUNCTION_DESCRIPTION_BEGIN
    !insertmacro MUI_DESCRIPTION_TEXT ${SecBin} $(DESC_SecBin)
    !insertmacro MUI_DESCRIPTION_TEXT ${SecSrc} $(DESC_SecSrc)
    !insertmacro MUI_DESCRIPTION_TEXT ${SecHlp} $(DESC_SecHlp)
    !insertmacro MUI_DESCRIPTION_TEXT ${SecShc} $(DESC_SecShc)
  !insertmacro MUI_FUNCTION_DESCRIPTION_END

;--------------------------------
;--------------------------------

; Uninstaller

Section "Uninstall"
  
  ; Remove registry keys
  DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\InformationBox"
  DeleteRegKey HKLM SOFTWARE\InformationBox

  ; Remove files and uninstaller
  RMDir /r $INSTDIR\bin
  RMDir /r $INSTDIR\help
  RMDir /r $INSTDIR\source
  
  Delete $INSTDIR\uninstall.exe

  ; Remove shortcuts, if any
  Delete "$SMPROGRAMS\InformationBox\*.*"

  ; Remove directories used
  RMDir "$SMPROGRAMS\InformationBox"
  RMDir "$INSTDIR"

SectionEnd
