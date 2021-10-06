using System;
using System.Collections.Generic;

namespace CadastroImoveis.Dtos
{
    public class MunicipioDTO
    {
        public int IdMunicipio { get; set; }
        public string Nome { get; set; }
        public int Populacao { get; set; }
        public string Porte { get; set; }
        public EstadoDTO Estado { get; set; }
    }
}
