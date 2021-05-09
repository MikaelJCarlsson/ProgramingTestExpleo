@ECHO OFF

dotnet build
dotnet test
cd .\ExpleoTestApp\
dotnet run .\ExpleoTestApp.csproj
Pause