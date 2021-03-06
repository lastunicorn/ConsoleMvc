@echo off

set root_directory=..
set version=0.1.0

rem ----------------------------------------------------------------------------------------------------
rem Clean up existing files.
rem ----------------------------------------------------------------------------------------------------

echo.
echo ---
echo --- Clenup existing files - ConsoleMvc directoty
echo ---
echo.
if EXIST "ConsoleMvc" (
	rmdir /S/Q "ConsoleMvc"
	if %errorlevel% neq 0 goto :error
)

echo.
echo ---
echo --- Clean up existing files - package file (7z file).
echo ---
echo.
del ConsoleMvc*.7z
if %errorlevel% neq 0 goto :error

rem ----------------------------------------------------------------------------------------------------
rem Retrieve assemblies.
rem ----------------------------------------------------------------------------------------------------

echo.
echo ---
echo --- Retrieve assemblies
echo ---
echo.
xcopy /Y/S/I "%root_directory%\sources\ConsoleMvc\bin\Release-Net461\*.dll" "ConsoleMvc\lib\Net461\"
if %errorlevel% neq 0 goto :error
xcopy /Y/S/I "%root_directory%\sources\ConsoleMvc\bin\Release-Net461\*.xml" "ConsoleMvc\lib\Net461\"
if %errorlevel% neq 0 goto :error
xcopy /Y/S/I "%root_directory%\sources\ConsoleMvc\bin\Release-Net461\ro\*" "ConsoleMvc\lib\Net461\ro\"
if %errorlevel% neq 0 goto :error
xcopy /Y/S/I "%root_directory%\sources\ConsoleMvc\bin\Release-Net461\fr\*" "ConsoleMvc\lib\Net461\fr\"
if %errorlevel% neq 0 goto :error

rem echo.
rem echo ---
rem echo --- Retrieve assemblies
rem echo ---
rem echo.
rem xcopy /Y/S/I "%root_directory%\sources\ConsoleMvc\bin\Release-Net45\*" "ConsoleMvc\lib\Net45"
rem if %errorlevel% neq 0 goto :error

rem ----------------------------------------------------------------------------------------------------
rem Retrieve license file.
rem ----------------------------------------------------------------------------------------------------

echo.
echo ---
echo --- Retrieve license file
echo ---
echo.
xcopy /Y "%root_directory%\LICENSE" "ConsoleMvc\*"
if %errorlevel% neq 0 goto :error

rem ----------------------------------------------------------------------------------------------------
rem Retrieve readme file.
rem ----------------------------------------------------------------------------------------------------

echo.
echo ---
echo --- Retrieve readme file
echo ---
echo.
xcopy /Y "%root_directory%\readme.txt" "ConsoleMvc\*"
if %errorlevel% neq 0 goto :error

rem ----------------------------------------------------------------------------------------------------
rem Retrieve changelog file.
rem ----------------------------------------------------------------------------------------------------

echo.
echo ---
echo --- Retrieve changelog file
echo ---
echo.
xcopy /Y "%root_directory%\doc\changelog.txt" "ConsoleMvc\*"
if %errorlevel% neq 0 goto :error

rem ----------------------------------------------------------------------------------------------------
rem Zip
rem ----------------------------------------------------------------------------------------------------

rem 7-Zip returns the following exit codes:
rem 
rem Code Meaning 
rem 0 No error 
rem 1 Warning (Non fatal error(s)). For example, one or more files were locked by some other application, so they were not compressed. 
rem 2 Fatal error 
rem 7 Command line error 
rem 8 Not enough memory for operation 
rem 255 User stopped the process 

echo.
echo ---
echo --- Create the zip package
echo ---
echo.
7z a "ConsoleMvc-v%version%.7z" "ConsoleMvc"
if %errorlevel% neq 0 goto :error

echo.
echo ---
echo --- Test the zip package
echo ---
echo.
7z t "ConsoleMvc-v%version%.7z"
if %errorlevel% neq 0 goto :error

rem ----------------------------------------------------------------------------------------------------
rem Clean up
rem ----------------------------------------------------------------------------------------------------

echo.
echo ---
echo --- Delete the uncompressed files
echo ---
echo.
rmdir /S/Q "ConsoleMvc"
if %errorlevel% neq 0 goto :error

rem ----------------------------------------------------------------------------------------------------
rem Success
rem ----------------------------------------------------------------------------------------------------

:success
echo.
echo.
echo ---
echo ---
echo --- Success
echo ---
echo ---
goto :end

rem ----------------------------------------------------------------------------------------------------
rem Error
rem ----------------------------------------------------------------------------------------------------

:error
echo.
echo.
echo ---
echo ---
echo --- Error
echo ---
echo ---

rem ----------------------------------------------------------------------------------------------------
rem End
rem ----------------------------------------------------------------------------------------------------

:end