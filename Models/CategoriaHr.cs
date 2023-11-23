using System.ComponentModel.DataAnnotations;

namespace ProyectoSoundify.Models
{
    public class CategoriaHr
    {
        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; } = null!;
    }
}
