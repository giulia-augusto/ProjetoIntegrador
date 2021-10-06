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
        public MunicipioDTO Consultar(int IdMunicipio)
        {
            var municipio = _contexto.Municipio
                .Include(c => c.IdEstadoNavigation)
                .FirstOrDefault(c => c.IdMunicipio == IdMunicipio);
            
            if (municipio == null)
            {
                return null;
            }
            else
            {
                return new MunicipioDTO 
                { 
                    IdMunicipio = municipio.IdMunicipio, 
                    Nome = municipio.Nome,
                    Populacao = municipio.Populacao,
                    Porte = municipio.Porte,
                    Estado = new EstadoDTO
                    {
                        Id = municipio.IdEstadoNavigation.Id,
                        Nome = municipio.IdEstadoNavigation.Nome
                    }
                };
            }
        }

        [HttpGet]
        public List<MunicipioDTO> Listar()
        {
            return _contexto.Municipio
                .Include(c => c.IdEstadoNavigation)
                .OrderBy(c => c.IdMunicipio)
                .Select(c => new MunicipioDTO 
                { 
                    IdMunicipio = c.IdMunicipio, 
                    Nome = c.Nome,
                    Populacao = c.Populacao,
                    Porte = c.Porte,
                    Estado = new EstadoDTO
                    {
                        Id = c.IdEstadoNavigation.Id,
                        Nome = c.IdEstadoNavigation.Nome
                    }
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
