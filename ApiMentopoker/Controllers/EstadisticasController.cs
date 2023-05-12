using ApiMentopoker.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NugetMentopoker.Models;

namespace ApiMentopoker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadisticasController : ControllerBase
    {
        private RepositoryEstadisticas repoStats;

        public EstadisticasController(RepositoryEstadisticas repoStats)
        {
            this.repoStats = repoStats;
        }


        [HttpGet]
        public async Task<ActionResult<List<PartidaModel>>> GetAllPartidas()
        {
            return await this.repoStats.GetAllPartidasAsync();
        }

        [HttpGet("{Partida_id}")]
        public async Task<ActionResult<PartidaModel>> FindPartida(string Partida_id)
        {
            return await this.repoStats.FindPartidaAsync(Partida_id);
        }

        [HttpPost("{Usuario_id}")]
        public async Task BorrarPartidas(string Usuario_id)
        {
             await this.repoStats.BorrarPartidasAsync(Usuario_id);
        }

        [HttpPut]
        public async Task<ActionResult> UpdatePartida([FromBody] PartidaModel partida)
        {
            await this.repoStats.UpdatePartidaAsync(partida);

            return Ok();
        }


    }
}
