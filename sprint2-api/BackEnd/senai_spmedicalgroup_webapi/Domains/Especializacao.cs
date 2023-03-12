using System;
using System.Collections.Generic;

#nullable disable

namespace senai_spmedicalgroup_webapi.Domains
{
    public partial class Especializacao
    {
        public Especializacao()
        {
            Medicos = new HashSet<Medico>();
        }

        public short IdEspecializacao { get; set; }
        public string TipoEspecializacao { get; set; }

        public virtual ICollection<Medico> Medicos { get; set; }
    }
}
