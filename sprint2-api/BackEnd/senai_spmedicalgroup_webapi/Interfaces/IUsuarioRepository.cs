using Microsoft.AspNetCore.Http;
using senai_spmedicalgroup_webapi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_spmedicalgroup_webapi.Interfaces
{
    interface IUsuarioRepository
    {
        List<Usuario> ListarUsuarios();
        void Cadastrar(Usuario novoUser);
        Usuario Login(string email, string senha);
        void Deletar(int id);
        void SalvarPerfilBD(IFormFile foto, short id);
        string ConsultarPerfilBD(short id);
        Usuario BuscarPorId(int id);
        void Atualizar(int id, Usuario userAtt);
    }
}
