using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaApi.Models {
    [Table("CLIENTE")]
    public class Cliente {
        [Key, Column("clienteId")]
        public int clienteId { get; set; }
        [Column("nombre")]
        public String nombre { get; set; }
        [Column("telefono")]
        public String telefono { get; set; }
        [Column("direccion")]
        public String direccion { get; set; }
        [Column("activo")]
        public Boolean? activo { get; set; }
        [Column("fechaCrea")]
        public DateTime? fechaCrea { get; set; }
        [Column("fechaModifica")]
        public DateTime? fechaModifica { get; set; }
    }
}
