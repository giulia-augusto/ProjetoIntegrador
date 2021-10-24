//Formulario Municipio
$(document).ready(function(){
  listarEstado();
  grid();
 
});

function cadastrar() {
  let successo = validarFormulario2();

  let municipio = {
    nome : dados.nome.value,
    populacao: +dados.populacao.value,
    idEstado: +dados.estado.value,
    porte: dados.porte.value
  };
  console.log(municipio);
  if (successo) {
    $.ajax({
      type:'POST',
      url: 'https://localhost:5001/api/Municipio/Cadastrar',
      contentType: 'application/json; charset=utf-8',
      data: JSON.stringify(municipio),
      success: function(resposta) {
        alert(resposta);
      },
      error: function(erro, mensagem, excecao) {
        alert('Deu errado');
      }
    });
  }
}

function listarEstado()
{
  $.get('https://localhost:5001/api/Estado/Listar')
  .done(function(resposta) {
    for (i = 0; i < resposta.length; i++) {
      let option =  $('<option></option>').val(resposta[i].id).html(resposta[i].nome);
      $('#idEstado').append(option);
    }
  })
  .fail(function(erro, mensagem, excecao) { 
    alert('Deu errado.');
  });
} 

function grid() {
  $.get('https://localhost:5001/api/Municipio/Listar')
  .done(function(resposta)
  {
    for (i = 0; i< resposta.length; i++) {
      let linha = $('<tr></tr>');

      let celulaIdMunicipio = $('<td></td>').html(resposta[i].idMunicipio);
      linha.append(celulaIdMunicipio);

      let celulanome = $('<td></td>').html(resposta[i].nome);
      linha.append(celulanome);

      let celulaPopulacao = $('<td></td>').html(resposta[i].populacao);
      linha.append(celulaPopulacao);

      let celulaEstado = $('<td></td>').html(resposta[i].estado.nome);
      linha.append(celulaEstado);

      let celulaPorte = $('<td></td>').html(resposta[i].porte);
      linha.append(celulaPorte);

      let celulaAcoes = $('<td></td>');

      let botaoVizualizar =$('<button></button>').attr('type', 'button').addClass('botao-tabela').addClass('botao').html('vizualizar').attr('onclick', 'vizualizar(' + resposta[i].idMunicipio +')');
      celulaAcoes.append(botaoVizualizar);

      let botaoExcluir = $('<button></button>').attr('type', 'button').addClass('botao-tabela').addClass('botao').html('Deletar').attr('onclick', 'excluir(' + resposta[i].idMunicipio +')');
      celulaAcoes.append('  '); 
      celulaAcoes.append(botaoExcluir);

      linha.append(celulaAcoes);

      $('#gridMunicipios').append(linha);
    }
  })
  .fail(function(erro, mensagem, excecao){ 
      alert('Deu errado.');
  });
}

function vizualizar(idMunicipio) {
  $.get('https://localhost:5001/api/Municipio/Consultar/' + idMunicipio)
  .done(function(resposta){
    let vizualizacao = resposta.idMunicipio + '\n';
    vizualizacao += resposta.nome + '\n';
    vizualizacao += resposta.populacao + '\n';
    vizualizacao += resposta.estado.nome + '\n';
    vizualizacao += resposta.porte + '\n';
    
    alert(vizualizacao);
  })
  .fail(function(erro, mensagem, excecao){ 
    alert('Deu errado.');
  });
}

function excluir(IdMunicipio) {
  console.log(IdMunicipio);
  $.ajax({
    type: 'DELETE',
    url: 'https://localhost:5001/api/Municipio/Excluir',
    contentType: 'application/json; charset=utf-8',
    data: JSON.stringify(IdMunicipio),
    success: function(resposta) {
      alert(resposta);
    },
    error: function(erro, mensagem, excecao) {
      alert('Deu errado.');
    }
  })
}

function validarFormulario2(){
  var nomeEhValido = validarnome();
  var populacaoEhValido = validarPopulacao();
  var estadoEhValido = validarEstado();
  var porteEhValido = validarPorte();


  if (nomeEhValido == true && populacaoEhValido == true && estadoEhValido == true && 
    porteEhValido == true)
  {
    return true;
  }
  else {
    return false;
  }
}

function validarnome(){
  if ($('#idNome').val() == null || $('#idNome').val() == "") {
    $('#idNome').addClass('vermelho');
    alert("Preencha o campo: " + $('#idNomeMlabel').text().replace(':', ''));
    return false;
  } else {
    $('#idNome').removeClass('vermelho');
    return true;
  }
}

function validarPopulacao(){
  if ($('#idPopulacao').val() == null || $('#idPopulacao').val() == "") {
    $('#idPopulacao').addClass('vermelho');
    alert("Preencha o campo: " + $('#idpopulacaolabel').text().replace(':', ''));
    return false;
  } else {
    $('#idPopulacao').removeClass('vermelho');
    return true;
  }
}

function validarEstado(){
  if ($('#idEstado').val() == 0) {
    $('#idEstado').addClass('vermelho');
    alert("Preencha o campo: " + $('#idestadolabel').text().replace(':', ''));
    return false;
  } else {
    $('#idEstado').removeClass('vermelho');
    return true;
  }
}


function validarPorte(){
  for (let i = 0; i < $('.portec').length; i++) {
    if ($('.portec')[i].checked) {
      $('#idtdporte').removeClass('vermelho');
      return true;
    }
  }

  $('#idtdporte').addClass('vermelho');
  alert("Preencha o campo: " + $('#idportelabel').text().replace(':', ''));
  return false;
}

