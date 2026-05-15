@echo off
setlocal

set "SOURCE_DIR=../"
set "TEMP_DIR=%TEMP%\%~n0_temp"
set "ZIP_FILE=%~dp0archive.zip"

echo Cleaning temp folder...
if exist "%TEMP_DIR%" rmdir /s /q "%TEMP_DIR%" 2>nul
mkdir "%TEMP_DIR%" 2>nul

echo Coping files with robocopy...
echo.

REM Используем robocopy для копирования с исключением папок
robocopy "%SOURCE_DIR%" "%TEMP_DIR%" *.* /E /XD node_modules obj bin .idea .vs .gradle build /NP /NFL /NDL

echo.
echo Creating ZIP-archive...
if exist "%ZIP_FILE%" del "%ZIP_FILE%" 2>nul

REM Создаем архив с помощью PowerShell - упрощенная версия
powershell -command "Add-Type -Assembly 'System.IO.Compression.FileSystem'; [System.IO.Compression.ZipFile]::CreateFromDirectory('%TEMP_DIR%', '%ZIP_FILE%')"

if !errorlevel! equ 0 (
    echo The archive has been created successfully
) else (
    echo Error during creating the archive
)

echo.
echo Cleaning temp files...
rmdir /s /q "%TEMP_DIR%" 2>nul

echo.
if exist "%ZIP_FILE%" (
    echo Done! The archive has been created: %ZIP_FILE%
    for %%F in ("%ZIP_FILE%") do echo The archive size: %%~zF bytes
) else (
    echo Error: the archive hasn't been created
)

pause