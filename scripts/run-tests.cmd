@echo off

pushd %~dp0..

dotnet test -c Release -s Common.runsettings
if %errorlevel% neq 0 exit /b %errorlevel%
dotnet tool restore
dotnet reportgenerator --reports:**\coverage\cobertura.xml --targetdir:coverage\report

popd
