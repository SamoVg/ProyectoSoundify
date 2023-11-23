using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoSoundify.Models
{
    public class PlaylistHR
    {
        public int IdPlaylist { get; set; }
        public int IdUser { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string NombrePlaylist { get; set; } = null!;
    }
}
