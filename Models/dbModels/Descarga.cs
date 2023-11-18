using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Soundify.Models.dbModels
{
    public partial class Descarga
    {
        public int IdCancion { get; set; }
        public int IdUser { get; set; }
        [Key]
        public int IdDescarga { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime FechaDescarga { get; set; }

        [ForeignKey("IdCancion")]
        [InverseProperty("Descargas")]
        public virtual Cancion IdCancionNavigation { get; set; } = null!;
        [ForeignKey("IdUser")]
        [InverseProperty("Descargas")]
        public virtual ApplicationUser IdUserNavigation { get; set; } = null!;
    }
}
