using ApiMentopoker.Helpers;

using ApiMentopoker.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NugetMentopoker.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApiMentopoker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private RepositoryLogin repoLogin;
        private HelperOAuthToken helper;

        public LoginController(RepositoryLogin repoLogin, HelperOAuthToken helper)
        {
            this.repoLogin = repoLogin;
            this.helper = helper;
        }



        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> RegisterUsuario([FromBody] UsuarioRequest request)
        {
            string Email = request.Email;
            string Pass = request.Pass;
            string Nombre = request.Nombre;
            string Rol = request.Rol;

            await repoLogin.RegisterUsuarioAsync(Email, Pass, Nombre, Rol);
            return Ok();
        }


        //[HttpPost]
        //[Route("[action]")]
        //public IActionResult Login([FromBody] UsuarioRequest request)
        //{
        //    string Email = request.Email;
        //    string Pass = request.Pass;


        //    var usuario = repoLogin.LoginAsync(Email, Pass);
        //    if (usuario == null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        return Ok(usuario);
        //    }
        //}


        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> Login([FromBody] UsuarioRequest request)
        {
            string Email = request.Email;
            string Pass = request.Pass;


            UsuarioModel usuario =
                await repoLogin.LoginAsync(Email, Pass);
            UsuarioRequest usuarioCompleto = new UsuarioRequest
            {
                Usuario_id = usuario.Usuario_id,
                Email = usuario.Email,
                Nombre = usuario.Nombre,
                Rol = usuario.Rol,


            };

            if (usuario == null)
            {
                return Unauthorized();
            }
            else
            {
                //DEBEMOS CREAR UNAS CREDENCIALES DENTRO
                //DEL TOKEN
                SigningCredentials credentials =
                    new SigningCredentials(this.helper.GetKeyToken()
                    , SecurityAlgorithms.HmacSha256);
                string jsonEmpleado =
                    JsonConvert.SerializeObject(usuario);
                Claim[] informacion = new[]
                {
                    new Claim("UserData", jsonEmpleado)
                };

                //EL TOKEN SE GENERA CON UNA CLASE Y DEBEMOS INDICAR
                //LOS DATOS QUE CONFORMAN DICHO TOKEN
                JwtSecurityToken token =
                    new JwtSecurityToken(
                        claims: informacion,
                        issuer: this.helper.Issuer,
                        audience: this.helper.Audience,
                        signingCredentials: credentials,
                        expires: DateTime.UtcNow.AddMinutes(30),
                        notBefore: DateTime.UtcNow
                        );

                string tokenString =
                    new JwtSecurityTokenHandler().WriteToken(token);
                usuarioCompleto.Token = tokenString;
               

                return Ok(new
                {
                    response = usuarioCompleto
                });
            }
        }



        [Authorize]
        [HttpGet]
        [Route("[action]")]
        public async Task<List<UsuarioModel>> GetUsuarios()
        {
            return await this.repoLogin.GetUsuariosAsync();
        }

        [Authorize]
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateUsuario([FromBody] UsuarioRequest request)
        {
            string Usuario_id = request.Usuario_id;
            string Email = request.Email;
            string Nombre = request.Nombre;
            string Rol = request.Rol;

            await repoLogin.UpdateUsuario(Usuario_id, Email, Nombre, Rol);
            return Ok();
        }

        [Authorize]
        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> DeleteUsuario([FromBody] UsuarioRequest request)
        {
            string Usuario_id = request.Usuario_id;
            await repoLogin.DeleteUsuario(Usuario_id);
            return Ok();
        }


       

    }
}
