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
  
  if (successo) {
    salvar();
  
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
  .done(function(resposta) {
    $('#gridMunicipios').children().remove();
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

      let botaoVizualizar =$('<button></button>').attr('type', 'button').addClass('botao-tabela').addClass('botao').html('Visualizar').attr('onclick', 'vizualizar(' + resposta[i].idMunicipio +')');
      celulaAcoes.append(botaoVizualizar);

      let botaoExcluir = $('<button></button>').attr('type', 'button').addClass('botao-tabela').addClass('botao').html('Deletar').attr('onclick', 'excluir(' + resposta[i].idMunicipio +')');
      celulaAcoes.append('  '); 
      celulaAcoes.append(botaoExcluir);

      let botaoAlterar = $('<button></button>').attr('type', 'button').addClass('botao-tabela').addClass('botao').html('Alterar').attr('onclick', 'editar(' + resposta[i].idMunicipio +')');
      celulaAcoes.append('  '); 
      celulaAcoes.append(botaoAlterar);

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

function excluir(idMunicipio) {
  $.ajax({
    type: 'DELETE',
    url: 'https://localhost:5001/api/Municipio/Excluir',
    contentType: 'application/json; charset=utf-8',
    data: JSON.stringify(idMunicipio),
    success: function(resposta) {
      grid();
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

function obterPorte(porte){
  switch (porte){
    case 'Metrópole': return 1;
    case 'Grande': return 2;
    case 'Médio': return 3;
    case 'Pequeno': return 4;
    default: return 0;
  }
}

function editar(idMunicipio){
  $.get('https://localhost:5001/api/Municipio/Visualizar?idMunicipio='+idMunicipio)
      .done(function(resposta) { 
        console.log(resposta);
          $('#idMunicipio').val(resposta.idMunicipio);
          $('#idNome').val(resposta.nome);
          $('#idPopulacao').val(resposta.populacao);
          $('input:radio[name="porte"]').filter(`[value="${obterPorte(resposta.porte)}"]`).attr('checked', true);
          $('#idEstado').val(resposta.estado.id);
          $('#salvar span').text('Editar');

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
  if ($('#salvar span')[0].textContent === 'Editar'){
      id = +$('#idMunicipio').val();
      metodo = 'PUT';
      url = 'https://localhost:5001/api/Municipio/Alterar';
  }
  else{
      id = 0;
      metodo = 'POST';
      url = 'https://localhost:5001/api/Municipio/Cadastrar';
  }
 
let municipio = {
    idMunicipio : id,
    nome : dados.nome.value,
    populacao: +dados.populacao.value,
    idEstado: +dados.estado.value,
    porte: dados.porte.value
  };
  

  $.ajax({
      type: metodo,
      url: url,
      contentType: "application/json; charset=utf-8",
      data: JSON.stringify(municipio),
      success: function(resposta) { 
        limparFormulario();  
        grid();
        alert(resposta);
      },
      error: function(erro, mensagem, excecao) { 
          alert(mensagem + ': ' + excecao);
      }
  });
}

function limparFormulario(){
  $('#idMunicipio').val('');
  $('#idNome').val('');
  $('#idPopulacao').val('');
  $('#idEstado').val(0);
  $('#salvar span').text('Salvar');
}



