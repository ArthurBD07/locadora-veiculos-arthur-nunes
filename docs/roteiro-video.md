# Pitch individual — roteiro de 4 minutos

O enunciado exige entre 3 e 5 minutos, com gravação de tela e áudio, abordando problema, modelagem e pelo menos duas consultas e resultados no front-end. Não exige câmera ou rosto nos trechos fornecidos. Não exiba vídeos de terceiros.

## Antes de gravar

Dê dois cliques em Abrir-Locadora.cmd. No navegador, clique em **Consultas do TP1 →**. Os exemplos originais já estarão carregados no SQL Express. Faça a gravação com microfone no OBS ou outro gravador de tela.

## 0:00–0:45 — problema

“Sou Arthur Nunes Henrique dos Santos e apresento a Locadora de Veículos. Uma locadora precisa saber quais veículos pertencem a cada fabricante e categoria, quem são os clientes e quais aluguéis estão em aberto. Quando essas informações ficam dispersas, fica difícil consultar o histórico e identificar a composição da frota. A proposta é reunir esses dados em uma interface web conectada ao banco, com cadastro, edição, exclusão e pesquisa.”

## 0:45–1:25 — modelagem

“Utilizei o back-end do TP1 em C# com ASP.NET Core e Entity Framework Core, integrado ao SQL Server Express. A modelagem possui cinco entidades: Cliente, Veículo, Fabricante, Categoria e Aluguel. Um fabricante pode ter vários veículos, assim como uma categoria. Um cliente pode ter vários aluguéis e um veículo pode aparecer em vários aluguéis ao longo do tempo. As chaves estrangeiras mantêm esses relacionamentos, e registros vinculados têm sua exclusão protegida.”

Mostre rapidamente o menu com os cinco cadastros. Volte para Consultas do TP1.

## 1:25–2:25 — consulta 1: veículos por fabricante

Selecione **Veículos por fabricante**. No campo Fabricante, escolha **Fabricante Demo Arthur 1210** e clique em **Consultar banco**.

“A primeira consulta responde quais veículos pertencem ao fabricante escolhido. A interface utiliza a rota original de veículos por fabricante. No banco, essa consulta relaciona Veículo e Fabricante por um INNER JOIN, usando a chave estrangeira FabricanteId. O resultado exibe o Veiculo Demo Arthur, com placa TPA1N26, ano 2024 e quilometragem 15000. São dados fictícios reaproveitados do TP1 e consultados no SQL Express, e não uma tabela fixa escrita no HTML. Se eu escolher outro fabricante, o resultado acompanha os dados encontrados no banco.”

## 2:25–3:25 — consulta 2: fabricantes com e sem veículos

Selecione **Fabricantes com e sem veículos**, digite **Demo** no nome e clique em **Consultar banco**.

“A segunda consulta mostra a composição da frota e também fabricantes que ainda não possuem veículos. Ela utiliza a rota original de fabricantes com veículos e um LEFT JOIN. O primeiro fabricante aparece associado ao veículo. Já o Fabricante Demo Sem Veiculo 1210 continua no resultado, com os campos do veículo vazios. Essa é a diferença prática do LEFT JOIN: o fabricante é preservado mesmo sem correspondência. A visualização ajuda a identificar fabricantes que ainda não têm participação na frota cadastrada.”

## 3:25–4:00 — resultados e encerramento

“Além desses exemplos, a interface disponibiliza as consultas originais por categoria, cliente, veículo, período e status. Os filtros dos cadastros permitem pesquisar pelo menos dois campos em cada entidade. O trabalho preserva a API e o histórico do TP1, com persistência real, validações e tratamento de relacionamentos. O código, os wireframes compartilháveis e o relatório no modelo SBC estão no repositório. Obrigado.”

## Conferência final

- Cronometre: a fala e os cliques devem durar entre 3 e 5 minutos; ajuste seu ritmo sem acelerar demais.
- Confirme que o áudio do microfone foi gravado e que as duas consultas aparecem com resultados legíveis.
- Não apague os dados de demonstração antes de gravar. O iniciador recupera os exemplos que faltarem sem duplicar os existentes.
- Envie o vídeo à atividade correspondente do Canvas e o ZIP à entrega do TP2. Confira também o vínculo do seu GitHub com a turma.
