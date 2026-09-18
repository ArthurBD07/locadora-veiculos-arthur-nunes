# Trabalho Prático 1 — Locadora de Veículos

**Aluno:** Arthur Nunes  
**Entrega:** Etapa 1

Projeto ASP.NET Core com Entity Framework Core e SQL Server Express. Esta etapa contém somente a modelagem solicitada: entidades, chaves primárias, chaves estrangeiras, relacionamentos e `ApplicationContext`.

## Estrutura

- `Models`: `Veiculo`, `Fabricante`, `Cliente`, `Aluguel` e `Categoria`.
- `Data/ApplicationContext.cs`: contexto do Entity Framework.
- `appsettings.json`: conexão com o SQL Server Express.
- `MODELO-CONCEITUAL.md`: representação simples das entidades e relacionamentos.

## Abrir no Visual Studio

1. Abra o arquivo `LocadoraVeiculos.sln`.
2. Aguarde a restauração dos pacotes NuGet.
3. Confira se o SQL Server Express está instalado com a instância `SQLEXPRESS`.
4. Compile a solução.

Controllers, CRUD, filtros, Swagger e testes serão acrescentados somente nas próximas etapas.
