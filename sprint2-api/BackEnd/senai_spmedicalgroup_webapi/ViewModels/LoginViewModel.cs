using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace senai_spmedicalgroup_webapi.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Informe o e-mail do usuário!")]
        public string EmailUsuario { get; set; }

        [Required(ErrorMessage = "Informe a senha do usuário!")]
        public string SenhaUsuario { get; set; }
    }
}
