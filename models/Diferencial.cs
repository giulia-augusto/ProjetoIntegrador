using System;
using System.Collections.Generic;

namespace CadastroImoveis.Models
{
    public partial class Diferencial
    {
        public Diferencial()
        {
            ImovelDiferencial = new HashSet<ImovelDiferencial>();
        }

        public int IdDiferencial { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<ImovelDiferencial> ImovelDiferencial { get; set; }
    }
}
