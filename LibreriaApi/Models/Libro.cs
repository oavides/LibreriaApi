using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaApi.Models {
    [Table("LIBRO")]
    public class Libro {
        [Key, Column("libroId")]
        public int libroId { get; set; }
        [Column("titulo")]
        public String titulo { get; set; }
        [Column("anio")]
        public int anio { get; set; }
        [Column("autor")]
        public String autor { get; set; }
        [Column("stock")]
        public int stock { get; set; }
        [Column("precio")]
        public Decimal? precio { get; set; }
        [Column("activo")]
        public Boolean? activo { get; set; }
        [Column("fechaCrea")]
        public DateTime? fechaCrea { get; set; }
        [Column("fechaModifica")]
        public DateTime? fechaModifica { get; set; }
    }
}
