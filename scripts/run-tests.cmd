@echo off

pushd %~dp0..

dotnet build -c Release --no-incremental /warnaserror
if %errorlevel% neq 0 exit /b %errorlevel%

dotnet test -c Release --no-build -s Common.runsettings
if %errorlevel% neq 0 exit /b %errorlevel%

dotnet tool restore
dotnet reportgenerator -reports:**\coverage\cobertura.xml -targetdir:coverage\report

popd
