#load "packages/Be.Vlaanderen.Basisregisters.Build.Pipeline/Content/build-generic.fsx"

open Fake
open ``Build-generic``

let assemblyVersionNumber = (sprintf "%s.0")
let nugetVersionNumber = (sprintf "%s")

let build = buildSolution assemblyVersionNumber
let test = testSolution
let publish = publishSolution assemblyVersionNumber
let pack = packSolution nugetVersionNumber

// Library ------------------------------------------------------------------------

Target "Lib_Build" (fun _ -> build "Be.Vlaanderen.Basisregisters.AspNetCore.Mvc.Middleware.AddCorrelationId")
Target "Lib_Test" (fun _ -> test "Be.Vlaanderen.Basisregisters.AspNetCore.Mvc.Middleware.AddCorrelationId")

Target "Lib_Publish" (fun _ -> publish "Be.Vlaanderen.Basisregisters.AspNetCore.Mvc.Middleware.AddCorrelationId")
Target "Lib_Pack" (fun _ -> pack "Be.Vlaanderen.Basisregisters.AspNetCore.Mvc.Middleware.AddCorrelationId")

// --------------------------------------------------------------------------------

Target "PublishLibrary" DoNothing
Target "PublishAll" DoNothing
Target "Test" DoNothing
Target "PackageMyGet" DoNothing
Target "PackageAll" DoNothing

// Publish ends up with artifacts in the build folder
"DotNetCli" ==> "Clean" ==> "Restore" ==> "Lib_Build" ==> "Lib_Test" ==> "Lib_Publish" ==> "PublishLibrary"
"PublishLibrary" ==> "PublishAll"
"Test" ==> "PublishAll"

// Package ends up with local NuGet packages
"PublishLibrary" ==> "Lib_Pack" ==> "PackageMyGet"
"PackageMyGet" ==> "PackageAll"

RunTargetOrDefault "Lib_Test"
