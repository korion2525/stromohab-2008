@echo off
echo Setting up Web Site Paths...
if "%COMPUTERNAME%"=="APPC05" set WEBSERVERDIR=E:\DocumentationServer
if "%COMPUTERNAME%"=="COMPAQ-NX7400" set WEBSERVERDIR=\\144.32.137.68\DocumentationServer
echo Web Site Path set to %WEBSERVERDIR%
echo.
echo Building Code
echo.
"%VS90COMNTOOLS%..\IDE\DEVENV.exe" /rebuild release "..\Visual Studio Solutions\Stromohab_Deployment\Stromohab_Deployment.sln"
echo Code Build Done
echo.
echo Building Documentation
%SystemRoot%\Microsoft.NET\Framework\v3.5\MSBuild.exe /p:Configuration=Release /p:DumpLogOnFailure=True StroMoHab.shfbproj

if not "%errorlevel%"=="0" goto _FAIL
echo Documentation Build Done
if exist %WEBSERVERDIR% goto _PUBLISH 
goto _DONE

:_PUBLISH
echo.
echo Publishing Documentation
if not exist %SystemRoot%\System32\robocopy.exe goto _XCOPY

:_ROBOCOPY
robocopy Help %WEBSERVERDIR%\Help /MIR


goto _DONE

:_XCOPY
if exist %WEBSERVERDIR%\Help rmdir %WEBSERVERDIR%\Help /s /q
mkdir %WEBSERVERDIR%\Help
xcopy Help %WEBSERVERDIR%\Help /E /I
goto _DONE

:_FAIL
echo Documentation Failed
rem Do something on fail here
pause

:_DONE