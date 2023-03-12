using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using senai_spmedicalgroup_webapi.Domains;
using senai_spmedicalgroup_webapi.Interfaces;
using senai_spmedicalgroup_webapi.Repositories;
using senai_spmedicalgroup_webapi.ViewModels;
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
    public class LoginController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository { get; set; }

        public LoginController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel Login)
        {
            try
            {
                Usuario usuarioBuscado = _usuarioRepository.Login(Login.EmailUsuario, Login.SenhaUsuario);
                if (usuarioBuscado != null)
                {
                    var Claims = new[]
                    {
                    new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.EmailUsuario),
                    new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.IdUsuario.ToString()),
                    new Claim(ClaimTypes.Role, usuarioBuscado.IdTipoUsuario.ToString()),
                    new Claim("role", usuarioBuscado.IdTipoUsuario.ToString())
                };

                    var Key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("senaispmedicalgroupwebapi"));

                    var Creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

                    var meuToken = new JwtSecurityToken(
                            issuer: "SPMEDICALGROUP.webApi",
                            audience: "SPMEDICALGROUP.webApi",
                            claims: Claims,
                            expires: DateTime.Now.AddMinutes(40),
                            signingCredentials: Creds
                        );

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(meuToken)
                    });
                }

                return NotFound("Email ou Senha Inválido!");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }
    }
}
