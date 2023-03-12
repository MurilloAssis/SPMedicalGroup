using Microsoft.AspNetCore.Http;
using senai_spmedicalgroup_webapi.Contexts;
using senai_spmedicalgroup_webapi.Domains;
using senai_spmedicalgroup_webapi.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace senai_spmedicalgroup_webapi.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        SpMedicalGroupContext ctx = new SpMedicalGroupContext();
       
        //Início CRUD
        public void Cadastrar(Usuario novoUser)
        {
            ctx.Usuarios.Add(novoUser);

            ctx.SaveChanges();
        }

        public List<Usuario> ListarUsuarios()
        {
            return ctx.Usuarios
                .Select(u => new Usuario
                {
                    EmailUsuario = u.EmailUsuario,
                    NomeUsuario = u.NomeUsuario,
                    IdTipoUsuarioNavigation = new TipoUsuario()
                    {
                        TituloTipoUsuario = u.IdTipoUsuarioNavigation.TituloTipoUsuario
                    }
                })
                .ToList();
        }

        public void Deletar(int id)
        {
            ctx.Usuarios.Remove(BuscarPorId(id));

            ctx.SaveChanges();
        }

        public void Atualizar(int id, Usuario userAtt)
        {
            Usuario userBuscado = BuscarPorId(id);

            if (userAtt.NomeUsuario != null || userAtt.SenhaUsuario != null || userAtt.EmailUsuario != null)
            {
                userBuscado.NomeUsuario = userAtt.NomeUsuario;
                userBuscado.EmailUsuario = userAtt.EmailUsuario;
                userBuscado.SenhaUsuario = userAtt.SenhaUsuario;

                ctx.Usuarios.Update(userBuscado);

                ctx.SaveChanges();
            }

        }

        //Fim CRUD

        public Usuario Login(string email, string senha)
        {
            return ctx.Usuarios.FirstOrDefault(e => e.EmailUsuario == email && e.SenhaUsuario == senha);
        }

        public void SalvarPerfilBD(IFormFile foto, short id)
        {
            ImagemUsuario imagemUsuario = new ImagemUsuario();

            using (var ms = new MemoryStream())
            {
                foto.CopyTo(ms);

                imagemUsuario.Binario = ms.ToArray();

                imagemUsuario.NomeArquivo = foto.FileName;
                imagemUsuario.MimeType = foto.FileName.Split('.').Last();
                imagemUsuario.IdUsuario = id;
            }

            ImagemUsuario imagemExistente = new ImagemUsuario();
            imagemExistente = ctx.ImagemUsuarios.FirstOrDefault(i => i.IdUsuario == id);

            if (imagemExistente != null)
            {
                imagemExistente.Binario = imagemUsuario.Binario;
                imagemExistente.NomeArquivo = imagemUsuario.NomeArquivo;
                imagemExistente.MimeType = imagemUsuario.MimeType;
                imagemExistente.IdUsuario = id;

                ctx.ImagemUsuarios.Update(imagemExistente);
            }
            else
            {
                ctx.ImagemUsuarios.Add(imagemUsuario);
            }

            ctx.SaveChanges();
        }

        public string ConsultarPerfilBD(short id)
        {
            ImagemUsuario imagemUsuario = new ImagemUsuario();
            imagemUsuario = ctx.ImagemUsuarios.FirstOrDefault(i => i.IdUsuario == id);

            if (imagemUsuario != null)
            {
                return Convert.ToBase64String(imagemUsuario.Binario);
            }
            return null;
        }

        public Usuario BuscarPorId(int id)
        {
            return ctx.Usuarios.FirstOrDefault(u => u.IdUsuario == id);           
        }

        
    }
}
