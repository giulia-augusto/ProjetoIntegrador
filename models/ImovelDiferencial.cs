using System;
using System.Collections.Generic;

namespace CadastroImoveis.Models
{
    public partial class ImovelDiferencial
    {
        public int CodImovel { get; set; }
        public int IdDiferencial { get; set; }

        public virtual Imovel CodImovelNavigation { get; set; }
        public virtual Diferencial IdDiferencialNavigation { get; set; }
    }
}
