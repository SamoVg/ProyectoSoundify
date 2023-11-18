using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoSoundify.Models.dbModels
{
    [Table("PlaylistCancion")]
    public partial class PlaylistCancion
    {
        [Key]
        public int IdPlaylist { get; set; }
        [Key]
        public int IdCancion { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime FechaAgregado { get; set; }

        [ForeignKey("IdCancion")]
        [InverseProperty("PlaylistCancions")]
        public virtual Cancion IdCancionNavigation { get; set; } = null!;
        [ForeignKey("IdPlaylist")]
        [InverseProperty("PlaylistCancions")]
        public virtual Playlist IdPlaylistNavigation { get; set; } = null!;
    }
}
