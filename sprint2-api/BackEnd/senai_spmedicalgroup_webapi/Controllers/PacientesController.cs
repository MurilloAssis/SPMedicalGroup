using Microsoft.AspNetCore.Authorization;
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
    public class PacientesController : ControllerBase
    {
        private IPacienteRepository _pacienteRepository { get; set; }

        public PacientesController()
        {
            _pacienteRepository = new PacienteRepository();
        }

        [Authorize(Roles = "1")]
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                List<Paciente> lista = _pacienteRepository.ListarTodos();

                if (lista == null)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Não há nenhum paciente cadastrado no sistema"
                    });
                }

                return Ok(lista);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "1")]
        [HttpPost]
        public IActionResult Cadastrar(Paciente novoPaciente)
        {
            try
            {
                if (novoPaciente.Cpf == null || novoPaciente.DataNasc > DateTime.Now || novoPaciente.Rg == null || novoPaciente.Telefone == null || novoPaciente.EnderecoPaciente == null || novoPaciente.IdUsuario == null)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Os dados estão inválidos!"
                    });
                }

                _pacienteRepository.Cadastrar(novoPaciente);

                return Ok(new
                {
                    Mensagem = "O Paciente foi cadastrado com sucesso!",
                    novoPaciente
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "1")]
        [HttpPut("{id:int}")]
        public IActionResult Atualizar(int id, Paciente attPaciente)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Insira um ID válido!"
                    });
                }

                if (_pacienteRepository.BuscarPorId(id) == null)
                {
                    return NotFound(new
                    {
                        Mensagem = "Não há nenhum paciente com o ID informado!"
                    });
                }

                if (attPaciente.Cpf == null || attPaciente.DataNasc > DateTime.Now || attPaciente.Rg == null || attPaciente.Telefone == null || attPaciente.EnderecoPaciente == null || attPaciente.IdUsuario == null)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Os dados informados são inválidos!"
                    });
                }

                _pacienteRepository.Atualizar(id, attPaciente);
                return Ok(new
                {
                    Mensagem = "O Paciente foi atualizado com sucesso!",
                    attPaciente
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "1")]
        [HttpDelete("{id:int}")]
        public IActionResult Deletar(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new
                {
                    Mensagem = "Insira um ID válido!"
                });
            }

            if (_pacienteRepository.BuscarPorId(id) == null)
            {
                return NotFound(new
                {
                    Mensagem = "Não há nenhum paciente com o ID informado!"
                });
            }

            _pacienteRepository.Deletar(id);
            return Ok(new
            {
                Mensagem = "O Paciente foi deletado com sucesso!",
                
            });
        }

        [Authorize(Roles = "1")]
        [HttpGet("{id:int}")]
        public IActionResult BuscarPorId(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new
                {
                    Mensagem = "Insira um ID válido!"
                });
            }

            if (_pacienteRepository.BuscarPorId(id) == null)
            {
                return NotFound(new
                {
                    Mensagem = "Não há nenhum paciente com o ID informado!"
                });
            }

            Paciente pacienteEncontrado = _pacienteRepository.BuscarPorId(id);
            return Ok(new
            {
                Mensagem = "O Paciente foi encontrado com sucesso!",
                pacienteEncontrado
            });
        }
    }
}
