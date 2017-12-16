#tool nuget:?package=NUnit.ConsoleRunner&version=3.4.0
//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define directories.
var buildDir = Directory("./src/Example/bin") + Directory(configuration);

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectory(buildDir);
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    NuGetRestore("./PagiNET.sln");
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
  // Use MSBuild
  MSBuild("./PagiNET.sln", settings =>
    settings.SetConfiguration(configuration));    
});

Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    RunUnitTests();
});

Task("Run-Unit-Tests-Solo")
    .Does(() =>
{
    RunUnitTests();
});

void RunUnitTests() {
    NUnit3("./tests/PagiNET.UnitTests/bin/" + configuration + "/netstandard2.0/PagiNET.UnitTests.dll", new NUnit3Settings {
            NoResults = true
        });
}

// Tested localy only for now
Task("Run-Integration-Tests")
    //.IsDependentOn("Build")
    .Does(() =>
{
    NUnit3("./tests/PagiNET.IntegrationTests.EFCore/bin/" + configuration + "/netstandard2.0/PagiNET.IntegrationTests.EFCore.dll", new NUnit3Settings {
            NoResults = true
        });
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Run-Unit-Tests");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);