# Mathbrain

> Status: Desenvolvendo

## Sobre o desenvolvimento

Este é um projeto avaliativo do curso FullStack DevInHouse que foi desenvolvido dentro de 7 dias,
Utilizando de micro-serviços para todo o processamento de Back-end da aplicação.
Além do EFcore para a criação de API's, também foi utilizado o serviço RabbitMq para criação de mensageria
e a inteligência artificial ChatGPT para o desenvolvimento de algumas estruturas e algoritmos.
Até então, o meu maior desafio neste projeto foi a integração e adequação de todos os micro-serviços juntos.

### Sobre o projeto

O intúito do projeto é criar um site onde sejam mostradas equações matemáticas e, o jogador,
caso as responda corretamente, gera uma quantidade X de pontos que posteriormente pode entrar no ranking geral da aplicação.
Este projeto contém um total de 5 micro-serviços, sendo 2 destes criados em TDD, e 1 database SQLServer, sendo eles:

GeraNumeros: Api responsável por retornar para o Front-end as equações matemáticas e suas respectivas respostas.
As equações são geradas de forma a serem dificultadas a cada 10 equações, aumentando os numeros aleatóriamente gerados e adicionando novos operadores matemáticos

RankingApi: Api responsável por receber do Front-end as respostas do jogador e seu respectivo tempo de resposta.
Sua função é atualizar a tabela EmProcesso do database, para que fique claro que os dados do jogador estão começando a ser processados, assim como também iniciar a primeira Queue RabbitMq, enviando os dados do jogador para a próxima etapa.

CalculaRanking: Console App responsável por consumir a fila criada pela RankingApi, e então calcular a pontuação
do jogador relativo ao seu tempo de resposta. Caso o jogador responda mais de uma pergunta corretamente
em sequência, recebe pontos adicionais, e caso erre alguma questão a sequência é reiniciada. Este app também é responsável por calcular quantas perguntas o jogador acertou e também o menor tempo de resposta entre as respostas corretas. Após o processamento, este app cria uma nova Queue RabbitMq, e envia todos os dados obtidos para a próxima etapa.

DbWrite: Console App responsável por consumir a fila criada pela CalculaRanking, inserir estes dados no banco de
dados e atualizar a tabela EmProcesso do respectivo usuário para sinalizar o fim do processo.

CoreApi: Api responsável por criar o banco de dados e consultá-lo, retornando as informações para o Front-end.
Uma das requisições desta Api apenas é realizada caso o processo respectivo do usuário que está sendo requisitado estiver finalizado.

O Front-End deste projeto está localizado dentro da estrutura RazorPages, utilizado para realizar a parte de register/login/autenticação de usuário.

#### Como testar

Inicialmente, para criar o database, deve ser realizado o comando update-database dentro da CoreApi.
Após isto, inicie os 5 micro-serviços e logo após o RazorPages. A página estará localizada no Localhost que o RazorPages gerar.
