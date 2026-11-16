import assert from 'node:assert/strict';
import {readFile,writeFile} from 'node:fs/promises';
const base=process.env.BASE_URL||'http://localhost:5081';
const fixture=JSON.parse((await readFile('scripts/dados-tp1-demo.json','utf8')).replace(/^\uFEFF/,''));
const evidence=[];
async function get(path,status=200){const response=await fetch(base+'/api/'+path);const data=await response.json();assert.equal(response.status,status,path);evidence.push({path,status:response.status});return data;}
const fabricante=(await get('fabricantes')).find(x=>x.nome===fixture.fabricantes[0].Nome);
const semVeiculo=(await get('fabricantes')).find(x=>x.nome===fixture.fabricantes[1].Nome);
const categoria=(await get('categorias')).find(x=>x.nome===fixture.categorias[0].Nome);
const cliente=(await get('clientes')).find(x=>x.email===fixture.clientes[0].Email);
const veiculo=(await get('veiculos')).find(x=>x.placa===fixture.veiculos[0].Placa);
assert.ok(fabricante&&semVeiculo&&categoria&&cliente&&veiculo,'Dados fictícios do TP1 presentes');
for(const [route,key,id]of [
 ['veiculos-por-fabricante/'+fabricante.fabricanteId,'veiculoId',veiculo.veiculoId],
 ['veiculos-por-categoria/'+categoria.categoriaId,'veiculoId',veiculo.veiculoId],
 ['alugueis-por-cliente/'+cliente.clienteId,'veiculoId',veiculo.veiculoId],
 ['alugueis-por-veiculo/'+veiculo.veiculoId,'veiculoId',veiculo.veiculoId]
])assert.ok((await get('filtros/'+route)).some(x=>x[key]===id),route);
assert.ok((await get('filtros/alugueis-por-periodo?inicio=2026-10-12T00:00:00&fim=2026-10-13T00:00:00')).some(x=>x.veiculoId===veiculo.veiculoId));
const status=fixture.alugueis[0].DataDevolucao?'devolvido':'aberto';
assert.ok((await get('filtros/alugueis-por-status?status='+status)).some(x=>x.veiculoId===veiculo.veiculoId));
const left=await get('filtros/fabricantes-com-veiculos?nome=Demo');
assert.ok(left.some(x=>x.fabricanteId===fabricante.fabricanteId&&x.veiculoId===veiculo.veiculoId));
assert.ok(left.some(x=>x.fabricanteId===semVeiculo.fabricanteId&&x.veiculoId===null),'LEFT JOIN preserva fabricante sem veículo');
await get('filtros/alugueis-por-periodo?inicio=2026-10-15&fim=2026-10-12',400);
await get('filtros/alugueis-por-status?status=invalido',400);
await get('filtros/veiculos-por-fabricante/0',400);
assert.equal((await get('filtros/fabricantes-com-veiculos?nome=INEXISTENTE_TP2')).length,0);
await writeFile('docs/evidencias/consultas-tp1-testes.json',JSON.stringify({date:new Date().toISOString(),base,result:'passed',checks:evidence},null,2));
console.log(`${evidence.length} verificações aprovadas nas consultas originais e seus dados fictícios.`);
