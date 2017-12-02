// --------------------------------------------------------------------------------------
// FAKE build script
// --------------------------------------------------------------------------------------

#r "./packages/FAKE/tools/FakeLib.dll"

//open Fake
open Fake.Core
open Fake.IO
open Fake.IO.FileSystemOperators
open Fake.DotNet.Cli
open Fake.Core.TargetOperators
open System

// --------------------------------------------------------------------------------------
// Build variables
// --------------------------------------------------------------------------------------

let projectName = "NumericsWSF"
let buildDir  = "build"
let fsproj = (projectName @@ (projectName + ".fsproj"))
let dnaFile = (projectName @@ (projectName + ".dna"))
// --------------------------------------------------------------------------------------
// Targets
// --------------------------------------------------------------------------------------

Target.Create "Clean" (fun _ ->
    [buildDir; (projectName @@ buildDir)]
        |> List.iter Shell.CleanDir
)

Target.Create "InstallDotNetCLI" (fun _ ->
    DotnetCliInstall (fun opts ->
        {opts with 
            Channel = Some "2.0"
            InstallerOptions = (fun opts -> {opts with Branch="master"})
            }
        )
)

Target.Create "Restore" (fun _ ->
    DotnetRestore id fsproj
)

Target.Create "Build" (fun _ ->
        DotnetCompile (fun opts ->
            {opts with OutputPath = Some buildDir}
        ) fsproj
)

Target.Create "Pack" (fun _ ->
    let projBuildDir = (projectName @@ buildDir)
    Shell.CopyFile projBuildDir dnaFile
    Shell.CopyFile projBuildDir "./packages/ExcelDna.Addin/tools/ExcelDna.xll"

    Shell.pushd projBuildDir
    Shell.Rename "NumericsWSF.xll" "ExcelDna.xll"
    let res = Process.ExecProcess (fun o ->
        {o with FileName = "../../packages/ExcelDna.Addin/tools/ExcelDnaPack.exe"
                Arguments = "NumericsWSF.dna"}) (TimeSpan.FromSeconds 5.0)

    if res <> 0 then failwithf "ExcelDnaPack returned with a non-zero exit code"

    Shell.popd ()
)

// --------------------------------------------------------------------------------------
// Build order
// --------------------------------------------------------------------------------------

"Clean"
  ==> "InstallDotNetCLI"
  ==> "Restore"
  ==> "Build"
  ==> "Pack"

Target.RunOrDefault "Pack"
