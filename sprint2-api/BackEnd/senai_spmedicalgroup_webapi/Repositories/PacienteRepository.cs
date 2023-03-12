using Microsoft.EntityFrameworkCore;
using senai_spmedicalgroup_webapi.Contexts;
using senai_spmedicalgroup_webapi.Domains;
using senai_spmedicalgroup_webapi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_spmedicalgroup_webapi.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {
        SpMedicalGroupContext ctx = new SpMedicalGroupContext();
        public void Atualizar(int id, Paciente attPaciente)
        {
            Paciente pacienteBuscado = BuscarPorId(id);

            if (attPaciente.Cpf != null || attPaciente.Rg != null || attPaciente.Telefone != null || attPaciente.EnderecoPaciente != null || attPaciente.DataNasc < DateTime.Now)
            {
                pacienteBuscado.Rg = pacienteBuscado.Rg;
                pacienteBuscado.IdUsuario = pacienteBuscado.IdUsuario;
                pacienteBuscado.Cpf = pacienteBuscado.Cpf;
                pacienteBuscado.Telefone = attPaciente.Telefone;
                pacienteBuscado.EnderecoPaciente = attPaciente.EnderecoPaciente;
                pacienteBuscado.DataNasc = attPaciente.DataNasc;

                ctx.Pacientes.Update(pacienteBuscado);

                ctx.SaveChanges();
            }
        }

        public Paciente BuscarPorId(int id)
        {
            return ctx.Pacientes.FirstOrDefault(p => p.IdPaciente == id);
        }

        public void Cadastrar(Paciente novoPaciente)
        {
            ctx.Pacientes.Add(novoPaciente);

            ctx.SaveChanges();
        }

        public void Deletar(int id)
        {
            ctx.Pacientes.Remove(BuscarPorId(id));

            ctx.SaveChanges();
        }

        public List<Paciente> ListarTodos()
        {
            return ctx.Pacientes
                        .AsNoTracking()
                        .Select(p => new Paciente()
                        {
                            IdPaciente = p.IdPaciente,
                            Rg = p.Rg,
                            EnderecoPaciente = p.EnderecoPaciente,
                            DataNasc = p.DataNasc,
                            Telefone = p.Telefone,
                            IdUsuarioNavigation = new Usuario()
                            {
                                NomeUsuario = p.IdUsuarioNavigation.NomeUsuario,
                                EmailUsuario = p.IdUsuarioNavigation.EmailUsuario,                              
                            }
                        })
                        .ToList();
                
        }
    }
}
