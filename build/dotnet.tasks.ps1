# Test for dotnet.exe and fail the build if it's not available
Get-Command dotnet -CommandType Application -ErrorAction Stop | Out-Null

# Set dotnet CLI environment variables to improve build times
$env:DOTNET_CLI_TELEMETRY_OPTOUT = $true
$env:DOTNET_SKIP_FIRST_TIME_EXPERIENCE = $true

$Configuration = (property Configuration "Release")

function Get-Projects() {
    return (Get-ChildItem . -Recurse -Include "*.csproj" -Exclude "nCrunchTemp*")
}

function Get-Artifacts() {
    return (Get-Projects |% { Write-Output (Join-Path $_.Directory "bin/$Configuration/net6.0/$($_.BaseName).dll") })
}

function Get-TargetFrameworks([IO.FileInfo]$project) {
    [xml]$projectDom = (Get-Content $project)
    write-host $projectDom
    $targetFrameworks = $projectDom.Project.PropertyGroup.TargetFrameworks
    if ($targetFrameworks -eq $null) {
        $targetFrameworks = $projectDom.Project.PropertyGroup.TargetFramework
    }

    return $targetFrameworks
}

function Test-IsPackable([IO.FileInfo]$project) {
    [xml]$projectFile = (Get-Content $project)
    return $projectFile.Project.PropertyGroup.IsPackable -ne $false
}

# Synopsis: Compile C# source to .NET Core binaries.
task Build @{
    Inputs  = { Get-Projects }
    Outputs = { Get-Artifacts }
    Jobs    = 'Init', {
        exec { dotnet build -c $Configuration }
    }
}

# Synopsis: Bootstrap the build
task Init @{
    Inputs  = { Get-Projects }
    Outputs = { Get-Projects |% { Write-Output (Join-Path $_.Directory "obj/project.assets.json") } }
    Jobs    = {
        exec { dotnet restore -v:m }
    }
}

task Test @{
    Jobs    = 'Build', {
        $projects = Get-Projects |? { $_.Name -match '[Tt]est' }
        $projects | ForEach-Object {
            (Get-TargetFrameworks $_).Split(';') |% {
                exec { dotnet test --no-build -f $_ -c Release }
            }
        }
    }
}

# Synopsis: Build the NuGet packages for release
task Pack @{
    Inputs  = { Get-Artifacts }
    Outputs = { Get-Projects |? { Test-IsPackable $_ } |% { 
        Write-Output "$(property Output `"pkg`")/$($_.BaseName).$(property Version `"0.0.0`").nupkg"
        Write-Output "$(property Output `"pkg`")/$($_.BaseName).$(property Version `"0.0.0`").snupkg"
    }}
    Jobs    = 'Build', {
        exec { 
            dotnet pack --no-build `
                --include-symbols `
                --include-source `
                -c $Configuration `
                -o (property Output 'pkg') `
                /p:Version=$Version `
                /p:SymbolPackageFormat=snupkg
        }
    }
}

task Publish {
    requires -Environment NUGET_API_KEY

    $packages = Get-ChildItem (property Output 'pkg') -Recurse -Filter *nupkg
    $packages |% {
        exec { dotnet nuget push $_ -s https://api.nuget.org/v3/index.json --skip-duplicate -k $env:NUGET_API_KEY }
    }
}

# Synopsis: Remove temporary stuff.
task Clean {
    exec { dotnet clean -v:m }
    Get-ChildItem -Recurse -Include "obj" | Remove-Item -Recurse -ErrorAction "SilentlyContinue"
    Remove-Item -Recurse (property Output "pkg") -ErrorAction "SilentlyContinue"
}

task Rebuild Clean, Build