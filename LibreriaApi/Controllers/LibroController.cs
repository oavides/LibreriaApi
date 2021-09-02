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
    public class LibroController : ControllerBase
    {

        private readonly LibreriaDbContext _ctx;
        private readonly IWebHostEnvironment _env;


        public LibroController(LibreriaDbContext iTextMeDbContext, IWebHostEnvironment env) {
            _ctx = iTextMeDbContext;
            _env = env;
        }



        [HttpGet]
        public List<Libro> Get()
        {
            return _ctx.Libro.ToList();
        }



        [HttpGet("GetLibro/{id}")]
        public Libro GetLibro(int id)
        {
            return _ctx.Libro.Find(id);
        }



        [HttpPost("AgregarLibro")]
        public IActionResult AgregarLibro([FromBody] Libro libro)
        {
            try {
                var parameters = new[] {
                    new SqlParameter("@titulo", SqlDbType.VarChar) {Value = libro.titulo },
                    new SqlParameter("@anio", SqlDbType.Int) {Value = libro.anio },
                    new SqlParameter("@autor", SqlDbType.VarChar) {Value = libro.autor },
                    new SqlParameter("@stock", SqlDbType.Int) {Value = libro.stock },
                    new SqlParameter("@precio", SqlDbType.Decimal) {Value = libro.precio }
                };

                int result = _ctx.SqlExec("LIBRERIA.AGREGAR_LIBRO", parameters, _env);
                var mensaje = result > 0 ? "Libro agregado con exito!" : "Libro no se pudo agregar";

                return Ok(new { message = mensaje });
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }



        [HttpPut("ModificarLibro")]
        public IActionResult ModificarLibro([FromBody] Libro libro)
        {
            try {
                var parameters = new[] {
                    new SqlParameter("@libroId", SqlDbType.VarChar) {Value = libro.libroId },
                    new SqlParameter("@titulo", SqlDbType.VarChar) {Value = libro.titulo },
                    new SqlParameter("@anio", SqlDbType.Int) {Value = libro.anio },
                    new SqlParameter("@autor", SqlDbType.VarChar) {Value = libro.autor },
                    new SqlParameter("@stock", SqlDbType.Int) {Value = libro.stock },
                    new SqlParameter("@precio", SqlDbType.Decimal) {Value = libro.precio }
                };

                int result = _ctx.SqlExec("LIBRERIA.MODIFICAR_LIBRO", parameters, _env);
                var mensaje = result > 0 ? "Libro modificado con exito!" : "Libro no se pudo modificar";

                return Ok(new { message = mensaje });
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }




        [HttpDelete("EliminarLibro/{id}")]
        public IActionResult EliminarLibro(int id)
        {
            try {
                var parameters = new[] {
                    new SqlParameter("@libroId", SqlDbType.VarChar) {Value = id }
                };

                int result = _ctx.SqlExec("LIBRERIA.ELIMINAR_LIBRO", parameters, _env);
                var mensaje = result > 0 ? "Libro eliminado con exito!" : "Libro no se pudo eliminado";

                return Ok(new { message = mensaje });
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
