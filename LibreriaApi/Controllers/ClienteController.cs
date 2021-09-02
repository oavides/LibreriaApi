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
    public class ClienteController : ControllerBase
    {

        private readonly LibreriaDbContext _ctx;
        private readonly IWebHostEnvironment _env;

        public ClienteController(LibreriaDbContext iTextMeDbContext, IWebHostEnvironment env) {
            _ctx = iTextMeDbContext;
            _env = env;
        }

        [HttpGet]
        public List<Cliente> Get()
        {
            return _ctx.Cliente.ToList();
        }




        [HttpGet("GetCliente/{id}")]
        public Cliente GetCliente(int id)
        {
            return _ctx.Cliente.Find(id);
        }

 


        [HttpPost("AgregarCliente")]
        public IActionResult AgregarCliente([FromBody] Cliente cliente)
        {
            try {
                var parameters = new[] {
                    new SqlParameter("@nombre", SqlDbType.VarChar) {Value = cliente.nombre },
                    new SqlParameter("@telefono", SqlDbType.VarChar) {Value = cliente.telefono },
                    new SqlParameter("@direccion", SqlDbType.VarChar) {Value = cliente.direccion }
                };

                int result = _ctx.SqlExec("LIBRERIA.AGREGAR_CLIENTE", parameters, _env);
                var mensaje = result > 0 ? "Cliente agregado con exito!" : "Cliente no se pudo agregar";

                return Ok(new { message = mensaje });
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }




        [HttpPut("ModificarCliente")]
        public IActionResult ModificarCliente([FromBody] Cliente cliente)
        {
            try {
                var parameters = new[] {
                    new SqlParameter("@clientId", SqlDbType.Int) {Value = cliente.clienteId },
                    new SqlParameter("@nombre", SqlDbType.VarChar) {Value = cliente.nombre },
                    new SqlParameter("@telefono", SqlDbType.VarChar) {Value = cliente.telefono },
                    new SqlParameter("@direccion", SqlDbType.VarChar) {Value = cliente.direccion }
                };

                int result = _ctx.SqlExec("LIBRERIA.MODIFICAR_CLIENTE", parameters, _env);
                var mensaje = result > 0 ? "Cliente modificado con exito!" : "Cliente no se pudo modificar";

                return Ok(new { message = mensaje });
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }




        [HttpDelete("EliminarCliente/{id}")]
        public IActionResult EliminarCliente(int id)
        {
            try {
                var parameters = new[] {
                    new SqlParameter("@clientId", SqlDbType.Int) {Value = id }
                };

                int result = _ctx.SqlExec("LIBRERIA.ELIMINAR_CLIENTE", parameters, _env);
                var mensaje = result > 0 ? "Cliente eliminado con exito!" : "Cliente no se pudo eliminar";

                return Ok(new { message = mensaje });
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
