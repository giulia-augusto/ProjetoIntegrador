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
    public class ImovelController : ControllerBase
    {
        private readonly ILogger<ImovelController> _logger;
        private List<Imovel> _imoveis = new List<Imovel>
        {
            new Imovel { IdImovel = 1, Proprietario = "Mariana", Ano = "1995", Municipio = new Municipio { IdMunicipio = 1, NomeMunicipio = "Barbacena" }, Tipo = "Usado" },
            new Imovel { IdImovel = 2, Proprietario = "Henrique", Ano = "2020", Municipio = new Municipio { IdMunicipio = 2, NomeMunicipio = "Varginha" }, Tipo = "Novo" },
            new Imovel { IdImovel = 3, Proprietario = "Glícia", Ano = "2017", Municipio = new Municipio { IdMunicipio = 3, NomeMunicipio = "Rio De Janeiro" }, Tipo = "Novo" },
            new Imovel { IdImovel = 4, Proprietario = "Emília", Ano = "1977", Municipio = new Municipio { IdMunicipio = 4, NomeMunicipio = "Ubatuba" }, Tipo = "Usado" },
            new Imovel { IdImovel = 5, Proprietario = "Everton", Ano = "2010", Municipio = new Municipio { IdMunicipio = 5, NomeMunicipio = "Divinopólis" }, Tipo = "Usado" },
            new Imovel { IdImovel = 6, Proprietario = "Júliana", Ano = "2019", Municipio = new Municipio { IdMunicipio = 6, NomeMunicipio = "Campo Grande" }, Tipo = "Novo" },
            new Imovel { IdImovel = 7, Proprietario = "Marcos Vinícios", Ano = "2021", Municipio = new Municipio { IdMunicipio = 7, NomeMunicipio = "São Paulo" }, Tipo = "Novo" },
            new Imovel { IdImovel = 8, Proprietario = "Sandrielly", Ano = "2008", Municipio = new Municipio { IdMunicipio = 8, NomeMunicipio = "Pernambuco" }, Tipo = "Usado" },
            new Imovel { IdImovel = 9, Proprietario = "Mário Santos", Ano = "1997", Municipio = new Municipio { IdMunicipio = 9, NomeMunicipio = "Belo Horizonte" }, Tipo = "Usado" },
            new Imovel { IdImovel = 10, Proprietario = "Carlos Eduardo", Ano = "2003", Municipio = new Municipio { IdMunicipio = 10, NomeMunicipio = "Bahia" }, Tipo = "Usado" }
        };

        public ImovelController(ILogger<ImovelController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<Imovel> Listar()
        {
            return _imoveis;
        }

        [HttpGet]
        public Imovel Consultar(int idImovel)
        {
            for (int i = 0; i < _imoveis.Count; i++)
            {
                if (_imoveis[i].IdImovel == idImovel) 
                {
                    return _imoveis[i];
                }
            }

            return null;
        }

        [HttpPost]
        public string Cadastrar([FromBody] Imovel novoImovel)
        {
            _imoveis.Add(novoImovel);
            return "Imóvel cadastrado com sucesso!";
        }

        [HttpPut]
        public string Alterar([FromBody] Imovel imovel)
        {
            return "Imóvel alterado com sucesso!";
        }

        [HttpDelete]
        public string Deletar([FromBody] int idImovel) 
        {
            return "Imóvel deletado com sucesso!";
        }
    }
}
