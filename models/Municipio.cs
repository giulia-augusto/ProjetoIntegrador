using System;

namespace CadastroImoveis.Models 
{
    public class Municipio
    {
        public int IdMunicipio { get; set; }
        public string NomeMunicipio { get; set; }
        public string Populacao { get; set; }
        public Estado Estado { get; set; }
        public string Porte { get; set; }
    }
}
