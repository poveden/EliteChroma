@echo off

pushd %~dp0..

set ROOT=%CD%

pushd test\EliteFiles.Tests
dotnet test -c Release -l "trx;LogFileName=TestResults.xml" /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput=coverage\cobertura.xml
dotnet reportgenerator --reports:coverage\cobertura.xml --targetdir:coverage\report
popd

pushd test\EliteChroma.Core.Tests
dotnet test -c Release -l "trx;LogFileName=TestResults.xml" /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput=coverage\cobertura.xml /p:ExcludeByFile=%ROOT%/src/EliteFiles/**
dotnet reportgenerator --reports:coverage\cobertura.xml --targetdir:coverage\report
popd

popd
