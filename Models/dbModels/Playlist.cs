using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoSoundify.Models.dbModels
{
    [Table("Playlist")]
    public partial class Playlist
    {
        public Playlist()
        {
            PlaylistCancions = new HashSet<PlaylistCancion>();
        }

        [Key]
        public int IdPlaylist { get; set; }
        public int IdUser { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime FechaCreacion { get; set; }
        [StringLength(50)]
        public string NombrePlaylist { get; set; } = null!;

        [ForeignKey("IdUser")]
        [InverseProperty("Playlists")]
        public virtual ApplicationUser IdUserNavigation { get; set; } = null!;
        [InverseProperty("IdPlaylistNavigation")]
        public virtual ICollection<PlaylistCancion> PlaylistCancions { get; set; }
    }
}
