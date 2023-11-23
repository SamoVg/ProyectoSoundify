using ProyectoSoundify.Models.dbModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoSoundify.Models
{
    public class CancionHR
    {
        public int IdCancion { get; set; }
        
        public string Nombre { get; set; } = null!;
        
        public string? Descripcion { get; set; }
        public TimeSpan Duracion { get; set; }
        public int IdCategoria { get; set; }
        
        public string? RutaImg { get; set; }
        
        public DateTime FechaSubida { get; set; }
        public int IdUsuario { get; set; }

       
    }
}
