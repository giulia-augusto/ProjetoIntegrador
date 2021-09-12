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
    public class EstadoController : ControllerBase
    {
        private readonly ILogger<EstadoController> _logger;
        
        [HttpGet]
        public List<Estado> Listar()
        {
            List<Estado> _estados = new List<Estado>
            {
                new Estado { Id = 1, Nome = "Minas Gerais"},
                new Estado { Id = 2, Nome = "Rio Grande do Sul"},
                new Estado { Id = 3, Nome = "Tocantins" },
                new Estado { Id = 4, Nome = "São Paulo" },
                new Estado { Id = 5, Nome = "Espírito Santo" },
                new Estado { Id = 6, Nome = "Rio de Janeiro" },
                new Estado { Id = 7, Nome = "Pernambuco" },
                new Estado { Id = 8, Nome = "Bahia" }
            };

            return _estados;
        }

        public EstadoController(ILogger<EstadoController> logger)
        {
            _logger = logger;
        }
    
   }
}