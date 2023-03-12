using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai_spmedicalgroup_webapi.Domains;
using senai_spmedicalgroup_webapi.Interfaces;
using senai_spmedicalgroup_webapi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_spmedicalgroup_webapi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class MedicosController : ControllerBase
    {
        private IMedicoRepository _medicoRepository { get; set; }

        public MedicosController()
        {
            _medicoRepository = new MedicoRepository();
        }

        [HttpGet]
        public IActionResult Listar()
        {
            List<Medico> lista = _medicoRepository.ListarTodos();

            return Ok(lista);
        }

        [HttpPost]
        public IActionResult Cadastrar(Medico novoMedico)
        {
            try
            {
                if (novoMedico.Crm == null || novoMedico.IdEspecializacao <= 0 || novoMedico.IdInstituicao <= 0 || novoMedico.IdUsuario <= 0)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Os dados são inválidos!"
                    });
                }

                _medicoRepository.Cadastrar(novoMedico);

                return Ok(new
                {
                    Mensagem = "Um novo médico foi cadastrado",
                    novoMedico
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

    }
}
