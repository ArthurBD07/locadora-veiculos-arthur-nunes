$ErrorActionPreference = 'Stop'
$projectRoot = Split-Path $PSScriptRoot -Parent
$url = 'http://localhost:5081'
Set-Location -LiteralPath $projectRoot
function Test-Locadora {
    try {
        $response = Invoke-WebRequest "$url/api/clientes" -UseBasicParsing -TimeoutSec 3
        return $response.StatusCode -eq 200 -and $response.Headers['Content-Type'] -match 'application/json'
    } catch { return $false }
}
if (-not (Test-Locadora)) {
    if (-not (Get-Command dotnet -ErrorAction SilentlyContinue)) { throw 'O .NET precisa estar instalado neste computador.' }
    $buildLog = Join-Path $projectRoot 'scripts/compilacao-local.log'
    & dotnet build (Join-Path $projectRoot 'LocadoraVeiculos/LocadoraVeiculos.csproj') -p:EnableWorkloadResolver=false *> $buildLog
    if ($LASTEXITCODE -ne 0) { throw "Não foi possível compilar. Consulte $buildLog" }
    $env:ASPNETCORE_ENVIRONMENT = 'Development'
    $env:ConnectionStrings__DefaultConnection = 'Server=.\SQLEXPRESS;Database=LocadoraTP2_Express_Validacao_20261116;Trusted_Connection=True;TrustServerCertificate=True;'
    $env:Logging__LogLevel__Microsoft = 'Warning'
    Start-Process -FilePath 'dotnet' -ArgumentList @('run','--project','LocadoraVeiculos','--no-build','--no-launch-profile','--urls',$url) -WorkingDirectory $projectRoot -WindowStyle Hidden -RedirectStandardOutput (Join-Path $projectRoot 'scripts/servidor-local.log') -RedirectStandardError (Join-Path $projectRoot 'scripts/servidor-erros.log') | Out-Null
    $ready = $false
    for ($attempt = 0; $attempt -lt 60; $attempt++) {
        if (Test-Locadora) { $ready = $true; break }
        Start-Sleep -Seconds 1
    }
    if (-not $ready) { throw 'O sistema não respondeu. Confira scripts/servidor-erros.log e se o SQL Express está iniciado.' }
}
& (Join-Path $PSScriptRoot 'Preparar-Demonstracao.ps1') -BaseUrl $url
Start-Process $url
Write-Host 'Locadora pronta. O navegador foi aberto em http://localhost:5081'
