// --------------------------------------------------------------------------------------
// FAKE build script
// --------------------------------------------------------------------------------------

#r "./packages/FAKE/tools/FakeLib.dll"

//open Fake
open Fake.Core
open Fake.IO
open Fake.Core.Globbing.Operators
open Fake.DotNet.Cli
open Fake.Core.TargetOperators

// --------------------------------------------------------------------------------------
// Build variables
// --------------------------------------------------------------------------------------

let buildDir  = "./build/"
let appReferences = !! "/**/*.fsproj"

// --------------------------------------------------------------------------------------
// Targets
// --------------------------------------------------------------------------------------

Target.Create "Clean" (fun _ ->
    buildDir |> Shell.CleanDir
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
    appReferences
    |> Seq.iter (fun p ->
        DotnetRestore (fun _ ->
            DotnetRestoreOptions.Default
        ) p
    )
)

Target.Create "Build" (fun _ ->
    appReferences
    |> Seq.iter (fun p ->
        DotnetCompile (fun opts ->
            {opts with OutputPath = Some buildDir}
        ) p
    )
)

// --------------------------------------------------------------------------------------
// Build order
// --------------------------------------------------------------------------------------

"Clean"
  ==> "InstallDotNetCLI"
  ==> "Restore"
  ==> "Build"

Target.RunOrDefault "Build"
