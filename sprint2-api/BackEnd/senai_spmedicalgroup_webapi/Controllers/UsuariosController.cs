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
using System.Threading.Tasks;

namespace senai_spmedicalgroup_webapi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository { get; set; }
        public string JwtRegisteredClaimTypes { get; private set; }

        public UsuariosController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        //Início CRUD
        [HttpGet]
        [Authorize(Roles = "1")]
        public IActionResult Listar()
        {
            if (_usuarioRepository.ListarUsuarios() == null)
            {
                return NotFound(new
                {
                    Mensagem = "Não há nenhum usuario cadastrado ainda"
                });
            }
            
            return Ok(_usuarioRepository.ListarUsuarios());           
        }

        [Authorize(Roles = "1")]
        [HttpPost]
        public IActionResult Cadastrar(Usuario novoUser)
        {
            try
            {
                if (novoUser == null)
                {
                    return BadRequest(new
                    {
                        Mensagem = "É necessário informar todos os dados"
                    });
                }
                _usuarioRepository.Cadastrar(novoUser);
                return StatusCode(201, new
                {
                    Mensagem = "O usuario informado foi cadastrado!",
                    novoUser
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }

        [Authorize(Roles = "1")]
        [HttpPut("att/{id}")]
        public IActionResult Atualizar(int id, Usuario userAtt)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Informe um ID válido"
                    });
                }

                if (_usuarioRepository.BuscarPorId(id) == null)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Não há nenhum usuário com este ID"
                    });
                }
                if (userAtt == null)
                {
                    return BadRequest(new
                    {
                        Mensagem = "É necessário informar todos os dados!"
                    });
                }
                _usuarioRepository.Atualizar(id, userAtt);
                return StatusCode(200, new
                {
                    Mensagem = "O usuario informado foi atualizado!",
                    userAtt
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
                       
        }

        [Authorize(Roles = "1")]
        [HttpDelete("delete/{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                if (id <= 0 )
                {
                    return BadRequest(new
                    {
                        Mensagem = "Informe um ID válido"
                    });
                }

                if (_usuarioRepository.BuscarPorId(id) == null)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Não há nenhum usuário com este ID"
                    });
                }

                _usuarioRepository.Deletar(id);

                return StatusCode(200, new
                {
                    Mensagem = "O usuario informado foi deletado!"
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }

        //Fim CRUD

        [Authorize(Roles = "1")]
        [HttpPost("imagem/bd/{idUsuario}")]
        public IActionResult postBD(IFormFile arquivo, short idUsuario)
        {
            try
            {
                if (arquivo == null)
                {
                    return BadRequest(new { mensagem = "É necessario enviar uma foto .png" });
                }
                if (arquivo.Length > 5000)
                {
                    return BadRequest(new { mensagem = "O tamanho máximo da imagem é de 5mb" });
                }

                string extensao = arquivo.FileName.Split('.').Last();

                if (extensao != "png" || extensao != "jpg")
                {
                    return BadRequest(new { mensagem = "Apenas arquivos .png ou .jpg são permitidos" });
                }

                

                _usuarioRepository.SalvarPerfilBD(arquivo, idUsuario);

                return Ok();
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }

            
        }
        [Authorize(Roles = "1")]
        [HttpGet("imagem/bd/{idUsuario}")]
        public IActionResult getBd(short idUsuario)
        {
            try
            {
                string base64 = _usuarioRepository.ConsultarPerfilBD(idUsuario);
                return Ok(base64);
            }
            catch (Exception erro)
            {

                return BadRequest(erro.Message);
            }
        }

        [Authorize(Roles = "1")]
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            try
            {
                Usuario userBuscado = _usuarioRepository.BuscarPorId(id);

                if (id <= 0)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Informe um ID válido!"
                    });
                }

                if (userBuscado == null)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Não há nenhum usuário com o ID informado!"
                    });
                }

                return StatusCode(201, new
                {
                    Mensagem = "Um usuário foi encontrado!",
                    userBuscado
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }
    }
}
