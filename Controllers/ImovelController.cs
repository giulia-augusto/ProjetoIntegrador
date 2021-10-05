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
    public class ImovelController : ControllerBase
    {
        private readonly ILogger<ImovelController> _logger;
        private BDContexto _contexto;

        public ImovelController(ILogger<ImovelController> logger, BDContexto contexto)
        {
            _logger = logger;
            _contexto = contexto;
        }

        [HttpGet]
        public List<Imovel> Listar()
        {
            return _contexto.Imovel
                .Include(c => c.IdMunicipioNavigation)
                .OrderBy(c => c.CodImovel)
                .Select(c => new Imovel 
                { 
                    CodImovel = c.CodImovel,
                    Proprietario = c.Proprietario,
                    Ano = c.Ano,
                    DataAquisicao = c.DataAquisicao,
                    Tipo = c.Tipo,
                    IdMunicipioNavigation = new Municipio 
                    { 
                        IdMunicipio = c.IdMunicipioNavigation.IdMunicipio, 
                        Nome = c.IdMunicipioNavigation.Nome,
                        Populacao = c.IdMunicipioNavigation.Populacao,
                      //  Estado = c.IdMunicipioNavigation.Estado.IdEstadoNavigation.Nome, 
                        Porte = c.IdMunicipioNavigation.Porte 
                    } 
                }).ToList();
        }

        [HttpGet("{CodImovel}")]
        public Imovel Consultar(int CodImovel)
        {
            return _contexto.Imovel
                .FirstOrDefault(c => c.CodImovel == CodImovel);

        }

        [HttpPost]
        public string Cadastrar([FromBody] Imovel novoImovel)
        {
            // _imoveis.Add(novoImovel);
            return "Imóvel cadastrado com sucesso!";
        }

        [HttpPut]
        public string Alterar([FromBody] Imovel imovel)
        {
            return "Imóvel alterado com sucesso!";
        }

        [HttpDelete]
        public string Deletar([FromBody] int CodImovel) 
        {
            return "Imóvel deletado com sucesso!";
        }
    }
}
