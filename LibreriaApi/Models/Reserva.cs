using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaApi.Models {
    [Table("RESERVA")]
    public class Reserva {
        [Key, Column("reservaId")]
        public int reservaId { get; set; }
        [Column("clienteId")]
        public int? clienteId { get; set; }
        [Column("libroId")]
        public int? libroId { get; set; }
        [Column("estado")]
        public String estado { get; set; }
        [Column("fechaCrea")]
        public DateTime? fechaCrea { get; set; }
        [Column("fechaModifica")]
        public DateTime? fechaModifica { get; set; }
    }
}
