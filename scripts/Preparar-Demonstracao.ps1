param([string]$BaseUrl = 'http://localhost:5081')
$ErrorActionPreference = 'Stop'
$samples = Get-Content (Join-Path $PSScriptRoot 'dados-tp1-demo.json') -Raw | ConvertFrom-Json
$maps = @{}
foreach ($spec in @(
    @{ Route='fabricantes'; Id='FabricanteId'; Unique='Nome' },
    @{ Route='categorias'; Id='CategoriaId'; Unique='Nome' },
    @{ Route='clientes'; Id='ClienteId'; Unique='Email' },
    @{ Route='veiculos'; Id='VeiculoId'; Unique='Placa' },
    @{ Route='alugueis'; Id='AluguelId'; Unique=$null }
)) {
    $route=$spec.Route; $id=$spec.Id; $maps[$route]=@{}
    $existing = @(foreach ($record in (Invoke-RestMethod "$BaseUrl/api/$route")) { $record })
    foreach ($row in $samples.$route) {
        $originalId=$row.$id
        $body=@{}
        foreach ($property in $row.PSObject.Properties) { if($property.Name -ne $id){$body[$property.Name]=$property.Value} }
        if($route -eq 'veiculos') { $body.FabricanteId=$maps.fabricantes[[string]$row.FabricanteId]; $body.CategoriaId=$maps.categorias[[string]$row.CategoriaId] }
        if($route -eq 'alugueis') { $body.ClienteId=$maps.clientes[[string]$row.ClienteId]; $body.VeiculoId=$maps.veiculos[[string]$row.VeiculoId] }
        $found = if($spec.Unique) { $existing | Where-Object { $_.($spec.Unique) -eq $row.($spec.Unique) } | Select-Object -First 1 } else { $existing | Where-Object { $_.ClienteId -eq $body.ClienteId -and $_.VeiculoId -eq $body.VeiculoId -and ([datetime]$_.DataRetirada) -eq ([datetime]$body.DataRetirada) } | Select-Object -First 1 }
        if(-not $found) { $found=Invoke-RestMethod "$BaseUrl/api/$route" -Method Post -ContentType 'application/json; charset=utf-8' -Body ([Text.Encoding]::UTF8.GetBytes(($body | ConvertTo-Json -Depth 5))); $existing += $found }
        $maps[$route][[string]$originalId]=$found.$id
    }
}
Write-Host 'Exemplos fictícios do TP1 disponíveis no banco SQL Express, sem duplicação.'
