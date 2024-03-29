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

        [HttpGet("{idMunicipio}")]
        public MunicipioDTO Consultar([FromRoute] int idMunicipio)
        {
            var municipio = _contexto.Municipio
                .Include(c => c.IdEstadoNavigation)
                .FirstOrDefault(c => c.IdMunicipio == idMunicipio && c.Ativo.Value);
            
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
                .Where(c => c.Ativo.Value)
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
                .Where(c => c.Ativo.Value)
                .OrderBy(c => c.IdMunicipio)
                .Select(c => new Municipio 
                { 
                    IdMunicipio = c.IdMunicipio, 
                    Nome = c.Nome
                }).ToList();
        }

        [HttpPost]
        public string Cadastrar([FromBody] Municipio dados)
        {
            _contexto.Add(dados);
            _contexto.SaveChanges();
            return "Município cadastrado com sucesso!";
        }

        [HttpPut]
        public string Alterar([FromBody] Municipio dados)
        {
            dados.Ativo = true;
            _contexto.Update(dados);
            _contexto.SaveChanges();
            return "Município alterado com sucessso";
        }


        [HttpDelete]
        public string Excluir([FromBody] int IdMunicipio) 
        {
            Municipio dados = _contexto.Municipio.FirstOrDefault(p => p.IdMunicipio == IdMunicipio);
             if (dados == null)
            {
                return "Não foi encontrado Município para o ID informado!";
            }
            else
            {
                // _contexto.Remove(dados);
                dados.Ativo = false;
                _contexto.Update(dados);
                _contexto.SaveChanges();
            
                return "Município deletado com sucesso!";
            }
        }

        [HttpGet]
        public MunicipioDTO Visualizar(int idMunicipio)
        {
            return _contexto.Municipio
                .Include(p => p.IdEstadoNavigation)
                .Where(c => c.Ativo.Value)
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
                })
                .FirstOrDefault(p => p.IdMunicipio == idMunicipio);
        }
    }   
}
