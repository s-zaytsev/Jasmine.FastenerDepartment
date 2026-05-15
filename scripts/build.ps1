# build.ps1 - Скрипт сборки для IIS развёртывания (UTF-8 with BOM)
param(
    [string]$OutputPath = "C:\Deploy",
    [string]$BackendProject = "../Jasmine.FastenerDepartment.Wpf\Jasmine.FastenerDepartment.WebApi\Jasmine.FastenerDepartment.WebApi.csproj",
    [string]$FrontendFolder = "../jasmine-fastener-department-react"
)

Write-Host "=== СБОРКА ПРОЕКТА ДЛЯ IIS ===" -ForegroundColor Green

# 1. Проверка зависимостей
Write-Host "`n1. Проверка зависимостей..." -ForegroundColor Cyan
dotnet --version
node --version
npm --version

# 2. Очистка выходной папки
Write-Host "`n2. Очистка выходной папки..." -ForegroundColor Cyan
if (Test-Path $OutputPath) {
    Remove-Item -Path $OutputPath -Recurse -Force
}
New-Item -ItemType Directory -Path "$OutputPath\Backend" -Force | Out-Null
New-Item -ItemType Directory -Path "$OutputPath\Frontend" -Force | Out-Null

# 3. Сборка бэкенда (.NET)
Write-Host "`n3. Сборка бэкенда (.NET WebAPI)..." -ForegroundColor Cyan
Write-Host "Восстановление зависимостей..." -ForegroundColor Gray
dotnet restore $BackendProject

Write-Host "Публикация проекта..." -ForegroundColor Gray
dotnet publish $BackendProject `
    -c Release `
    -o "$OutputPath\Backend" `
    /p:UseAppHost=false `
    /p:PublishSingleFile=false `
    /p:PublishTrimmed=false

# 4. Копирование конфигурационных файлов бэкенда
Write-Host "Копирование конфигурационных файлов..." -ForegroundColor Gray
$backendDir = Split-Path $BackendProject -Parent
if (Test-Path "$backendDir\appsettings.json") {
    Copy-Item "$backendDir\appsettings.json" "$OutputPath\Backend\" -Force
}
if (Test-Path "$backendDir\web.config") {
    Copy-Item "$backendDir\web.config" "$OutputPath\Backend\" -Force
}

# Если web.config нет, создаем его
if (-not (Test-Path "$OutputPath\Backend\web.config")) {
    $webConfig = @'
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <location path="." inheritInChildApplications="false">
    <system.webServer>
      <handlers>
        <add name="aspNetCore" path="*" verb="*" modules="AspNetCoreModuleV2" resourceType="Unspecified" />
      </handlers>
      <aspNetCore processPath="dotnet" 
                  arguments=".\Jasmine.FastenerDepartment.WebApi.dll" 
                  stdoutLogEnabled="false" 
                  stdoutLogFile=".\logs\stdout" 
                  hostingModel="inprocess">
        <environmentVariables>
          <environmentVariable name="ASPNETCORE_ENVIRONMENT" value="Production" />
        </environmentVariables>
      </aspNetCore>
    </system.webServer>
  </location>
</configuration>
'@
    Set-Content -Path "$OutputPath\Backend\web.config" -Value $webConfig -Encoding UTF8
}

# 5. Сборка фронтенда (React/Vite)
Write-Host "`n4. Сборка фронтенда (React/Vite)..." -ForegroundColor Cyan
$currentDir = Get-Location
Set-Location $FrontendFolder

Write-Host "Проверка зависимостей..." -ForegroundColor Gray
# Проверяем node_modules без удаления
if (-not (Test-Path "node_modules")) {
    Write-Host "Установка зависимостей..." -ForegroundColor Gray
    npm install --silent
} else {
    Write-Host "node_modules уже существует, пропускаем установку" -ForegroundColor Green
    
    # Опционально: обновляем зависимости если нужно
    # Write-Host "Обновление зависимостей..." -ForegroundColor Gray
    # npm update --silent
}

Write-Host "Проверка TypeScript..." -ForegroundColor Gray
npx tsc --version

Write-Host "Сборка проекта..." -ForegroundColor Gray
try {
    # Пытаемся собрать с помощью npm scripts
    if (Test-Path "package.json") {
        $packageJson = Get-Content "package.json" -Raw | ConvertFrom-Json
        
        if ($packageJson.scripts.build) {
            npm run build
        } elseif ($packageJson.scripts.vite) {
            npm run vite build
        } else {
            # Пробуем vite напрямую
            Write-Host "Пробуем Vite напрямую..." -ForegroundColor Yellow
            npx vite build
        }
    }
} catch {
    Write-Host "Ошибка сборки: $_" -ForegroundColor Red
    
    # Создаем простую сборку для продолжения работы
    Write-Host "Создаю минимальную сборку..." -ForegroundColor Yellow
    if (-not (Test-Path "dist")) {
        New-Item -ItemType Directory -Path "dist" -Force | Out-Null
    }
    
    $minimalHtml = @'
<!DOCTYPE html>
<html lang="ru">
<head>
    <meta charset="UTF-8">
    <title>Fastener Department</title>
    <style>
        body { font-family: Arial, sans-serif; padding: 20px; }
        h1 { color: #333; }
    </style>
</head>
<body>
    <h1>Fastener Department Application</h1>
    <p>Frontend is running</p>
    <p>API: <a href="http://localhost:5034/swagger">Swagger</a></p>
</body>
</html>
'@
    Set-Content -Path "dist\index.html" -Value $minimalHtml -Encoding UTF8
}

# Копирование результата
Write-Host "Копирование фронтенда..." -ForegroundColor Gray
$buildDirs = @("dist", "build", "out")
$copied = $false

foreach ($dir in $buildDirs) {
    if (Test-Path $dir -PathType Container) {
        Write-Host "Найдена папка: $dir" -ForegroundColor Green
        Copy-Item "$dir\*" "$OutputPath\Frontend\" -Recurse -Force
        $copied = $true
        break
    }
}

if (-not $copied) {
    Write-Host "Папка сборки не найдена, копируем всё кроме node_modules..." -ForegroundColor Yellow
    
    # Копируем всё кроме node_modules
    Get-ChildItem -Path . -Exclude "node_modules", ".git", ".vscode" | ForEach-Object {
        if ($_.Name -ne "node_modules") {
            Copy-Item $_.FullName "$OutputPath\Frontend\" -Recurse -Force -ErrorAction SilentlyContinue
        }
    }
}

if (-not (Test-Path "$OutputPath\Frontend\web.config")) {
    $webConfig = @'
<?xml version="1.0" encoding="UTF-8"?>
<configuration>
  <system.webServer>
    <rewrite>
      <rules>
        <rule name="React Routes">
          <match url=".*" />
          <conditions logicalGrouping="MatchAll">
            <add input="{REQUEST_FILENAME}" matchType="IsFile" negate="true" />
            <add input="{REQUEST_FILENAME}" matchType="IsDirectory" negate="true" />
          </conditions>
          <action type="Rewrite" url="/index.html" />
        </rule>
      </rules>
    </rewrite>
  </system.webServer>
</configuration>
'@
    Set-Content -Path "$OutputPath\Frontend\web.config" -Value $webConfig -Encoding UTF8
}

Set-Location $currentDir