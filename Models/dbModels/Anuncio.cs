using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoSoundify.Models.dbModels
{
    public partial class Anuncio
    {
        [Key]
        public int IdAnuncio { get; set; }
        [StringLength(50)]
        public string TituloAnuncio { get; set; } = null!;
        [StringLength(256)]
        public string RutaImgAnuncio { get; set; } = null!;
        public int IdUsuario { get; set; }

        [ForeignKey("IdUsuario")]
        [InverseProperty("Anuncios")]
        public virtual ApplicationUser IdUsuarioNavigation { get; set; } = null!;
    }
}
