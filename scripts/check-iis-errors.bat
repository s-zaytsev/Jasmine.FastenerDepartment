@echo off
chcp 1251
echo Запуск проверки IIS...
echo.

REM Переходим в нужную директорию
cd /d "%~dp0"

REM Проверяем наличие PowerShell
where powershell >nul 2>nul
if %errorlevel% neq 0 (
    echo Ошибка: PowerShell не установлен
    pause
    exit /b 1
)

REM Запускаем скрипт сборки
powershell -ExecutionPolicy Bypass -File "check-iis-errors.ps1"

echo.
echo Проверка завершена!
echo.
echo Для настройки IIS запустите: setup-iis.ps1 (от Администратора)
pause