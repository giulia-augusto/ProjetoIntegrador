using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CadastroImoveis.Models;

namespace CadastroImoveis.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MunicipioController : ControllerBase
    {
        private readonly ILogger<MunicipioController> _logger;
        private BDContexto _contexto;

        public MunicipioController(ILogger<MunicipioController> logger, BDContexto contexto)
        {
            _logger = logger;
            _contexto = contexto;
        }

        [HttpGet("{IdMunicipio}")]
        public Municipio Consultar(int IdMunicipio)
        {
            return _contexto.Municipio
                .FirstOrDefault(c => c.IdMunicipio == IdMunicipio);
            
        }

        [HttpGet]
        public List<Municipio> Listar()
        {
            return _contexto.Municipio
                .OrderBy(c => c.IdMunicipio)
                .Select(c => new Municipio 
                { 
                    IdMunicipio = c.IdMunicipio, 
                    Nome = c.Nome,
                    Populacao = c.Populacao,
                   // Estado = c.Estado, 
                    Porte = c.Porte 
                }).ToList();
        }

        [HttpGet]
        // [Route("municipio-imovel")]
        public List<Municipio> Listar2()
        {
            return _contexto.Municipio
                .OrderBy(c => c.IdMunicipio)
                .Select(c => new Municipio 
                { 
                    IdMunicipio = c.IdMunicipio, 
                    Nome = c.Nome
                }).ToList();
        }

        [HttpPost]
        public string Cadastrar([FromBody] Municipio novoMunicipio)
        {
            // _municipios.Add(novoMunicipio);
            return "Município cadastrado com sucesso!";
        }

        [HttpPut]
        public string Alterar([FromBody] Municipio municipio)
        {
            return "Município alterado com sucessso";
        }

        [HttpDelete]
        public string Deletar([FromBody] int IdMunicipio)
        {
            return "Município deletado com sucesso!";
        }

        
    }
}
