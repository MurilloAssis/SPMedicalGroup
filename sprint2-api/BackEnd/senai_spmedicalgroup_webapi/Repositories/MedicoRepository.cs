using senai_spmedicalgroup_webapi.Contexts;
using senai_spmedicalgroup_webapi.Domains;
using senai_spmedicalgroup_webapi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_spmedicalgroup_webapi.Repositories
{
    public class MedicoRepository : IMedicoRepository
    {
        SpMedicalGroupContext ctx = new SpMedicalGroupContext();
        public List<Medico> ListarTodos()
        {
            return ctx.Medicos.Select(u => new Medico()
            {
                IdUsuario = u.IdUsuario,
                IdMedico = u.IdMedico,
                IdUsuarioNavigation = new Usuario()
                {
                    NomeUsuario = u.IdUsuarioNavigation.NomeUsuario
                }
            }).ToList();
                 
        }

        public void Cadastrar(Medico novoMedico)
        {
            ctx.Medicos.Add(novoMedico);

            ctx.SaveChanges();
        }
    }
}
