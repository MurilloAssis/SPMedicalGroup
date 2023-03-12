using System;
using System.Collections.Generic;

#nullable disable

namespace senai_spmedicalgroup_webapi.Domains
{
    public partial class Medico
    {
        public Medico()
        {
            Consulta = new HashSet<Consultum>();
        }

        public short IdMedico { get; set; }
        public short? IdUsuario { get; set; }
        public short? IdEspecializacao { get; set; }
        public short? IdInstituicao { get; set; }
        public string Crm { get; set; }

        public virtual Especializacao IdEspecializacaoNavigation { get; set; }
        public virtual Instituicao IdInstituicaoNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
        public virtual ICollection<Consultum> Consulta { get; set; }
    }
}
