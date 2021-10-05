using System;
using System.Collections.Generic;

namespace CadastroImoveis.Models
{
    public partial class Estado
    {
        public Estado()
        {
            Municipio = new HashSet<Municipio>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<Municipio> Municipio { get; set; }
    }
}
