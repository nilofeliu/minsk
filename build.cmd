@echo off
set LOG_FILE=output.log

echo ===== BUILD AND TEST =====
echo.

REM Function-like behavior for tee
call :tee "Building..." "%LOG_FILE%"
dotnet build 2>&1 | findstr /V "^$"
dotnet build >> "%LOG_FILE%" 2>&1

echo.
call :tee "Testing..." "%LOG_FILE%"
dotnet test .\Minsk.Tests\Minsk.Tests.csproj 2>&1 | findstr /V "^$"
dotnet test .\Minsk.Tests\Minsk.Tests.csproj >> "%LOG_FILE%" 2>&1

echo.
echo Full log saved to %LOG_FILE%
goto :eof

:tee <message> <logfile>
echo %~1
echo %~1 >> %~2
exit /b