using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace senai_spmedicalgroup_webapi.Domains
{
    public partial class Usuario
    {
        public Usuario()
        {
            Medicos = new HashSet<Medico>();
            Pacientes = new HashSet<Paciente>();
        }

        public short IdUsuario { get; set; }
        public byte? IdTipoUsuario { get; set; }

        [Required]
        public string NomeUsuario { get; set; }
        [Required]
        public string EmailUsuario { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Este campo requer no mínimo 3 e, no máximo, 60 caracteres")]
        [MaxLength(60, ErrorMessage = "Este campo requer no mínimo 3 e, no máximo, 60 caracteres")]
        public string SenhaUsuario { get; set; }

        public virtual TipoUsuario IdTipoUsuarioNavigation { get; set; }
        public virtual ImagemUsuario ImagemUsuario { get; set; }
        public virtual ICollection<Medico> Medicos { get; set; }
        public virtual ICollection<Paciente> Pacientes { get; set; }
    }
}
