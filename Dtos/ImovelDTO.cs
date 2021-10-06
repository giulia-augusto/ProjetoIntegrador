using System;
using System.Collections.Generic;

namespace CadastroImoveis.Dtos
{
    public class ImovelDTO
    {
        public int CodImovel { get; set; }
        public string Proprietario { get; set; }
        public short Ano { get; set; }
        public DateTime DataAquisicao { get; set; }
        public string Tipo { get; set; }
        public MunicipioDTO Municipio { get; set; }
    }
}
