# setup-iis.ps1 - Настройка IIS (запускать от Администратора)
param(
    [string]$SitePath = "C:\Deploy"
)

# Проверка прав администратора
if (-NOT ([Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole] "Administrator")) {
    Write-Host "Запустите скрипт от имени Администратора!" -ForegroundColor Red
    pause
    exit
}

# Включение компонентов Windows
Enable-WindowsOptionalFeature -Online -FeatureName IIS-WebServerRole
Enable-WindowsOptionalFeature -Online -FeatureName IIS-WebServer
Enable-WindowsOptionalFeature -Online -FeatureName IIS-CommonHttpFeatures
Enable-WindowsOptionalFeature -Online -FeatureName IIS-HttpErrors
Enable-WindowsOptionalFeature -Online -FeatureName IIS-StaticContent
Enable-WindowsOptionalFeature -Online -FeatureName IIS-DefaultDocument
Enable-WindowsOptionalFeature -Online -FeatureName IIS-DirectoryBrowsing
Enable-WindowsOptionalFeature -Online -FeatureName IIS-HttpCompressionStatic
Enable-WindowsOptionalFeature -Online -FeatureName IIS-ASPNET45

# Создание пулов приложений
New-WebAppPool -Name "FastenerBackendPool" -Force
Set-ItemProperty "IIS:\AppPools\FastenerBackendPool" -Name managedRuntimeVersion -Value ""
Set-ItemProperty "IIS:\AppPools\FastenderBackendPool" -Name processModel.identityType -Value 2

New-WebAppPool -Name "FastenerFrontendPool" -Force

# Создание сайтов
# Бэкенд
New-Website -Name "FastenerBackend" -Port 5034 -PhysicalPath "$SitePath\Backend" -ApplicationPool "FastenerBackendPool" -Force

# Фронтенд
New-Website -Name "FastenerFrontend" -Port 3000 -PhysicalPath "$SitePath\Frontend" -ApplicationPool "FastenerFrontendPool" -Force

# Настройка прав доступа
$acl = Get-Acl "$SitePath\Backend"
$permission = "IIS_IUSRS","FullControl","ContainerInherit,ObjectInherit","None","Allow"
$accessRule = New-Object System.Security.AccessControl.FileSystemAccessRule $permission
$acl.SetAccessRule($accessRule)
Set-Acl "$SitePath\Backend" $acl

# Запуск сайтов
Start-Website "FastenerBackend"
Start-Website "FastenerFrontend"

Write-Host "`n=== IIS НАСТРОЕН ===" -ForegroundColor Green
Write-Host "Бэкенд: http://localhost:5034" -ForegroundColor Yellow
Write-Host "Фронтенд: http://localhost:3000" -ForegroundColor Yellow
Write-Host "Swagger: http://localhost:5034/swagger" -ForegroundColor Yellow

# Проверка
Invoke-WebRequest -Uri "http://localhost:5034" -UseBasicParsing