using Microsoft.AspNetCore.Mvc;
using WebApplication3.Datos;
using WebApplication3.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("api/prueba/v1")]
    public class DestinatarioController : Controller
    {
        private readonly IMapeoDatosDestinatrio _repo;

        public DestinatarioController(IMapeoDatosDestinatrio repo)
        {
            _repo = repo;
        }


        [HttpPost("consultar")]
        public async Task<ActionResult<List<Destinatario>>> Consultar(
              [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER,
            [FromBody] ConsultarDestinatarioQuery query
            )
        {
            var data = await _repo.Consulta(query);
            return Ok(data);
        }
        [HttpGet("listar")]
        public async Task<ActionResult<List<Destinatario>>>Listar(
              [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER,
            [FromQuery] ListarDestinatariosQuery query
            )
        {
            var data = await _repo.listar(query);
            return Ok(data);
        }
        [HttpPost("crear")]
        public async Task<ActionResult<int>> crear(
             [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER,
           [FromBody] Destinatario query
           )
        {
            var data = await _repo.crear(query);
            return Ok(data);
        }
        [HttpPut("actualiza")]
        public async Task<ActionResult<int>> Actualizar(
             [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER,
           [FromBody] Destinatario query
           )
        {
            var data = await _repo.Actualizar( query);
            return Ok(data);
        }

        [HttpPut("eliminar")]
        public async Task<ActionResult<string>>eliminar([FromHeader] string REFRENCES_ID, [FromHeader ] string CONSUMER,
             [FromQuery] string idDestinatario
            )
        {
            var data = await _repo.eliminar(idDestinatario);
            return Ok(data);
        }
        
    }


}
