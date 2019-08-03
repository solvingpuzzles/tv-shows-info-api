var target = Argument("target", "Default");

var projPath = "./src/Host/Host.csproj";

Setup(ctx => {
    CleanDirectory("./app");
});

Task("clean")
    .Does(() =>
    {
        var settings = new DotNetCoreCleanSettings
        {
            Configuration = "Release",
            OutputDirectory = "./app/"
        };
        DotNetCoreClean(projPath, settings);
    });

Task("restore")
    .IsDependentOn("clean")
    .Does(() =>
    {
        DotNetCoreRestore(projPath);
    });

Task("build")
    .IsDependentOn("restore")
    .Does(() =>
    {
        var settings = new DotNetCoreBuildSettings
        {
            NoRestore = true,
            Configuration = "Release",
            OutputDirectory = "./app/"
        };
        DotNetCoreBuild(projPath, settings);
    });

Task("Default").IsDependentOn("build");

RunTarget(target);
