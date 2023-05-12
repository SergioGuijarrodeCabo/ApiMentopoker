using ApiMentopoker.Repositories;
using Microsoft.AspNetCore.Mvc;
using NugetMentopoker.Models;

namespace ApiMentopoker.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TablasController : ControllerBase
    {


        private RepositoryTablas repoTablas;


        public TablasController(RepositoryTablas repoTablas)
        {
         
            this.repoTablas = repoTablas;
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<ActionResult<List<Celda>>> GetTabla(int id)
        {
            return await this.repoTablas.GetTablaAsync(id);
        }

        //[HttpPost()]
        //public async Task<ActionResult> InsertarPartida(int[] ids_Jugadas, int[] ids_Rondas, double[] ganancias_Rondas, double[] cantidades_Rondas,
        //    string[] cell_ids_Jugadas, int[] table_ids_Jugadas, double[] cantidades_Jugadas,
        //    Boolean[] seguimiento_jugadas, double dinero_inicial, double dinero_actual, string comentario, string usuario_id)
        //{
        //     await this.repoTablas.insertPartidaAsync(ids_Jugadas, ids_Rondas, ganancias_Rondas, cantidades_Rondas, cell_ids_Jugadas, table_ids_Jugadas, cantidades_Jugadas,
        //       seguimiento_jugadas, dinero_inicial, dinero_actual, comentario, usuario_id);
        //}

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> InsertarPartida([FromBody] PartidaRequest partidaRequest)
        {
            int[] ids_Jugadas = partidaRequest.IdsJugadas;
            int[] ids_Rondas = partidaRequest.IdsRondas;
            double[] ganancias_Rondas = partidaRequest.GananciasRondas;
            double[] cantidades_Rondas = partidaRequest.CantidadesRondas;
            string[] cell_ids_Jugadas = partidaRequest.CellIdsJugadas;
            int[] table_ids_Jugadas = partidaRequest.TableIdsJugadas;
            double[] cantidades_Jugadas = partidaRequest.CantidadesJugadas;
            bool[] seguimiento_jugadas = partidaRequest.SeguimientoJugadas;
            double dinero_inicial = partidaRequest.DineroInicial;
            double dinero_actual = partidaRequest.DineroActual;
            string comentario = partidaRequest.Comentario;
            string usuario_id = partidaRequest.UsuarioId;

            await this.repoTablas.insertPartidaAsync(ids_Jugadas, ids_Rondas, ganancias_Rondas, cantidades_Rondas,
                cell_ids_Jugadas, table_ids_Jugadas, cantidades_Jugadas, seguimiento_jugadas,
                dinero_inicial, dinero_actual, comentario, usuario_id);

            return Ok();
        }

    }
}
