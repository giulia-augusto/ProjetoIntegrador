//Formulario Imovel
$(document).ready(function(){
  listarMunicipio();
  grid();
});

function cadastrar() {
  let successo = validarFormulario();

  let imovel = {
    proprietario : dados.proprietario.value,
    ano: dados.ano.value,
    data: dados.data.value,
    municipio: {
      id: parseInt(dados.municipio.value)
    },
    tipo: toString(dados.tipo.value)
  };

  if (successo) {
  
    $.ajax({
      type:'POST',
      url: 'https://localhost:5001/api/Imovel/Cadastrar',
      contentType: 'application/json; charset=utf-8',
      data: JSON.stringify(imovel),
      success: function(resposta) {
        alert(resposta);
      },
      error: function(erro, mensagem, excecao) {
        alert('deu errado');
      }
    });
  }
}

function listarMunicipio()
{
  $.get('https://localhost:5001/api/Municipio/Listar2')
  .done(function(resposta) {
    for (i = 0; i < resposta.length; i++) {
      let option =  $('<option></option>').val(resposta[i].idMunicipio).html(resposta[i].nomeMunicipio);
      $('#idMunicipio').append(option);
    }
  })
  .fail(function(erro, mensagem, excecao) { 
    alert('Deu errado.');
  });
}  

function grid() {
  $.get('https://localhost:5001/api/Imovel/Listar')
  .done(function(resposta)
  {
    for (i = 0; i< resposta.length; i++) {
      let linha = $('<tr></tr>');

      let celulaId = $('<td></td>').html(resposta[i].idImovel);
      linha.append(celulaId);

      let celulaProprietario = $('<td></td>').html(resposta[i].proprietario);
      linha.append(celulaProprietario);

      let celulaAno = $('<td></td>').html(resposta[i].ano);
      linha.append(celulaAno);

      let celulaMunicipio = $('<td></td>').html(resposta[i].municipio.nomeMunicipio);
      linha.append(celulaMunicipio);

      let celulaTipo = $('<td></td>').html(resposta[i].tipo);
      linha.append(celulaTipo);

      let celulaAcoes = $('<td></td>');

      let botaoVizualizar =$('<button></button>').attr('type', 'button').addClass('botao-tabela').addClass('botao').html('vizualizar').attr('onclick', 'vizualizar(' + resposta[i].idImovel +')');
      celulaAcoes.append(botaoVizualizar);

      let botaoExcluir = $('<button></button>').attr('type', 'button').addClass('botao-tabela').addClass('botao').html('Deletar').attr('onclick', 'excluir(' + resposta[i].idImovel +')');
      celulaAcoes.append('  '); 
      celulaAcoes.append(botaoExcluir);

      linha.append(celulaAcoes);

      $('#grid').append(linha);
    }
  })
  .fail(function(erro, mensagem, excecao){ 
      alert('Deu errado.');
  });
}

function vizualizar(id) {
  $.get('https://localhost:5001/api/Imovel/Consultar?idImovel=' + id)
  .done(function(resposta){
    let vizualizacao = resposta.idImovel + '\n';
    vizualizacao += resposta.proprietario + '\n';
    vizualizacao += resposta.ano + '\n';
    vizualizacao += resposta.municipio.nomeMunicipio + '\n';
    vizualizacao += resposta.tipo + '\n';
    
    alert(vizualizacao);
  })
  .fail(function(erro, mensagem, excecao){ 
    alert('Deu errado.');
  });
}

function excluir(id) {
  $.ajax({
    type: 'DELETE',
    url: 'https://localhost:5001/api/Imovel/Deletar',
    contentType: 'application/json; charset=utf-8',
    data: JSON.stringify(id),
    success: function(resposta) {
      alert(resposta);
    },
    error: function(erro, mensagem, excecao) {
      alert('Deu errado.');
    }
  })
}
  

function validarFormulario(){
  var nomeEhValido = validarNome();
  var anoEhValido = validarAno();
  var dataEhValido = validarData();
  var municipioEhValido = validarMunicipio();
  var diferencialEhValido = validarDiferencial();
  var tipoEhValido = validarTipo();

  if (nomeEhValido == true && anoEhValido == true && dataEhValido == true && 
      municipioEhValido == true && diferencialEhValido == true && tipoEhValido == true) {
    return true;
  }
  else {
    return false;
  }
}

function validarNome(){
  if ($('#idNome').val() == null || $('#idNome').val() == "") {
    $('#idNome').addClass('vermelho');
    alert("Preencha o campo: " + $('#idNomeLabel').text().replace(':', ''));
    return false;
  } else {
    $('#idNome').removeClass('vermelho');
    return true;
  }
}

function validarAno(){
  if ($('#idAno').val() == null || $('#idAno').val() == "") {
    $('#idAno').addClass('vermelho');
    alert("Preencha o campo: " + $('#idAnoLabel').text().replace(':', ''));
    return false;
  } else {
    $('#idAno').removeClass('vermelho');
    return true;
  }
}

function validarData(){
  if ($('#idData').val() == null || $('#idData').val() == ""){
    $('#idData').addClass('vermelho');
    alert("Preencha o campo: " + $('#idDataLabel').text().replace(':', ''));
    return false;
  } else {
    $('#idData').removeClass('vermelho');
    return true;
  }
}

function validarMunicipio(){
  if ($('#idMunicipio').val() == 0){
    $('#idMunicipio').addClass('vermelho');
    alert("Preencha o campo: " + $('#idMunicipioLabel').text().replace(':', ''));
    return false;
  } else {
    $('#idMunicipio').removeClass('vermelho');
    return true;
  }
}

function validarDiferencial(){
  for (let i = 0; i < $('.diferencial').length; i++) {
    if ($('.diferencial')[i].checked) {
      $('#idtddiferencial').removeClass('vermelho');
      return true;
    }
  }

  $('#idtddiferencial').addClass('vermelho');
  alert("Preencha o campo: " + $('#idDiferencialLabel').text().replace(':', ''));
  return false;
}

function validarTipo(){
  for (let i = 0; i < $('.tipoImovel').length; i++) {
    if ($('.tipoImovel')[i].checked) {
      $('#idtdtipo').removeClass('vermelho');
      return true;
    }
  }

  $('#idtdtipo').addClass('vermelho');
  alert("Preencha o campo: " + $('#idTipoLabel').text().replace(':', ''));
  return false;
}
