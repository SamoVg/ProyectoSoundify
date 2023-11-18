using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Soundify.Models.dbModels
{
    [Table("Reproduccion")]
    public partial class Reproduccion
    {
        [Column(TypeName = "datetime")]
        public DateTime FechaReproduccion { get; set; }
        public int IdCancion { get; set; }
        public int IdUsuario { get; set; }
        [Key]
        public int IdReproduccion { get; set; }

        [ForeignKey("IdCancion")]
        [InverseProperty("Reproduccions")]
        public virtual Cancion IdCancionNavigation { get; set; } = null!;
        [ForeignKey("IdUsuario")]
        [InverseProperty("Reproduccions")]
        public virtual ApplicationUser IdUsuarioNavigation { get; set; } = null!;
    }
}
