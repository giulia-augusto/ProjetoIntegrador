//Formulario Imovel
$(document).ready(function(){
  listarMunicipio();
  grid();

});

function cadastrar() {
  let successo = validarFormulario();

  let imovel = {
    proprietario : dados.proprietario.value,
    ano: +dados.ano.value,
    dataAquisicao: dados.data.value,
    idMunicipio: +dados.municipio.value,
    tipo:dados.tipo.value
  };
  
  if (successo) {
    salvar();
    
  }
}

function listarMunicipio()
{
  $.get('https://localhost:5001/api/Municipio/Listar2')
  .done(function(resposta) {
    for (i = 0; i < resposta.length; i++) {
      let option =  $('<option></option>').val(resposta[i].idMunicipio).html(resposta[i].nome);
      $('#idMunicipio').append(option);
    }
  })
  .fail(function(erro, mensagem, excecao) { 
    alert('Deu errado.');
  });
}  

function grid() {
  $.get('https://localhost:5001/api/Imovel/Listar')
  .done(function(resposta) {
    $('#grid').children().remove();
    for (i = 0; i< resposta.length; i++) {
      let linha = $('<tr></tr>');

      let celulaId = $('<td></td>').html(resposta[i].codImovel);
      linha.append(celulaId);

      let celulaProprietario = $('<td></td>').html(resposta[i].proprietario);
      linha.append(celulaProprietario);

      let celulaAno = $('<td></td>').html(resposta[i].ano);
      linha.append(celulaAno);

      let celulaMunicipio = $('<td></td>').html(resposta[i].municipio.nome);
      linha.append(celulaMunicipio);

      let celulaTipo = $('<td></td>').html(resposta[i].tipo);
      linha.append(celulaTipo);

      let celulaAcoes = $('<td></td>');

      let botaoVizualizar =$('<button></button>').attr('type', 'button').addClass('botao-tabela').addClass('botao').html('Visualizar').attr('onclick', 'vizualizar(' + resposta[i].codImovel +')');
      celulaAcoes.append(botaoVizualizar);

      let botaoExcluir = $('<button></button>').attr('type', 'button').addClass('botao-tabela').addClass('botao').html('Deletar').attr('onclick', 'excluir(' + resposta[i].codImovel +')');
      celulaAcoes.append('  '); 
      celulaAcoes.append(botaoExcluir);

      let botaoAlterar = $('<button></button>').attr('type', 'button').addClass('botao-tabela').addClass('botao').html('Alterar').attr('onclick', 'editar(' + resposta[i].codImovel +')');
      celulaAcoes.append('  '); 
      celulaAcoes.append(botaoAlterar);

      linha.append(celulaAcoes);

      $('#grid').append(linha);
    }
  })
  .fail(function(erro, mensagem, excecao){ 
      alert('Deu errado.');
  });
}

function vizualizar(id) {
  $.get('https://localhost:5001/api/Imovel/Consultar/' + id)
  .done(function(resposta){
    let vizualizacao = resposta.codImovel + '\n';
    vizualizacao += resposta.proprietario + '\n';
    vizualizacao += resposta.ano + '\n';
    vizualizacao += resposta.municipio.nome + '\n';
    vizualizacao += resposta.tipo + '\n';
    
    alert(vizualizacao);
  })
  .fail(function(erro, mensagem, excecao){ 
    alert('Deu errado.');
  });
}

function excluir(codImovel) {
  $.ajax({
    type: 'DELETE',
    url: 'https://localhost:5001/api/Imovel/Excluir',
    contentType: 'application/json; charset=utf-8',
    data: JSON.stringify(codImovel),
    success: function(resposta) {
      grid();
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

function editar(codImovel){
  $.get('https://localhost:5001/api/Imovel/Visualizar?codImovel='+codImovel)
      .done(function(resposta) { 
          $('#codImovel').val(resposta.codImovel);
          $('#idNome').val(resposta.proprietario);
          $('#idAno').val(resposta.ano);
          $('#idData').val(resposta.dataAquisicao.substr(0, 10));
          $('input:radio[name="tipo"]').filter(`[value="${resposta.tipo === 'Novo' ? 1 : 2}"]`).attr('checked', true);
          $('#idMunicipio').val(resposta.municipio.idMunicipio);
          $('#salvar span').text('EDITAR');

          document.body.scrollTop = 0;
          document.documentElement.scrollTop = 0;
      })
      .fail(function(erro, mensagem, excecao) { 
          alert(mensagem + ': ' + excecao);
      });
}

function salvar(){

  var id;
  var url;
  var metodo;
 
  if ($('#salvar span')[0].textContent === 'EDITAR'){
    console.log('alterar')
      id = +$('#codImovel').val();
      metodo = 'PUT';
      url = 'https://localhost:5001/api/Imovel/Alterar';
  }
  else{
    console.log('cadastrar')

      id = 0;
      metodo = 'POST';
      url = 'https://localhost:5001/api/Imovel/Cadastrar';
  }
  
  let imovel = {
    
    codImovel: id,
    proprietario : dados.proprietario.value,
    ano: +dados.ano.value,
    dataAquisicao: dados.data.value,
    idMunicipio: +dados.municipio.value,
    tipo:dados.tipo.value
  };

    console.log(imovel);
  

  $.ajax({
      type: metodo,
      url: url,
      contentType: "application/json; charset=utf-8",
      data: JSON.stringify(imovel),
      success: function(resposta) { 
        limparFormulario()
        grid();
        alert(resposta);
      },
      error: function(erro, mensagem, excecao) { 
          alert(mensagem + ': ' + excecao);
      }
  });
}

function limparFormulario(){
  $('#codImovel').val(0);
  $('#idNome').val('');
  $('#idAno').val('');
  $('#idData').val('');
  $('#idMunicipio').val(0);
  $('#salvar span').text('Salvar');
  }
