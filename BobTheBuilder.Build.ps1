
<#
.Synopsis
	Builds the project with automatic bootstrapping of dependencies.

.Example
	PS> ./Project.build.ps1 Build

	This command invokes the task Build defined in this script.
	The required packages are downloaded on the first call.
	Then Build is invoked by local Invoke-Build.

.Example
	PS> Invoke-Build Build

	It also invokes the task Build defined in this script. But:
	- It is invoked by global Invoke-Build.
	- It does not check or install packages.
#>

param(
    [Parameter(Position = 0)]
    $Tasks,
    [Parameter(Position = 1, Mandatory = $false)]
    [string]$Output = "",
    [Parameter(Position = 2, Mandatory = $false)]
    [string]$Version = ""
)

# Set dotnet CLI environment variables to improve build times
$env:DOTNET_CLI_TELEMETRY_OPTOUT = $true
$env:DOTNET_SKIP_FIRST_TIME_EXPERIENCE = $true

function Test-Command([string]$command) {
    try {
        Get-Command -CommandType Application $command -ErrorAction Stop | Out-Null
        return $true
    } catch {
        return $false
    }
}

function Test-DotnetSdks([string]$version) {
    Write-Host "Testing for required .NET SDK $requiredSdk"
    if ($version -eq $null) { return $true }
    $sdks = & dotnet --list-sdks
    return ($sdks |? { $_.StartsWith($version) }) -ne $null
}

$requiredSdk = (Get-Content "global.json" -ErrorAction SilentlyContinue | ConvertFrom-Json).sdk.version
if (!(Test-Command dotnet) -or !(Test-DotnetSdks $requiredSdk)) {
    if ($requiredSdk -ne $null) {
        $installArgs = @{"-Version" = $requiredSdk }
    }

    try {
        Invoke-WebRequest -UseBasicParsing 'https://dot.net/v1/dotnet-install.ps1' -OutFile "dotnet-install.ps1"
        & ./dotnet-install.ps1 @installArgs
    } finally {
        remove-item ./dotnet-install.ps1
    }
}

# Direct call: ensure packages and call the dotnet tool

if ([System.IO.Path]::GetFileName($MyInvocation.ScriptName) -ne 'Invoke-Build.ps1') {
    $ErrorActionPreference = 'Stop'
    & dotnet tool restore

    # call Invoke-Build
    & dotnet ib --pwsh $Tasks @PSBoundParameters
    return
}

foreach ($file in Get-ChildItem build/*.tasks.ps1) {
    . $file
}

task . Rebuild