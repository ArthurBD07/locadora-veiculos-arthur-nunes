# Trabalho Prático 1 - Etapa 3 - Arthur Nunes

Sistema de locadora de veículos desenvolvido em C# com ASP.NET Core, Entity Framework Core, SQL Server/SQL Express e Swagger.

## Entrega da Etapa 3

Esta versão inclui:

- Swagger integrado e funcional;
- documentação dos 32 endpoints existentes;
- testes manuais dos 25 endpoints de CRUD;
- testes manuais dos 7 endpoints de filtros;
- evidências das chamadas e respostas no relatório em PDF;
- tratamento de validações, registros inexistentes e conflitos.

O relatório final está no arquivo:

`ArthurNunes_TrabalhoPratico1_Etapa3.pdf`

## Tecnologias

- .NET 7 e ASP.NET Core;
- Entity Framework Core 7.0.20;
- SQL Server e SQL Express;
- Swashbuckle/Swagger;
- C#.

## Estrutura da API

A API possui CRUD completo para:

- Alugueis;
- Categorias;
- Clientes;
- Fabricantes;
- Veiculos.

Cada CRUD oferece listagem, consulta por identificador, criação, atualização e exclusão, totalizando 25 operações.

O `FiltrosController` possui 7 consultas:

- veículos por fabricante;
- veículos por categoria;
- aluguéis por cliente;
- aluguéis por veículo;
- aluguéis por período;
- aluguéis por status;
- fabricantes com veículos.

As consultas utilizam INNER JOIN e LEFT JOIN explícitos.

## Executar no Visual Studio 2022

1. Extraia o ZIP em uma pasta local.
2. Abra `LocadoraVeiculos.sln`.
3. Aguarde a restauração dos pacotes NuGet.
4. Escolha o perfil de execução compatível com o banco instalado:
   - **SQL Express**: utiliza `.\SQLEXPRESS`;
   - **SQL Server local**: utiliza `localhost`.
5. Pressione **F5**.
6. Acesse `http://localhost:5107/swagger` caso o navegador não abra automaticamente.

Em ambiente de desenvolvimento, as migrations são aplicadas na inicialização para criar ou atualizar o banco `LocadoraVeiculosArthurNunes`.

## Configuração do Swagger

O Swagger é registrado por `AddSwaggerGen()` e disponibilizado em ambiente de desenvolvimento por `UseSwagger()` e `UseSwaggerUI()`.

## Banco de dados e integridade

O projeto contém cinco entidades, chaves primárias e estrangeiras, relacionamentos 1:N, restrições de exclusão, validações por DataAnnotations e índices únicos para CPF, e-mail e placa.

Os controladores retornam códigos HTTP adequados, incluindo 200, 201, 204, 400, 404 e 409 conforme a operação e o resultado.

## Verificação da Etapa 3

Foram executadas manualmente no Swagger todas as 32 operações da API. O relatório contém as evidências dos CRUDs, dos sete filtros, de uma resposta 404 e da persistência de atualização após PUT.

Aluno: **Arthur Nunes Henrique dos Santos**.
