@echo off

pushd %~dp0..

dotnet test -c Release -s Common.runsettings
dotnet reportgenerator --reports:**\coverage\cobertura.xml --targetdir:coverage\report

popd
