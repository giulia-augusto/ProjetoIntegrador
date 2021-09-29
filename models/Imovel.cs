using System;
using System.Collections.Generic;

namespace CadastroImoveis.Models
{
    public partial class Imovel
    {
        public Imovel()
        {
            ImovelDiferencial = new HashSet<ImovelDiferencial>();
        }

        public int CodImovel { get; set; }
        public string Proprietario { get; set; }
        public short Ano { get; set; }
        public DateTime DataAquisicao { get; set; }
        public int IdMunicipio { get; set; }
        public string Tipo { get; set; }

        public virtual Municipio IdMunicipioNavigation { get; set; }
        public virtual ICollection<ImovelDiferencial> ImovelDiferencial { get; set; }
    }
}
