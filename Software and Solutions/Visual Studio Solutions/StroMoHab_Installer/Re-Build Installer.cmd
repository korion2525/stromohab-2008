@echo off
echo Building...
"%VS90COMNTOOLS%..\IDE\DEVENV.exe" /rebuild release StroMoHab_Installer.sln
echo Done
if not "%ERRORLEVEL%" == "0" echo FAILED!
pause