using System;

namespace CadastroImoveis.Models 
{
    public class Imovel
    {
        public int IdImovel { get; set; }
        public string Proprietario { get; set; }
        public string Ano { get; set; }
        public DateTime Data { get; set; }
        public Municipio Municipio { get; set; }
        public string Diferencial { get; set; }
        public string Tipo { get; set; }
    }
}
