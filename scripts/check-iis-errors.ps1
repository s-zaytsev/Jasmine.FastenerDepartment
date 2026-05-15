# check-iis-errors.ps1
Write-Host "=== ANALIZA OSHIBOK IIS ===" -ForegroundColor Cyan
Write-Host "Vremya: $(Get-Date)`n" -ForegroundColor Gray

# 1. Logi sobytiy Windows
Write-Host "1. Poslednie oshibki IIS iz zhurnala sobytiy:" -ForegroundColor Yellow
$events = Get-EventLog -LogName Application -Source "IIS*","ASP.NET*","IIS Express*" -Newest 20 -EntryType Error,Warning
if ($events) {
    $events | ForEach-Object {
        Write-Host "`n[$($_.TimeGenerated)] $($_.Source)" -ForegroundColor Red
        Write-Host "$($_.Message)" -ForegroundColor Gray
        Write-Host "-" * 50
    }
} else {
    Write-Host "Oshibok IIS v zhurnale sobytiy ne naydeno" -ForegroundColor Green
}

# 2. Proverka konfiguratsii sayta
Write-Host "`n2. Proverka konfiguratsii sayta FastenerBackend:" -ForegroundColor Yellow
try {
    $site = Get-Website -Name "FastenerBackend" -ErrorAction Stop
    Write-Host "Sayt nayden:" -ForegroundColor Green
    Write-Host "  Put: $($site.physicalPath)" -ForegroundColor Gray
    Write-Host "  Port: $($site.bindings.Collection.protocol)://$($site.bindings.Collection.bindingInformation)" -ForegroundColor Gray
    
    # Proveryaem web.config
    $webConfigPath = Join-Path $site.physicalPath "web.config"
    if (Test-Path $webConfigPath) {
        Write-Host "  web.config: nayden" -ForegroundColor Green
        try {
            $xml = [xml](Get-Content $webConfigPath)
            $handler = $xml.configuration.location.system.webServer.handlers.add
            Write-Host "  Obrabotchik: $($handler.name)" -ForegroundColor Gray
        } catch {
            Write-Host "  web.config: povrezhden!" -ForegroundColor Red
        }
    } else {
        Write-Host "  web.config: NE NAYDEN!" -ForegroundColor Red
    }
} catch {
    Write-Host "Sayt FastenerBackend ne nayden!" -ForegroundColor Red
}

# 3. Proverka modulov IIS
Write-Host "`n3. Proverka ustanovlennykh modulov IIS:" -ForegroundColor Yellow
$modules = @("AspNetCoreModuleV2", "RewriteModule", "WindowsAuthentication", "BasicAuthentication")
foreach ($module in $modules) {
    $installed = Get-WebGlobalModule -Name $module -ErrorAction SilentlyContinue
    if ($installed) {
        Write-Host "  $module : USTANOVLEN" -ForegroundColor Green
    } else {
        Write-Host "  $module : OTSUTSTVUET" -ForegroundColor Red
    }
}

# 4. Proverka papki s logami
Write-Host "`n4. Proverka logov IIS:" -ForegroundColor Yellow
$logPath = "C:\inetpub\logs\LogFiles"
if (Test-Path $logPath) {
    $latestLog = Get-ChildItem $logPath -Recurse -Filter "*.log" | 
                 Sort-Object LastWriteTime -Descending | 
                 Select-Object -First 1
    
    if ($latestLog) {
        Write-Host "  Posledniy log: $($latestLog.FullName)" -ForegroundColor Gray
        Write-Host "  Poslednie stroki:" -ForegroundColor Gray
        Get-Content $latestLog.FullName -Tail 10 | ForEach-Object {
            if ($_ -match "500") {
                Write-Host "    $_" -ForegroundColor Red
            } else {
                Write-Host "    $_" -ForegroundColor Gray
            }
        }
    }
} else {
    Write-Host "  Papka logov ne naydena: $logPath" -ForegroundColor Red
}

# 5. Proverka prav dostupa
Write-Host "`n5. Proverka prav dostupa k papke:" -ForegroundColor Yellow
$deployPath = "C:\Deploy\Backend"
if (Test-Path $deployPath) {
    $acl = Get-Acl $deployPath
    $access = $acl.Access | Where-Object {$_.IdentityReference -like "*IIS*" -or $_.IdentityReference -like "*IUSR*"}
    if ($access) {
        Write-Host "  Prava naydeny dlya:" -ForegroundColor Green
        $access | ForEach-Object {
            Write-Host "    $($_.IdentityReference): $($_.FileSystemRights)" -ForegroundColor Gray
        }
    } else {
        Write-Host "  Prava dlya IIS ne naydeny!" -ForegroundColor Red
    }
}

Write-Host "`n=== ANALIZ ZAVERSHEN ===" -ForegroundColor Cyan
Write-Host "`nRekomendatsii:" -ForegroundColor Yellow
Write-Host "1. Ubedites, chto ustanovlen .NET Core Hosting Bundle" -ForegroundColor Gray
Write-Host "2. Proverte, chto v IIS Manager v razdelen 'Moduli' est 'AspNetCoreModuleV2'" -ForegroundColor Gray
Write-Host "3. Perezapustite IIS: iisreset" -ForegroundColor Gray

pause