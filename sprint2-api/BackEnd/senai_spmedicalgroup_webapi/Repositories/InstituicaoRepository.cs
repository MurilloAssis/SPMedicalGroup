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
    public class InstituicaoRepository : IInstituicaoRepository
    {
        SpMedicalGroupContext ctx = new SpMedicalGroupContext();

        public void Atualizar(int id, Instituicao attClinica)
        {
            Instituicao clinicaBuscada = BuscarClinica(id);
            if (attClinica.Endereco != null || attClinica.Cnpj != null || attClinica.NomeFantasia != null || attClinica.RazaoSocial != null)
            {
                clinicaBuscada.Endereco = attClinica.Endereco;
                clinicaBuscada.Cnpj = attClinica.Cnpj;
                clinicaBuscada.NomeFantasia = attClinica.NomeFantasia;
                clinicaBuscada.RazaoSocial = attClinica.RazaoSocial;

                ctx.Instituicaos.Update(clinicaBuscada);

                ctx.SaveChanges();
            }
        }

        public Instituicao BuscarClinica(int id)
        {
            return ctx.Instituicaos.FirstOrDefault(c => c.IdInstituicao == id);
        }

        public void CadastrarClinica(Instituicao novaClinica)
        {
            ctx.Instituicaos.Add(novaClinica);

            ctx.SaveChanges();
        }

        public void Deletar(int id)
        {
            ctx.Instituicaos.Remove(BuscarClinica(id));

            ctx.SaveChanges();
        }

        public List<Instituicao> ListarTodas()
        {
            return ctx.Instituicaos
                    .AsNoTracking()
                    .Select(c => new Instituicao
                    {
                        Cnpj = c.Cnpj,
                        Endereco = c.Endereco,
                        NomeFantasia = c.NomeFantasia,
                        Medicos = ctx.Medicos.Where(m => m.IdInstituicao == c.IdInstituicao).ToList()
                    })                    
                    .ToList();
        }
    }
}
