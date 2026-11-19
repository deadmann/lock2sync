<#
.SYNOPSIS
  Builds the Setup.exe installer: publishes the .NET Framework 4.8 build, then compiles installer\DirectoryLockToSync.iss.

.DESCRIPTION
  Needs the .NET SDK and Inno Setup 6 (winget install JRSoftware.InnoSetup).
  Result: artifacts\installer\DirectoryLockToSync-Setup-<version>.exe  (the version comes from the project file).
#>
$ErrorActionPreference = "Stop"
$root = Split-Path $PSScriptRoot -Parent
$project = Join-Path $root "src\DirectoryLockToSync\DirectoryLockToSync.csproj"
$publishDir = Join-Path $root "artifacts\publish"

# Find the Inno Setup compiler.
$candidates = @(
    "$env:ProgramFiles\Inno Setup 6\ISCC.exe",
    "${env:ProgramFiles(x86)}\Inno Setup 6\ISCC.exe",
    "$env:LOCALAPPDATA\Programs\Inno Setup 6\ISCC.exe"
)
$iscc = $candidates | Where-Object { Test-Path $_ } | Select-Object -First 1
if (-not $iscc) {
    $onPath = Get-Command ISCC.exe -ErrorAction SilentlyContinue
    if ($onPath) { $iscc = $onPath.Source }
}
if (-not $iscc) { throw "Inno Setup 6 was not found. Install it with:  winget install JRSoftware.InnoSetup" }

# The version number lives in one place: the project file.
$version = (dotnet msbuild $project -getProperty:Version -p:TargetFramework=net48).Trim()
if (-not $version) { throw "Could not read the version from $project" }
Write-Host "Building Directory Lock To Sync $version"

if (Test-Path $publishDir) { Remove-Item $publishDir -Recurse -Force }
dotnet publish $project -c Release -f net48 -o $publishDir
if ($LASTEXITCODE -ne 0) { throw "dotnet publish failed" }

& $iscc "/DAppVersion=$version" "/DPublishDir=$publishDir" (Join-Path $root "installer\DirectoryLockToSync.iss")
if ($LASTEXITCODE -ne 0) { throw "Inno Setup failed" }

$setup = Join-Path $root "artifacts\installer\DirectoryLockToSync-Setup-$version.exe"
Write-Host ""
Write-Host "Done: $setup  ($([math]::Round((Get-Item $setup).Length / 1MB, 1)) MB)"
