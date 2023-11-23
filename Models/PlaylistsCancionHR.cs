using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoSoundify.Models
{
    public class PlaylistsCancionHR
    {
        public int IdPlaylist { get; set; }
       
        public int IdCancion { get; set; }
       
        public DateTime FechaAgregado { get; set; }
    }
}
