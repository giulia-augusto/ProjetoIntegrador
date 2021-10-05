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
    public class EstadoController : ControllerBase
    {
        private readonly ILogger<EstadoController> _logger;
        private BDContexto _contexto;

        public EstadoController(ILogger<EstadoController> logger, BDContexto contexto)
        {
            _logger = logger;
            _contexto = contexto;
        }

        
        [HttpGet]
        public List<Estado> Listar()
        {
            return _contexto.Estado
                .OrderBy(c => c.Id)
                .Select(c => new Estado 
                { 
                    Id = c.Id, 
                    Nome = c.Nome
                }).ToList();
        }
    
   }
}