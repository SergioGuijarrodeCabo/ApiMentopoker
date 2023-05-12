﻿using ApiMentopoker.Repositories;
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
        [Route("[action]")]
        public async Task<ActionResult<List<PartidaModel>>> GetAllPartidas()
        {
            return await this.repoStats.GetAllPartidasAsync();
        }

        [HttpGet]
        [Route("[action]/{Partida_id}")]
        public async Task<ActionResult<PartidaModel>> FindPartida(string Partida_id)
        {
            return await this.repoStats.FindPartidaAsync(Partida_id);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<ConjuntoPartidasUsuario>> GetPartidas([FromBody] PartidasRequest request)
        {
            int usuarioId = request.UsuarioId;
     
            DateTime? fechaInicio = request.FechaInicio;
            DateTime? fechaFinal = request.FechaFinal;


            ConjuntoPartidasUsuario result = await this.repoStats.GetPartidasAsync(
                usuarioId, fechaInicio, fechaFinal);

            return Ok(result);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<EstadisticasPartidas>> GetEstadisticasPartidas([FromBody] PartidasRequest request)
        {
            int usuarioId = request.UsuarioId;
         
            DateTime? fechaInicio = request.FechaInicio;
            DateTime? fechaFinal = request.FechaFinal;
           

            EstadisticasPartidas result = await this.repoStats.GetEstadisticasPartidasAsync(
                usuarioId, fechaInicio, fechaFinal);

            return Ok(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<EstadisticasJugadas>> GetEstadisticasJugadas([FromBody] PartidasRequest request)
        {
            int usuarioId = request.UsuarioId;

            DateTime? fechaInicio = request.FechaInicio;
            DateTime? fechaFinal = request.FechaFinal;
            string cellId = request.CellId;
            int? condicion = request.Condicion;
            double? cantidadJugada = request.CantidadJugada;

            EstadisticasJugadas result = await this.repoStats.GetEstadisticasJugadasAsync(
                usuarioId, fechaInicio, fechaFinal, cellId, condicion, cantidadJugada);

            return Ok(result);
        }




    }
}
