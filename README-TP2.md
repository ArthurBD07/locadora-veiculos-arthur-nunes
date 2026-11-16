# TP2 — Locadora de Veículos

Interface integrada à API original do TP1. Histórico preservado a partir de `ea8b138`, com alterações na branch `tp2/interface-validacao`.

## Executar

Para abrir no notebook preparado, dê dois cliques em **Abrir-Locadora.cmd**. O iniciador compila, abre o servidor em **http://localhost:5081** e recupera os exemplos fictícios do TP1 sem duplicá-los. Deixe a janela de abertura terminar; o navegador será aberto automaticamente.

Para desenvolvimento no Visual Studio, abra `LocadoraVeiculos.sln` e selecione o perfil **SQL Express** ou **SQL Server local**, conforme a instância instalada. Acesse a raiz `http://localhost:5107/` para a interface e `/swagger` para a documentação. Em desenvolvimento, as migrations originais são aplicadas ao banco configurado.

O perfil SQL Express usa `.\SQLEXPRESS`. O perfil local usa `localhost`. Para não tocar nos dados do TP1, configure `ConnectionStrings__DefaultConnection` com o nome de um banco exclusivo de teste. A primeira execução utilizou `http://localhost:5080` e `LocadoraTP2_Validacao_20261116`, na edição Developer. A validação final no Express utilizou `http://localhost:5081` e `LocadoraTP2_Express_Validacao_20261116`.

Não é necessário instalar Node.js para executar o produto. Node.js é usado somente pelo script de teste.

## Recursos

- Clientes, veículos, fabricantes, categorias e aluguéis: cadastro, listagem, visualização, edição e exclusão com confirmação.
- Filtros na API, combinados por AND: clientes (nome/CPF/e-mail), fabricantes (nome/ID), categorias (nome/descrição), veículos (modelo/placa/fabricante/categoria) e aluguéis (cliente/veículo/limites de datas).
- Seletores de relacionamentos, mensagens de validação e conflitos, estado vazio e layout responsivo.
- Modelos, migrations e sete consultas do `FiltrosController` do TP1 mantidos e acessíveis pela tela **Consultas do TP1**.
- Exemplos fictícios originais do TP1 recuperados no banco de demonstração.
- [Wireframes compartilháveis](https://github.com/ArthurBD07/locadora-veiculos-arthur-nunes/tree/tp2/interface-validacao/docs/wireframes).

## Verificações

Execute `node tests/api-integration.mjs` a partir da raiz do repositório com a aplicação em execução. O script usa dados fictícios e os remove ao concluir. Defina `BASE_URL` caso a porta seja diferente. Use exclusivamente um banco de teste. O resultado detalhado é escrito em `docs/evidencias/api-testes.json`.

A primeira rodada teve 81 verificações HTTP aprovadas, em SQL Server Developer. A compilação não apresentou erros; o alvo .NET 7 gera aviso de fim de suporte. Depois da instalação da instância SQLEXPRESS, a mesma bateria de 81 verificações também passou no SQL Server 2022 Express. A edição foi confirmada por consulta ao servidor.

## Documentação

- `docs/relatorio.tex`: fonte LaTeX no estilo SBC.
- `docs/sbc-template.sty`: estilo publicado pelo SBGames, sem alterações.
- `docs/wireframes/`: seis SVGs editáveis e visualizações PNG.
- `docs/evidencias/`: testes e capturas reais.
- `docs/roteiro-video.md`: pitch de aproximadamente 4 minutos, alinhado ao enunciado.

Para compilar o PDF: execute `tectonic docs/relatorio.tex` ou use uma distribuição LaTeX compatível.

## Pendências de fechamento

O enunciado recebido foi conferido e as lacunas técnicas identificadas foram corrigidas. Resta gravar o pitch individual com tela e áudio, entre 3 e 5 minutos, e enviar o vídeo e o ZIP nas atividades correspondentes do Canvas. Confira também se o GitHub está vinculado à turma. A nota final depende da avaliação do professor.
