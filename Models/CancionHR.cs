using ProyectoSoundify.Models.dbModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json.Serialization;
using MessagePack;
using System.Runtime.Serialization;

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

        public IFormFile? ImagenArchivo { get; set; }

        [JsonIgnore]
        [IgnoreMember]
        [IgnoreDataMember]

        public SelectList? Categoria { get; set; }

    }
}
