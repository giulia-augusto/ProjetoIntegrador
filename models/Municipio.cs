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
        public int IdEstado { get; set; }
        public string Porte { get; set; }
        public bool? Ativo { get; set; }

        public virtual Estado IdEstadoNavigation { get; set; }
        public virtual ICollection<Imovel> Imovel { get; set; }
    }
}
