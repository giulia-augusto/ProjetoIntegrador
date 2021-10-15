using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CadastroImoveis.Models;
using CadastroImoveis.Dtos;

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
        public List<ImovelDTO> Listar()
        {
            return _contexto.Imovel
                .Include(c => c.IdMunicipioNavigation)
                .OrderBy(c => c.CodImovel)
                .Select(c => new ImovelDTO 
                { 
                    CodImovel = c.CodImovel,
                    Proprietario = c.Proprietario,
                    Ano = c.Ano,
                    DataAquisicao = c.DataAquisicao,
                    Tipo = c.Tipo,
                    Municipio = new MunicipioDTO
                    { 
                        IdMunicipio = c.IdMunicipioNavigation.IdMunicipio, 
                        Nome = c.IdMunicipioNavigation.Nome
                    } 
                }).ToList();
        }

        [HttpGet("{codImovel}")]
        public ImovelDTO Consultar([FromRoute] int codImovel)
        {
            var imovel =  _contexto.Imovel
                .Include(c => c.IdMunicipioNavigation)
                .FirstOrDefault(c => c.CodImovel == codImovel);
            if (imovel == null)
            {
                return null;
            }
            else
            {
                return new ImovelDTO 
                { 
                    CodImovel = imovel.CodImovel,
                    Proprietario = imovel.Proprietario,
                    Ano = imovel.Ano,
                    DataAquisicao = imovel.DataAquisicao,
                    Tipo = imovel.Tipo,
                    Municipio = new MunicipioDTO
                    { 
                        IdMunicipio = imovel.IdMunicipioNavigation.IdMunicipio, 
                        Nome = imovel.IdMunicipioNavigation.Nome
                    } 
                };
            }    
        }

        [HttpPost]
        public string Cadastrar([FromBody] ImovelDTO novoImovel)
        {
             //_contexto.Add(novoImovel);
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
