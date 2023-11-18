using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoSoundify.Models.dbModels
{
    [Table("Usuario")]
    public partial class Usuario
    {
        public Usuario()
        {
            Anuncios = new HashSet<Anuncio>();
            Cancions = new HashSet<Cancion>();
            Descargas = new HashSet<Descarga>();
            Playlists = new HashSet<Playlist>();
            Reproduccions = new HashSet<Reproduccion>();
            IdCancions = new HashSet<Cancion>();
        }

        [Key]
        public int IdUsuario { get; set; }
        [StringLength(50)]
        public string Nombre { get; set; } = null!;
        [StringLength(256)]
        public string Correo { get; set; } = null!;
        [StringLength(50)]
        public string Contrasenia { get; set; } = null!;
        [StringLength(256)]
        public string? RutaImg { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime FechaUnion { get; set; }

        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<Anuncio> Anuncios { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<Cancion> Cancions { get; set; }
        [InverseProperty("IdUserNavigation")]
        public virtual ICollection<Descarga> Descargas { get; set; }
        [InverseProperty("IdUserNavigation")]
        public virtual ICollection<Playlist> Playlists { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<Reproduccion> Reproduccions { get; set; }

        [ForeignKey("IdUsuario")]
        [InverseProperty("IdUsuarios")]
        public virtual ICollection<Cancion> IdCancions { get; set; }
    }
}
