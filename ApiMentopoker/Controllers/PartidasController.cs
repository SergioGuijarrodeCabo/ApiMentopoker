﻿using ApiMentopoker.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NugetMentopoker.Models;

namespace ApiMentopoker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartidasController : ControllerBase
    {
        private RepositoryEstadisticas repoStats;

        public PartidasController(RepositoryEstadisticas repoStats)
        {
            this.repoStats = repoStats;
        }

        [Authorize]
        [HttpPost]
        [Route("[action]/{Usuario_id}")]
        public async Task BorrarPartidasUsuario(string Usuario_id)
        {
            await this.repoStats.BorrarPartidasAsync(Usuario_id);
        }

        [Authorize]
        [HttpPost]
        [Route("[action]/{Partida_id}")]
        public async Task BorrarPartidasId(string Partida_id)
        {
            await this.repoStats.DeletePartidaAsync(Partida_id);
        }

        [Authorize]
        [HttpPut]
        [Route("[action]")]
        public async Task<ActionResult> UpdatePartida(PartidaModel partida)
        {
            await this.repoStats.UpdatePartidaAsync(partida);

            return Ok();
        }
    }
}
