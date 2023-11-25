using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoSoundify.Models
{
    public class ApplicationUserHR
    {
        public string? RutaImg { get; set; }
        public DateTime FechaUnion { get; set; }
    }
}
