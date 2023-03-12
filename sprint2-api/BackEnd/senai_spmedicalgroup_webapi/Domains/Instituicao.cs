using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace senai_spmedicalgroup_webapi.Domains
{
    public partial class Instituicao
    {
        public Instituicao()
        {
            Medicos = new HashSet<Medico>();
        }

        public short IdInstituicao { get; set; }
        public string NomeFantasia { get; set; }
        public string Endereco { get; set; }
        public string RazaoSocial { get; set; }

        [MinLength(14, ErrorMessage ="O cnpj deve conter 14 dígitos"), MaxLength(14, ErrorMessage = "O cnpj deve conter 14 dígitos")]
        public string Cnpj { get; set; }

        public virtual ICollection<Medico> Medicos { get; set; }
    }
}
