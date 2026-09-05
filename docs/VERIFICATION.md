# Verification

Run the narrowest relevant check first:

```powershell
dotnet build StoryForge.slnx
dotnet test StoryForge.Tests/StoryForge.Tests.csproj
dotnet run --project StoryForge.Desktop/StoryForge.Desktop.csproj
```

For UI changes, at least build `StoryForge.Presentation` and the active desktop host. For Application/Core changes, add or update focused xUnit tests when the test project covers the area.

The solution includes reference and legacy-adjacent projects. Report separately when a full-solution failure is outside the changed scope. Do not expose API keys or other secrets from appsettings, simulator configuration, logs, or test output.
