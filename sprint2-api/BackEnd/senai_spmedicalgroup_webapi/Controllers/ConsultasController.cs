using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai_spmedicalgroup_webapi.Domains;
using senai_spmedicalgroup_webapi.Interfaces;
using senai_spmedicalgroup_webapi.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace senai_spmedicalgroup_webapi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultasController : ControllerBase
    {
        private IConsultaRepository _consultaRepository { get; set; }

        public ConsultasController()
        {
            _consultaRepository = new ConsultaRepository();
        }

        [Authorize(Roles = "1")]
        [HttpGet]
        public IActionResult ListarTodos()
        {
            try
            {
                List<Consultum> listaConsultas = _consultaRepository.ListarTodas();
                if (listaConsultas.Count == 0)
                {
                    return StatusCode(404, new
                    {
                        Mensagem = "Não há nenhuma consulta cadastrada no sistema!"
                    });
                }
                return Ok(listaConsultas);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }

        [Authorize(Roles = "1")]
        [HttpPost]
        public IActionResult Cadastrar(Consultum novaConsulta)
        {
            try
            {

                if (novaConsulta.IdMedico == null || novaConsulta.IdPaciente == null || novaConsulta.DataConsulta < DateTime.Now)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Os dados informados são inválidos ou estão vazios!"
                    });
                }
                _consultaRepository.CadastrarConsulta(novaConsulta);

                return StatusCode(201, new
                {
                    Mensagem = "A consulta foi cadastrada!",
                    novaConsulta
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }

        [Authorize(Roles = "1")]
        [HttpPatch("Cancelar/{id:int}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Informe um ID válido!"
                    });
                }

                if (_consultaRepository.BuscarPorId(id) == null)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Não há nenhuma consulta com o ID informado!"
                    });
                }
                _consultaRepository.CancelarConsulta(id);

                return StatusCode(204, new
                {
                    Mensagem = "A consulta foi cancelada!"
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [Authorize(Roles = "2, 3")]
        [HttpGet("Lista/Minhas")]
        public IActionResult ListarMinhas()
        {
            try
            {

                int id = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value);
                int idTipo = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Role).Value);
                List<Consultum> listaConsulta = _consultaRepository.ListarMinhasConsultas(id, idTipo);


                if (idTipo == 2)
                {
                    return Ok(
                    
                        listaConsulta
                    );
                }
                if (idTipo == 3)
                {
                    return Ok(
                    
                        listaConsulta
                    );
                }
                return null;
                
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [Authorize(Roles = "3")]
        [HttpPatch("AlterarDescricao/{id}")]
        public IActionResult AlterarDescricao(Consultum consultaAtt, int id)
        {
            try
            {
                Consultum consultaBuscada = _consultaRepository.BuscarPorId(id);
                int idMedico = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value);
                if (consultaAtt.DescricaoConsulta == null)
                {
                    return BadRequest(new
                    {
                        Mensagem = "É necessário informar a descrição!"
                    });
                }
                

                

                if (id <= 0)
                {
                    return BadRequest(new
                    {
                        Mensagem = "É necessário informar um ID válido!"
                    });
                }

                if (_consultaRepository.BuscarPorId(id) == null)
                {
                    return NotFound(new
                    {
                        Mensagem = "Não há nenhuma consulta com o ID informado!"
                    });
                }

                if (consultaBuscada.IdMedico != idMedico)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Somente o médico titular da consulta pode fazer alterações na descrição!"
                    });
                }
                _consultaRepository.AlterarDescricao(consultaAtt.DescricaoConsulta, id);
                return StatusCode(200, new
                {
                    Mensagem = "A descrição da consulta foi alterada com sucesso!",
                    consultaBuscada
                }) ;
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "1")]
        [HttpDelete("Remover/{id:int}")]
        public IActionResult RemoverConsultaSistema(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new
                    {
                        Mensagem = "É necessário informar um ID válido!"
                    });
                }

                if (_consultaRepository.BuscarPorId(id) == null)
                {
                    return NotFound(new
                    {
                        Mensagem = "Não há nenhuma consulta com o ID informado!"
                    });
                }

                _consultaRepository.RemoverConsultaSistema(id);

                return StatusCode(200, new
                {
                    Mensagem = "A consulta informada foi removida do sistema!"
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
