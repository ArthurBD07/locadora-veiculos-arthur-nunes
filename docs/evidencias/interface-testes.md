# Verificação visual real — 16/11/2026

Ambiente: http://localhost:5080, banco isolado LocadoraTP2_Validacao_20261116 em SQL Server Developer. Operações realizadas pelo agente no navegador, usando dados fictícios.

- Clientes: cadastro, listagem com confirmação de salvamento, edição de nome, filtro sem correspondência e limpeza do filtro.
- Fabricantes: cadastro, listagem e edição do nome.
- Categorias: cadastro, listagem e edição da descrição.
- Veículos: cadastro com fabricante/categoria selecionados e edição da quilometragem para 1300.
- Aluguéis: cadastro com cliente/veículo selecionados, edição do total para R$ 500,00 e visualização com os nove campos bloqueados.
- Layout móvel: inspeção visual em 390 × 844; menu e tabela com rolagem própria, campos reorganizados.
- Exclusões e conflitos de FK foram verificados pelo script HTTP; não são apresentados como operações visuais executadas.

Capturas: interface-clientes.jpg, interface-mobile.jpg, interface-alugueis.jpg e interface-aluguel-detalhes.jpg. Os registros de demonstração permanecem apenas no banco isolado.

## Validação adicional no SQL Express

A aplicação foi executada em http://localhost:5081 com o banco separado LocadoraTP2_Express_Validacao_20261116. Um cliente fictício foi cadastrado visualmente; a listagem confirmou o salvamento e a consulta SQL direta encontrou o mesmo nome/e-mail. Evidências: interface-sql-express.jpg e sql-express-persistencia.txt. A instalação terminou com resultado Aprovado e código 0, conforme o resumo do instalador Microsoft. A bateria HTTP teve 81 verificações aprovadas também nessa instância.

## Conferência após o recebimento do enunciado

As sete consultas originais foram selecionadas e executadas visualmente, com resultados reais dos dados fictícios do TP1. Foram verificados estado vazio e erro de período invertido. A tela de consultas foi inspecionada em tamanho de celular. A confirmação de exclusão passou a ser um diálogo explícito da aplicação; foi aberta e cancelada, mantendo o registro. O iniciador Abrir-Locadora.cmd foi executado com servidor desligado e abriu o navegador após compilar e preparar os dados. Depois disso, as baterias de 81 e 16 verificações passaram novamente.
