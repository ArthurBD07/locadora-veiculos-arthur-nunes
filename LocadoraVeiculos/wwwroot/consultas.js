"use strict";
const $ = s => document.querySelector(s);
const configs = {
 fabricante:{source:'fabricantes',id:'fabricanteId',label:'Fabricante',route:'veiculos-por-fabricante/',description:'INNER JOIN entre veículos e fabricantes. Mostra a frota do fabricante selecionado.'},
 categoria:{source:'categorias',id:'categoriaId',label:'Categoria',route:'veiculos-por-categoria/',description:'INNER JOIN entre veículos e categorias. Mostra os veículos da categoria selecionada.'},
 cliente:{source:'clientes',id:'clienteId',label:'Cliente',route:'alugueis-por-cliente/',description:'Relaciona aluguéis, clientes e veículos para consultar o histórico do cliente.'},
 veiculo:{source:'veiculos',id:'veiculoId',label:'Veículo',route:'alugueis-por-veiculo/',description:'Relaciona aluguéis, veículos e clientes para consultar o histórico do veículo.'},
 periodo:{route:'alugueis-por-periodo',description:'Aluguéis cuja retirada está entre o início e o fim informados, incluindo os limites.'},
 status:{route:'alugueis-por-status',description:'Aluguéis abertos ou devolvidos, conforme a presença da data de devolução.'},
 frota:{route:'fabricantes-com-veiculos',description:'LEFT JOIN: inclui fabricantes sem veículos, permitindo identificar lacunas na frota.'}
};
const labels={veiculoId:'ID veículo',modelo:'Modelo',placa:'Placa',anoFabricacao:'Ano',quilometragem:'Quilometragem',fabricanteId:'ID fabricante',fabricante:'Fabricante',categoriaId:'ID categoria',categoria:'Categoria',aluguelId:'ID aluguel',clienteId:'ID cliente',cliente:'Cliente',dataRetirada:'Retirada',dataPrevistaDevolucao:'Devolução prevista',dataDevolucao:'Devolução efetiva',valorTotal:'Valor total'};
let version=0;
function node(tag,text,props={}){const n=document.createElement(tag);if(text!=null)n.textContent=text;Object.assign(n,props);return n;}
async function get(path){const r=await fetch('/api/'+path);const d=await r.json().catch(()=>null);if(!r.ok)throw Error(d?.mensagem||Object.values(d?.errors||{}).flat().join(' ')||'Não foi possível consultar o banco. Tente novamente.');return d;}
function field(label,input){const wrapper=node('label',label);wrapper.append(input);$('#parameters').append(wrapper);}
function clearResults(){ $('thead').replaceChildren();$('tbody').replaceChildren();$('#count').textContent='Escolha os parâmetros e consulte';$('#empty').hidden=true;}
async function configure(){const token=++version,c=configs[$('#kind').value];clearResults();$('#parameters').replaceChildren();$('#description').textContent=c.description;$('#message').textContent='';$('#search').disabled=true;
 try{if(c.source){$('#message').textContent='Carregando opções…';const rows=await get(c.source);if(token!==version)return;const select=node('select',null,{name:'id',required:true});select.append(node('option','Selecione',{value:''}));for(const row of rows)select.append(node('option',row.nome||`${row.modelo} · ${row.placa}`,{value:row[c.id]}));field(c.label,select);}
 else if($('#kind').value==='periodo'){for(const [name,label]of [['inicio','Retirada a partir de'],['fim','Retirada até']])field(label,node('input',null,{type:'datetime-local',name,required:true}));}
 else if($('#kind').value==='status'){const select=node('select',null,{name:'status'});select.append(node('option','Em aberto',{value:'aberto'}),node('option','Devolvidos',{value:'devolvido'}));field('Status do aluguel',select);}
 else field('Nome do fabricante (opcional)',node('input',null,{name:'nome',maxLength:100}));
 if(token===version)$('#message').textContent='';
 }catch(e){if(token===version)$('#message').textContent=e.message;}finally{if(token===version)$('#search').disabled=false;}}
function value(key,v){if(v===null)return '—';if(key.startsWith('data'))return new Date(v).toLocaleString('pt-BR');if(key==='valorTotal')return Number(v).toLocaleString('pt-BR',{style:'currency',currency:'BRL'});return String(v);}
$('#query').onsubmit=async e=>{e.preventDefault();const token=++version,c=configs[$('#kind').value],params=new URLSearchParams(new FormData($('#query')));params.delete('consulta');const route=c.source?c.route+params.get('id'):c.route+'?'+params.toString();clearResults();$('#search').disabled=true;$('#message').textContent='Consultando o banco…';try{const rows=await get('filtros/'+route);if(token!==version)return;const keys=rows.length?Object.keys(rows[0]):[];const header=node('tr');keys.forEach(k=>header.append(node('th',labels[k]||k)));$('thead').append(header);for(const row of rows){const tr=node('tr');keys.forEach(k=>tr.append(node('td',value(k,row[k]))));$('tbody').append(tr);}$('#count').textContent=`${rows.length} resultado(s)`;$('#empty').hidden=rows.length>0;$('#message').textContent='Consulta concluída.';}catch(error){if(token===version)$('#message').textContent=error.message;}finally{if(token===version)$('#search').disabled=false;}};
$('#kind').onchange=configure;configure();
