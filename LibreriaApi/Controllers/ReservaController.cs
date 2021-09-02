using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using LibreriaApi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace LibreriaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private readonly LibreriaDbContext _ctx;
        private readonly IWebHostEnvironment _env;


        public ReservaController(LibreriaDbContext iTextMeDbContext, IWebHostEnvironment env) {
            _ctx = iTextMeDbContext;
            _env = env;
        }




        [HttpGet]
        public List<Reserva> Get()
        {
            return _ctx.Reserva.ToList();
        }




        [HttpPost("ReservarLibro")]
        public IActionResult ReservarLibro([FromBody] Reserva reserva)
        {
            try {
                var parameters = new[] {
                    new SqlParameter("@clienteId", SqlDbType.Int) {Value = reserva.clienteId },
                    new SqlParameter("@libroId", SqlDbType.Int) {Value = reserva.libroId },
                    new SqlParameter("@estado", SqlDbType.VarChar) {Value = reserva.estado }
                };

                int result = _ctx.SqlExec("LIBRERIA.RESERVAR_LIBRO", parameters, _env);
                var mensaje = result > 0 ? reserva.estado.Equals("A") ?  "Libro alquilado!" : "Libro comprado!" : "Error al realizar operacion";

                return Ok(new { message = mensaje });
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }




        [HttpPut("DevolverLibro/{libroId}/{clienteId}")]
        public IActionResult DevolverLibro(int libroId, int clienteId)
        {
            try {
                var parameters = new[] {
                    new SqlParameter("@clienteId", SqlDbType.Int) {Value = clienteId },
                    new SqlParameter("@libroId", SqlDbType.Int) {Value = libroId }
                };

                int result = _ctx.SqlExec("LIBRERIA.DEVOLVER_LIBRO", parameters, _env);
                var mensaje = result > 0 ? "Libro devuelto" : "No hay libros alquilados";

                return Ok(new { message = mensaje });
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
