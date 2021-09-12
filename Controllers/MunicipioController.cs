using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CadastroImoveis.Models;

namespace CadastroImoveis.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MunicipioController : ControllerBase
    {
        private readonly ILogger<MunicipioController> _logger;

        public MunicipioController(ILogger<MunicipioController> logger)
        {
            _logger = logger;
        }

        public List<Municipio> _municipios = new List<Municipio>
        {
            new Municipio {IdMunicipio = 1, NomeMunicipio = "Barbacena", Populacao = "138.204", Estado = new Estado { Id = 1, Nome = "Minas Gerais"}, Porte = "Médio"},
            new Municipio {IdMunicipio = 2, NomeMunicipio = "Porto Alegre", Populacao = "1.488.252", Estado = new Estado { Id = 2, Nome = "Rio Grande do Sul"}, Porte = "Metrópole"},
            new Municipio {IdMunicipio = 3, NomeMunicipio = "Palmas", Populacao = "306.296", Estado = new Estado { Id = 3, Nome = "Tocantins" }, Porte = "Grande"},
            new Municipio {IdMunicipio = 4, NomeMunicipio = "Campinas", Populacao = "1.213.792", Estado = new Estado { Id = 4, Nome = "São Paulo" }, Porte = "Metrópole"},
            new Municipio {IdMunicipio = 5, NomeMunicipio = "Guarapari", Populacao = "126.701", Estado = new Estado { Id = 5, Nome = "Espírito Santo" }, Porte = "Grande"},
            new Municipio {IdMunicipio = 6, NomeMunicipio = "Trindade", Populacao = "30.816", Estado = new Estado { Id = 6, Nome = "Rio de Janeiro" }, Porte = "Médio"},
            new Municipio {IdMunicipio = 7, NomeMunicipio = "Fernando de Noronha", Populacao = "3.061", Estado = new Estado { Id = 7, Nome = "Pernambuco" }, Porte = "Pequeno"},
            new Municipio {IdMunicipio = 8, NomeMunicipio = "Juazeiro", Populacao = "214.748", Estado = new Estado { Id = 8, Nome = "Bahia" }, Porte = "Grande"}
        };

        [HttpGet]
        public Municipio Consultar(int IdMunicipio)
        {
            for (int i = 0; i< _municipios.Count; i++)
            {
                if (_municipios[i].IdMunicipio == IdMunicipio)
                {
                    return _municipios[i];
                }
            }
            return null;

        }

        [HttpGet]
        public List<Municipio> Listar()
        {
            return _municipios;
        }

        [HttpGet]
        // [Route("municipio-imovel")]
        public List<Municipio> Listar2()
        {
           List<Municipio> municipios = new List<Municipio>
            {
                new Municipio { IdMunicipio = 1, NomeMunicipio = "Belo Horizonte" },
                new Municipio { IdMunicipio = 2, NomeMunicipio = "Gramado" },
                new Municipio { IdMunicipio = 3, NomeMunicipio = "Palmas" },
                new Municipio { IdMunicipio = 4, NomeMunicipio = "Formosa" },
                new Municipio { IdMunicipio = 5, NomeMunicipio = "Ilhéus" },
                new Municipio { IdMunicipio = 6, NomeMunicipio = "Varginha" },
                new Municipio { IdMunicipio = 7, NomeMunicipio = "Barbacena" },
                new Municipio { IdMunicipio = 8, NomeMunicipio = "Ubatuba" },
                new Municipio { IdMunicipio = 9, NomeMunicipio = "São Paulo" },
                new Municipio { IdMunicipio = 10, NomeMunicipio = "Bahia" }
            };

            return municipios;
        }

        [HttpPost]
        public string Cadastrar([FromBody] Municipio novoMunicipio)
        {
            _municipios.Add(novoMunicipio);
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
