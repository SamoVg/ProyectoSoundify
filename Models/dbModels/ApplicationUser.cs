using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Soundify.Models.dbModels
{
    [Table("AspNetUsers")]
    public class ApplicationUser : IdentityUser<int>
    {
        public ApplicationUser()
        {
            Anuncios = new HashSet<Anuncio>();
            Cancions = new HashSet<Cancion>();
            Descargas = new HashSet<Descarga>();
            Playlists = new HashSet<Playlist>();
            Reproduccions = new HashSet<Reproduccion>();
            IdCancions = new HashSet<Cancion>();
        }

       
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
