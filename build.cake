var target = Argument("target", "Default");
var configuration = Argument("configuraion", "Release");

var solution = "./ORA.sln";
var cliCsProject = "./ORA.Application.CLI.CoreWrapper/ORA.Application.CLI.CoreWrapper.csproj";
var guiCsProject = "./ORA.Application.GUI.CoreWrapper/ORA.Application.GUI.CoreWrapper.csproj";
var buildDir = Directory("./build");

var buildSettings = new DotNetCoreBuildSettings()
{
  Framework = "netcoreapp3.1",
  Configuration = "Release",
  OutputDirectory = buildDir
};

Task("Default")
  .IsDependentOn("Build");

Task("Build")
  .IsDependentOn("CLI")
  .IsDependentOn("GUI");

Task("CLI")
  .Does(() => {
    if (!DirectoryExists(buildDir)) {
      CreateDirectory(buildDir);
    }

    DotNetCoreBuild(cliCsProject, buildSettings);

    var linuxBinary = "./build/ORA.Application.CLI.CoreWrapper";
    if (FileExists(linuxBinary)) {
        CopyFile(linuxBinary, "./build/ora-cli");
    } else {
        CopyFile(linuxBinary + ".exe", "./build/ora-cli.exe");
    }
  });

Task("GUI")
  .Does(() => {
    if (!DirectoryExists(buildDir)) {
      CreateDirectory(buildDir);
    }

    DotNetCoreBuild(guiCsProject, buildSettings);

    var linuxBinary = "./build/ORA.Application.GUI.CoreWrapper";
    if (FileExists(linuxBinary)) {
        CopyFile(linuxBinary, "./build/ora-gui");
    } else {
        CopyFile(linuxBinary + ".exe", "./build/ora-gui.exe");
    }
  });

Task("Clean")
  .Does(() => {
    CleanDirectory(buildDir);
  });

RunTarget(target);

// https://github.com/cake-build/cake/blob/develop/build.cake
// https://github.com/luisgoncalves/cake-sample/blob/master/build.cake
