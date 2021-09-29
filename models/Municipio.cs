using System;
using System.Collections.Generic;

namespace CadastroImoveis.Models
{
    public partial class Municipio
    {
        public Municipio()
        {
            Imovel = new HashSet<Imovel>();
        }

        public int IdMunicipio { get; set; }
        public string Nome { get; set; }
        public int Populacao { get; set; }
        public string Estado { get; set; }
        public string Porte { get; set; }

        public virtual ICollection<Imovel> Imovel { get; set; }
    }
}
