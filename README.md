# Trabalho Prático 1 — Etapa 2 — Arthur Nunes

Evolução da Etapa 1, mantendo `LocadoraVeiculos.sln`, .NET 7, EF Core 7.0.20, `ApplicationContext` e as cinco entidades originais. Os Controllers usam diretamente o contexto, sem camadas adicionais.

## Executar no Visual Studio 2022

1. Extraia todo o ZIP em uma pasta nova, por exemplo `Documentos/ArthurNunes_Etapa2`. Não execute dentro do ZIP nem sobreponha a Etapa 1.
2. Abra `ArthurNunes_Etapa2/LocadoraVeiculos.sln` e aguarde a restauração do NuGet.
3. Ao lado do botão verde, escolha o perfil correspondente ao banco instalado:
   - **SQL Express**: usa `.\SQLEXPRESS`, conforme o requisito e o appsettings.json original.
   - **SQL Server local**: usa `localhost`. É a opção para o computador verificado em 26/09, que tem SQL Server Developer na instância padrão.
4. Pressione **F5**. O banco `LocadoraVeiculosArthurNunes` e as tabelas serão criados pelas migrations na primeira execução em Development. O usuário Windows precisa ter permissão de criação de banco.
5. O Swagger deve abrir em `http://localhost:5107/swagger`. Se o navegador não abrir sozinho, acesse esse endereço com o programa em execução.

O perfil local permite testar nesta máquina; a configuração exigida de SQL Express está mantida. Para validar especificamente a edição Express, é necessário ter a instância SQLEXPRESS instalada e executar o perfil SQL Express. Não foi instalada outra edição do SQL Server nesta tarefa.

Se aparecer erro de conexão, migration ou compilação, envie o erro completo antes de modificar o projeto. Não apague bancos existentes. Esta é a primeira migration do projeto original, que não possuía migrations; um banco criado manualmente com tabelas homônimas precisa ser conferido antes de aplicar esta migration.

## Primeiro teste no Swagger

Em cada operação, clique em **Try it out**, preencha o corpo e clique em **Execute**. POST deve retornar **201**. Anote os IDs retornados: os números 1 abaixo são exemplos para um banco novo.

1. `POST /api/Fabricantes`

```json
{ "nome": "Fiat" }
```

2. `POST /api/Categorias`

```json
{ "nome": "Economico", "descricao": "Veiculos compactos" }
```

3. `POST /api/Clientes` — dados fictícios para teste.

```json
{ "nome": "Cliente de Teste", "cpf": "12345678901", "email": "cliente@example.com" }
```

4. `POST /api/Veiculos` — substitua os IDs pelos retornados.

```json
{
  "modelo": "Argo",
  "anoFabricacao": 2023,
  "quilometragem": 15000,
  "placa": "ABC1D23",
  "fabricanteId": 1,
  "categoriaId": 1
}
```

5. `POST /api/Alugueis` — substitua os IDs pelos retornados.

```json
{
  "dataRetirada": "2026-09-26T10:00:00",
  "dataPrevistaDevolucao": "2026-09-28T10:00:00",
  "dataDevolucao": null,
  "kmInicial": 15000,
  "kmFinal": null,
  "valorDiaria": 100,
  "valorTotal": 200,
  "clienteId": 1,
  "veiculoId": 1
}
```

Consulte `GET /api/Veiculos` e `GET /api/Alugueis`. Para atualizar, use PUT com todos os campos e o ID igual ao da URL. O valor total é informado pelo usuário; não há regra de cobrança automática exigida pela rubrica. Para devolver, informe dataDevolucao e kmFinal juntos. CPF é validado quanto ao formato, não quanto aos dígitos verificadores.

## Conferência dos quatro critérios da Etapa 2

| Critério | Implementação |
|---|---|
| CRUD para todas as entidades | Fabricantes, Categorias, Clientes, Veiculos e Alugueis: GET lista, GET por ID, POST, PUT e DELETE — 25 operações. |
| EF e SQL Express | EF Core 7.0.20, UseSqlServer, cinco DbSets, quatro relacionamentos 1:N, migration incluída e conexão `.\SQLEXPRESS` preservada. |
| Validação e erros | DataAnnotations, ApiController/ModelState, FKs existentes, datas e quilometragens coerentes, índices únicos de CPF/e-mail/placa, HTTP 400/404/409 e tratamento de conflitos de gravação. Exclusões vinculadas são bloqueadas no Controller e no banco. |
| Pelo menos 5 filtros e 2 tipos de JOIN | Sete rotas em FiltrosController, LINQ join (INNER JOIN) e join ... into com DefaultIfEmpty() (LEFT JOIN). |

## Filtros — todos GET

| Rota após `/api/Filtros/` | Resultado |
|---|---|
| `veiculos-por-fabricante/1` | Veículos do fabricante — INNER JOIN. |
| `veiculos-por-categoria/1` | Veículos da categoria — INNER JOIN. |
| `alugueis-por-cliente/1` | Aluguéis do cliente, com cliente e veículo — INNER JOIN. |
| `alugueis-por-veiculo/1` | Aluguéis do veículo, com cliente e veículo — INNER JOIN. |
| `alugueis-por-periodo?inicio=2026-09-26T00:00:00&fim=2026-09-28T23:59:59` | Retiradas no intervalo inclusivo — INNER JOIN. |
| `alugueis-por-status?status=aberto` | Sem devolução; use devolvido para devolvidos — INNER JOIN. |
| `fabricantes-com-veiculos?nome=Fiat` | Fabricantes filtrados por parte do nome, inclusive sem veículos — LEFT JOIN. Omita nome para listar todos. |

Para observar o LEFT JOIN, cadastre outro fabricante sem veículos e consulte fabricantes-com-veiculos: ele aparece com veiculoId e modelo nulos. Filtros válidos sem correspondências retornam 200 com lista vazia.

## Verificação realizada

- Compilação Release: zero erros, sem alertas de nullable. O SDK emite o aviso de fim de suporte do .NET 7, mantido conforme o projeto original e a solicitação.
- 149 verificações HTTP e de dados aprovadas, usando SQL Server Developer real em banco separado: CRUD das cinco entidades, status HTTP, atualização persistida, validações, duplicidades, FKs, exclusões e os sete filtros.
- SQL emitido pelo EF conferido: há INNER JOIN e LEFT JOIN reais executados no SQL Server.
- Criação das tabelas por migration e reinicialização sem reaplicar a migration verificadas.
- A execução específica em SQL Express permanece dependente de uma instância Express disponível. Não há banco de dados nem dados pessoais dentro deste ZIP.

Depois de conferir a execução, atualize seu repositório pessoal e baixe o ZIP pelo GitHub para enviar ao Canvas, conforme a orientação de entrega. Este pacote não inclui relatório ou prints da Etapa 3.
