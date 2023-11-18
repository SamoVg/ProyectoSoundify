using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoSoundify.Models.dbModels
{
    [Table("Rol")]
    public partial class Rol
    {
        [Key]
        public int IdRol { get; set; }
        [StringLength(10)]
        public string? NombreRol { get; set; }
    }
}
