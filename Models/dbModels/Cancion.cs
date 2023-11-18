using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoSoundify.Models.dbModels
{
    [Table("Cancion")]
    public partial class Cancion
    {
        public Cancion()
        {
            Descargas = new HashSet<Descarga>();
            PlaylistCancions = new HashSet<PlaylistCancion>();
            Reproduccions = new HashSet<Reproduccion>();
            IdUsuarios = new HashSet<ApplicationUser>();
        }

        [Key]
        public int IdCancion { get; set; }
        [StringLength(50)]
        public string Nombre { get; set; } = null!;
        [StringLength(50)]
        public string? Descripcion { get; set; }
        public TimeSpan Duracion { get; set; }
        public int IdCategoria { get; set; }
        [StringLength(256)]
        public string? RutaImg { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime FechaSubida { get; set; }
        public int IdUsuario { get; set; }

        [ForeignKey("IdCategoria")]
        [InverseProperty("Cancions")]
        public virtual Categorium IdCategoriaNavigation { get; set; } = null!;
        [ForeignKey("IdUsuario")]
        [InverseProperty("Cancions")]
        public virtual ApplicationUser IdUsuarioNavigation { get; set; } = null!;
        [InverseProperty("IdCancionNavigation")]
        public virtual ICollection<Descarga> Descargas { get; set; }
        [InverseProperty("IdCancionNavigation")]
        public virtual ICollection<PlaylistCancion> PlaylistCancions { get; set; }
        [InverseProperty("IdCancionNavigation")]
        public virtual ICollection<Reproduccion> Reproduccions { get; set; }

        [ForeignKey("IdCancion")]
        [InverseProperty("IdCancions")]
        public virtual ICollection<ApplicationUser> IdUsuarios { get; set; }
    }
}
