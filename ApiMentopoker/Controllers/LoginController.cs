using ApiMentopoker.Models;
using ApiMentopoker.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NugetMentopoker.Models;

namespace ApiMentopoker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private RepositoryLogin repoLogin;

        public LoginController(RepositoryLogin repoLogin)
        {
            this.repoLogin = repoLogin;
        }



        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> RegisterUsuario([FromBody] UsuarioRequest request)
        {
            string Email = request.Email;
            string Pass = request.Pass;
            string Nombre = request.Nombre;
            string Rol = request.Rol;

            await repoLogin.RegisterUsuario(Email, Pass, Nombre, Rol);
            return Ok();
        }


        [HttpPost]
        [Route("[action]")]
        public IActionResult Login([FromBody] UsuarioRequest request)
        {
            string Email = request.Email;
            string Pass = request.Pass;


            var usuario = repoLogin.Login(Email, Pass);
            if (usuario == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(usuario);
            }
        }

        [HttpGet]
        [Route("[action]")]
        public List<UsuarioModel> GetUsuarios()
        {
            return repoLogin.GetUsuarios();
        }

       
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
