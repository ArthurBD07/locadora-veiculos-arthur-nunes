# TP2 — Locadora de Veículos

Interface integrada à API original do TP1. Histórico preservado a partir de `ea8b138`, com alterações na branch `tp2/interface-validacao`.

## Executar

Abra `LocadoraVeiculos.sln` e selecione o perfil **SQL Express** ou **SQL Server local**, conforme a instância instalada. Acesse a raiz `http://localhost:5107/` para a interface e `/swagger` para a documentação. Em desenvolvimento, as migrations originais são aplicadas ao banco configurado.

O perfil SQL Express usa `.\SQLEXPRESS`. O perfil local usa `localhost`. Para não tocar nos dados do TP1, configure `ConnectionStrings__DefaultConnection` com o nome de um banco exclusivo de teste. A primeira execução utilizou `http://localhost:5080` e `LocadoraTP2_Validacao_20261116`, na edição Developer. A validação final no Express utilizou `http://localhost:5081` e `LocadoraTP2_Express_Validacao_20261116`.

Não é necessário instalar Node.js para executar o produto. Node.js é usado somente pelo script de teste.

## Recursos

- Clientes, veículos, fabricantes, categorias e aluguéis: cadastro, listagem, visualização, edição e exclusão com confirmação.
- Filtros na API, combinados por AND: clientes (nome/CPF/e-mail), fabricantes (nome/ID), categorias (nome/descrição), veículos (modelo/placa/fabricante/categoria) e aluguéis (cliente/veículo/limites de datas).
- Seletores de relacionamentos, mensagens de validação e conflitos, estado vazio e layout responsivo.
- Modelos, migrations e sete consultas do `FiltrosController` do TP1 mantidos.

## Verificações

Execute `node tests/api-integration.mjs` a partir da raiz do repositório com a aplicação em execução. O script usa dados fictícios e os remove ao concluir. Defina `BASE_URL` caso a porta seja diferente. Use exclusivamente um banco de teste. O resultado detalhado é escrito em `docs/evidencias/api-testes.json`.

A primeira rodada teve 81 verificações HTTP aprovadas, em SQL Server Developer. A compilação não apresentou erros; o alvo .NET 7 gera aviso de fim de suporte. Depois da instalação da instância SQLEXPRESS, a mesma bateria de 81 verificações também passou no SQL Server 2022 Express. A edição foi confirmada por consulta ao servidor.

## Documentação

- `docs/relatorio.tex`: fonte LaTeX no estilo SBC.
- `docs/sbc-template.sty`: estilo publicado pelo SBGames, sem alterações.
- `docs/wireframes/`: cinco SVGs editáveis.
- `docs/evidencias/`: testes e capturas reais.
- `docs/roteiro-video.md`: roteiro previsto para 4min40s.

Para compilar o PDF: execute `tectonic docs/relatorio.tex` ou use uma distribuição LaTeX compatível.

## Pendências de fechamento

Conferir os três screenshots originais da rubrica; revisar dados institucionais exigidos no relatório; gravar/publicar o vídeo e registrar seu link. A branch já foi publicada. O pacote anterior citado na conversa não estava acessível como anexo e não foi usado. O trabalho não deve ser apresentado como entrega final antes desses passos.
