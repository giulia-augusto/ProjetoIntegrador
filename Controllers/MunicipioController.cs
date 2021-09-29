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

        [HttpGet]
        public Municipio Consultar(int IdMunicipio)
        {
            // for (int i = 0; i< _municipios.Count; i++)
            // {
            //     if (_municipios[i].IdMunicipio == IdMunicipio)
            //     {
            //         return _municipios[i];
            //     }
            // }
            return null;

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
                    Estado = c.Estado, 
                    Porte = c.Porte 
                }).ToList();
        }

        [HttpGet]
        // [Route("municipio-imovel")]
        public List<Municipio> Listar2()
        {
        //    List<Municipio> municipios = new List<Municipio>
        //     {
        //         new Municipio { IdMunicipio = 1, Nome = "Belo Horizonte" },
        //         new Municipio { IdMunicipio = 2, Nome = "Gramado" },
        //         new Municipio { IdMunicipio = 3, Nome = "Palmas" },
        //         new Municipio { IdMunicipio = 4, Nome = "Formosa" },
        //         new Municipio { IdMunicipio = 5, Nome = "Ilhéus" },
        //         new Municipio { IdMunicipio = 6, Nome = "Varginha" },
        //         new Municipio { IdMunicipio = 7, Nome = "Barbacena" },
        //         new Municipio { IdMunicipio = 8, Nome = "Ubatuba" },
        //         new Municipio { IdMunicipio = 9, Nome = "São Paulo" },
        //         new Municipio { IdMunicipio = 10, Nome = "Bahia" }
        //     };

        //     return municipios;
            return null;
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
