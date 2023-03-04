const questaoElement = document.querySelector(".question");
const respostasElement = document.querySelector(".options");
const timerElement = document.querySelector("#countdown");
const errosElement = document.querySelector("#numErros");
const fimDeJogoElement = document.querySelector(".fimDeJogo");
const leaderboardElement = document.querySelector(".ranking");

const melhorRankingElement = document.querySelector("#melhorRanking");
const ultimoRankingElement = document.querySelector("#ultimoRanking");
const qtdAcertosElement = document.querySelector("#qtdAcertos");
const melhorTempoElement = document.querySelector("#melhorTempo");
const popupJogo = document.querySelector("#popup-jogo");
const botaoElement = document.querySelector("#jogarNovamente");

const headerElement = document.getElementById("headerId").remove();

// Variáveis para armazenar as informações da API
let equacoes = [];
let respostas = [];

// Variáveis para armazenar as informações da API do jogador
let melhorRanking = 0.0;
let ultimoRanking = 0;
let qtdAcertos = 0;
let melhorTempo = 0.0;
let processamento = true;

// Variáveis para armazenar as respostas do jogador
let tempoDeResposta = [];
let tempoDeRespostaAtual = 0.0;
let respostasCorretas = [];

// Variáveis para controlar o jogo
let questaoAtual = 0;
let erros = 0;
let timer;

// Função para buscar as equações da API
function buscarEquacoes() {
  fetch("http://localhost:5293/api/GeraNumeros")
    .then((response) => response.json())
    .then((data) => {
      equacoes = data.operacao;
      respostas = data.resultado;
      exibirQuestao();
    })
    .catch((error) => console.error(error));
}

// Função para buscar os rankings atuais
async function getTopRankings() {
  const response = await fetch(
    "http://localhost:5119/api/UserRanking/GetUserRanking"
  )
    .then((data) => {
      return data.json();
    })
    .then((data) => {
      const sortedData = data.sort((a, b) => b.melhorRanking - a.melhorRanking);

      const topRankings = sortedData.slice(0, 10);

      const output = topRankings.map((ranking) => ({
        username: ranking.idUsuario.split("@")[0],
        email: ranking.idUsuario,
        ranking: ranking.melhorRanking,
      }));
      for (let i = 0; i < output.length; i++) {
        const div = document.createElement("div");
        div.classList.add("rankings");
        console.log(output.email, id);
        if (output[i].email === id) {
          div.classList.add("seuRanking");
        }
        div.innerText =
          output[i].username + ": " + output[i].ranking.toFixed(2);
        leaderboardElement.appendChild(div);
      }
    })
    .catch((error) => {
      console.error(error);
    });
}
// Função para exibir a questão atual
function exibirQuestao() {
  questaoElement.innerText = equacoes[questaoAtual];
  exibirRespostas();
  exibirErros();
}

// Função para exibir as respostas disponíveis
function exibirRespostas() {
  const respostaCorreta = respostas[questaoAtual];
  const respostasAleatorias = gerarRespostasAleatorias(respostaCorreta);

  const todasRespostas = [...respostasAleatorias, respostaCorreta];

  todasRespostas.sort(() => Math.random() - 0.5);

  respostasElement.innerHTML = "";
  todasRespostas.forEach((resposta) => {
    const botao = document.createElement("button");
    botao.innerText = resposta;
    botao.classList.add("option");
    botao.onclick = () => responder(resposta);
    respostasElement.appendChild(botao);
  });
  iniciarTemporizador();
}
// Função para exibir a quantidade de erros em tela
function exibirErros() {
  errosElement.innerText = erros;
}

// Função para gerar respostas aleatórias próximas à resposta correta
function gerarRespostasAleatorias(respostaCorreta) {
  const respostasAleatorias = [];

  // Gera duas respostas aleatórias
  for (let i = 0; i < 2; i++) {
    let respostaAleatoria = Math.round(
      respostaCorreta + Math.random() * 10 - 5
    );

    // Garante que a resposta aleatória não é igual à resposta correta
    while (respostaAleatoria === respostaCorreta) {
      respostaAleatoria = Math.round(respostaCorreta + Math.random() * 10 - 5);
    }
    respostasAleatorias.push(respostaAleatoria);
  }

  return respostasAleatorias;
}

// Função para iniciar o temporizador
function iniciarTemporizador() {
  var inicio = Date.now();
  var intervalo = setInterval(function () {
    var agora = Date.now();
    var duracao = agora - inicio;
    var segundos = Math.floor(duracao / 1000);
    var milissegundos = duracao % 1000;
    var tempoRestante = 20 - segundos - milissegundos / 1000;
    tempoDeRespostaAtual = (Date.now() - inicio) / 1000;
    if (tempoRestante < 0) {
      respostasCorretas.push(false);
      tempoDeResposta.push(tempoDeRespostaAtual);
      erros++;
      if (erros === 3) {
        FimDeJogo();
      }
      clearInterval(timer);
      if (erros < 3) {
        questaoAtual++;
        exibirQuestao();
      }
    }
    timerElement.innerText = `Tempo restante: ${tempoRestante.toFixed(2)}`;
  }, 10);
  timer = intervalo;
}

function responder(resposta) {
  clearInterval(timer);
  if (resposta === respostas[questaoAtual]) {
    respostasCorretas.push(true);
    tempoDeResposta.push(tempoDeRespostaAtual);
  } else {
    respostasCorretas.push(false);
    tempoDeResposta.push(tempoDeRespostaAtual);
    erros++;
    if (erros === 3) {
      FimDeJogo();
    }
  }
  if (questaoAtual === 49) {
    // o jogador completou todas as questões, enviar dados para a API
    FimDeJogo();
  }
  if (erros < 3) {
    questaoAtual++;
    exibirQuestao();
  }
}

// função para enviar dados para a API
async function enviarDados() {
  let idUsuario = id;
  const dados = {
    idUsuario,
    respostasCorretas,
    tempoDeResposta,
  };
  fetch("http://localhost:5183/api/UserInfo", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(dados),
  })
    .then((response) => {
      if (response.ok) {
        ReceberDadosDoJogador();
      } else {
        throw new Error("Erro ao enviar dados para a API.");
      }
    })
    .catch((error) => {
      console.error(error);
    });
}

// função que recebe dados de pontuação do jogador
async function ReceberDadosDoJogador() {
  var ref = setInterval(async () => {
    let json = await fetch(
      "http://localhost:5119/api/UserRanking/GetProcessamento?id=" + id
    );
    let resultado = await json.json();
    if (!resultado.processando) {
      fetch("http://localhost:5119/api/UserRanking/GetUserRankingId?id=" + id)
        .then((data) => {
          return data.json();
        })
        .then((data) => {
          melhorRankingElement.innerHTML = data.melhorRanking.toFixed(2);
          ultimoRankingElement.innerHTML = data.ultimoRanking.toFixed(2);
          qtdAcertosElement.innerHTML = data.qtdAcertos;
          melhorTempoElement.innerHTML = data.melhorTempo;

          fimDeJogoElement.classList.remove("active");
          popupJogo.classList.add("active");

          botaoElement.onclick = () => location.reload();
        })
        .catch((error) => {
          console.error(error);
        });
      clearInterval(ref);
    }
  }, 1000);
}

// Função para mostrar a tela de fim de jogo
function FimDeJogo() {
  fimDeJogoElement.classList.add("active");
  enviarDados();
}
// função para iniciar o jogo
buscarEquacoes();
getTopRankings();
