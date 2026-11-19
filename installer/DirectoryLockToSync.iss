; Inno Setup script for Directory Lock To Sync (the .NET Framework 4.8 build).
; Build it with installer\build-installer.ps1, which publishes the app first and passes the two defines below.
; Do not change AppId: Windows uses it to recognise an earlier install and upgrade it in place.

#ifndef AppVersion
  #define AppVersion "4.0.0"
#endif
#ifndef PublishDir
  #define PublishDir "..\artifacts\publish"
#endif

#define AppName "Directory Lock To Sync"
#define AppExe "DirectoryLockToSync.exe"

[Setup]
AppId={{4FBF5B2E-31A9-4277-A9BB-67766692BF50}
AppName={#AppName}
AppVersion={#AppVersion}
AppVerName={#AppName} {#AppVersion}
AppPublisher=Hassan Faghihi
AppPublisherURL=https://github.com/deadmann/lock2sync
AppSupportURL=https://github.com/deadmann/lock2sync/issues
AppUpdatesURL=https://github.com/deadmann/lock2sync/releases
AppCopyright=Copyright (C) 2007-2026 Hassan Faghihi
VersionInfoVersion={#AppVersion}.0
DefaultDirName={autopf}\{#AppName}
DefaultGroupName={#AppName}
DisableProgramGroupPage=yes
UninstallDisplayIcon={app}\{#AppExe}
SetupIconFile=..\src\DirectoryLockToSync\Icon1.ico
OutputDir=..\artifacts\installer
OutputBaseFilename=DirectoryLockToSync-Setup-{#AppVersion}
Compression=lzma2
SolidCompression=yes
WizardStyle=modern
; Installs for the current user without asking for administrator rights; the wizard offers "all users" as a choice.
PrivilegesRequired=lowest
PrivilegesRequiredOverridesAllowed=dialog
ArchitecturesInstallIn64BitMode=x64compatible

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "{#PublishDir}\*"; DestDir: "{app}"; Excludes: "*.pdb"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "..\LICENSE"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{autoprograms}\{#AppName}"; Filename: "{app}\{#AppExe}"
Name: "{autodesktop}\{#AppName}"; Filename: "{app}\{#AppExe}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#AppExe}"; Description: "{cm:LaunchProgram,{#AppName}}"; Flags: nowait postinstall skipifsilent

[Code]
// The program needs .NET Framework 4.8, which is already part of Windows 10 (1903 and later) and Windows 11.
// Older Windows 10 builds need it installed first, so say so instead of failing later with a cryptic error.
function IsDotNet48Installed: Boolean;
var
  Release: Cardinal;
begin
  Result := RegQueryDWordValue(HKLM, 'SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full', 'Release', Release)
            and (Release >= 528040);
end;

// Locking a folder only renames it, so removing the program does NOT unlock anything. Say so before it is too late.
function InitializeUninstall: Boolean;
begin
  Result := True;
  if not UninstallSilent then
    Result := MsgBox('Folders you locked with {#AppName} stay locked after the program is removed.' + #13#10 +
                     'Open the program and unlock them first, if you still need them.' + #13#10 + #13#10 +
                     'Do you want to continue removing the program?',
                     mbConfirmation, MB_YESNO) = IDYES;
end;

function InitializeSetup: Boolean;
begin
  Result := True;
  if not IsDotNet48Installed then
  begin
    MsgBox('{#AppName} needs Microsoft .NET Framework 4.8 or newer.' + #13#10 + #13#10 +
           'It is built into Windows 11 and recent Windows 10 versions. Please run Windows Update, or install it from' + #13#10 +
           'https://dotnet.microsoft.com/download/dotnet-framework/net48 and then run this setup again.',
           mbError, MB_OK);
    Result := False;
  end;
end;
