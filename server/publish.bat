dotnet publish server.csproj -c Release -r win-x64 --self-contained true ^
  /p:PublishSingleFile=true ^
  /p:IncludeNativeLibrariesForSelfExtract=true ^
  /p:PublishTrimmed=false ^
  /p:PublishDir=E:\Alaska\bin\
echo =========== DONE ===========
pause