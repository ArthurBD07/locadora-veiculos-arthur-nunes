import assert from 'node:assert/strict';
import {mkdir,writeFile} from 'node:fs/promises';
const base=process.env.BASE_URL||'http://localhost:5080';
const evidence=[];const created=[];const stamp=Date.now().toString().slice(-8);
async function call(method,path,data,status){const response=await fetch(base+'/api/'+path,{method,headers:{'Content-Type':'application/json'},body:data===undefined?undefined:JSON.stringify(data)});const text=await response.text();assert.equal(response.status,status,`${method} ${path}: ${text}`);const result=text?JSON.parse(text):null;evidence.push({method,path,status:response.status});return result;}
async function create(route,id,data){const row=await call('POST',route,data,201);created.push({route,id,value:row[id]});return row;}
async function check(route,id,row,changes,filters){const key=row[id];assert.equal((await call('GET',`${route}/${key}`,undefined,200))[id],key);Object.assign(row,changes);await call('PUT',`${route}/${key}`,row,204);const reread=await call('GET',`${route}/${key}`,undefined,200);for(const [field,value]of Object.entries(changes))assert.equal(reread[field],value);for(const [field,value]of Object.entries(filters)){const list=await call('GET',`${route}?${new URLSearchParams({[field]:value})}`,undefined,200);assert.ok(list.some(x=>x[id]===key),`Filtro ${route}.${field}`);}const none=await call('GET',`${route}?${new URLSearchParams({[Object.keys(filters)[0]]:Object.keys(filters)[0].endsWith('Id')?'2147483647':'INEXISTENTE_'+stamp})}`,undefined,200);assert.equal(none.length,0);}
let failure;
try{
 const f=await create('fabricantes','fabricanteId',{nome:'Fabricante TP2 '+stamp});
 const c=await create('categorias','categoriaId',{nome:'Categoria '+stamp,descricao:'Teste automático SQL'});
 const cli=await create('clientes','clienteId',{nome:'Cliente fictício '+stamp,cpf:'123'+stamp,email:`teste${stamp}@example.com`});
 const v=await create('veiculos','veiculoId',{modelo:'Modelo '+stamp,anoFabricacao:2024,quilometragem:1200,placa:'TPA'+stamp.slice(-4),fabricanteId:f.fabricanteId,categoriaId:c.categoriaId});
 const a=await create('alugueis','aluguelId',{clienteId:cli.clienteId,veiculoId:v.veiculoId,dataRetirada:'2026-11-16T10:00:00',dataPrevistaDevolucao:'2026-11-19T10:00:00',kmInicial:1200,valorDiaria:150,valorTotal:450});
 await check('fabricantes','fabricanteId',f,{nome:'Fabricante editado '+stamp},{nome:stamp,fabricanteId:f.fabricanteId});
 await check('categorias','categoriaId',c,{descricao:'Descrição editada '+stamp},{nome:stamp,descricao:stamp});
 await check('clientes','clienteId',cli,{nome:'Cliente editado '+stamp},{nome:stamp,cpf:cli.cpf,email:cli.email});
 await check('veiculos','veiculoId',v,{quilometragem:1300},{modelo:stamp,placa:v.placa,fabricanteId:f.fabricanteId,categoriaId:c.categoriaId});
 await check('alugueis','aluguelId',a,{valorTotal:500},{clienteId:cli.clienteId,veiculoId:v.veiculoId});
 for(const route of ['clientes/'+cli.clienteId,'veiculos/'+v.veiculoId,'categorias/'+c.categoriaId,'fabricantes/'+f.fabricanteId])await call('DELETE',route,undefined,409);
 await call('POST','clientes',{nome:'Duplicado',cpf:cli.cpf,email:`duplicado${stamp}@example.com`},409);
 await call('POST','clientes',{nome:'Inválido',cpf:'x',email:'x'},400);
 await call('POST','veiculos',{...v,veiculoId:0,fabricanteId:2147483647},400);
 await call('POST','alugueis',{...a,aluguelId:0,dataPrevistaDevolucao:'2026-11-15T10:00:00'},400);
 await call('POST','alugueis',{...a,aluguelId:0,clienteId:2147483647},400);
 await call('PUT','clientes/'+cli.clienteId,{...cli,clienteId:cli.clienteId+1},400);
 await call('GET','clientes/2147483647',undefined,404);
}catch(error){failure=error;}
finally{for(const item of created.reverse()){await call('DELETE',`${item.route}/${item.value}`,undefined,204);await call('GET',`${item.route}/${item.value}`,undefined,404);}await mkdir('docs/evidencias',{recursive:true});await writeFile('docs/evidencias/api-testes.json',JSON.stringify({date:new Date().toISOString(),base,database:process.env.TEST_DATABASE||'LocadoraTP2_Validacao_20261116',result:failure?'failed':'passed',failure:failure?.message,checks:evidence},null,2));}
if(failure)throw failure;console.log(`${evidence.length} verificações HTTP aprovadas, registros de teste removidos.`);
