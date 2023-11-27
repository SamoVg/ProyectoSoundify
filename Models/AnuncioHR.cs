using System.ComponentModel.DataAnnotations;

namespace ProyectoSoundify.Models
{
    public class AnuncioHR
    {
        
        public int IdAnuncio { get; set; }
        public string TituloAnuncio { get; set; } = null!;
        public string RutaImgAnuncio { get; set; } = null!;
        public int IdUsuario { get; set; }
        public IFormFile? ImagenArchivo { get; set; }
    }
}
