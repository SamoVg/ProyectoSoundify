using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Soundify.Models.dbModels
{
    public partial class Categorium
    {
        public Categorium()
        {
            Cancions = new HashSet<Cancion>();
        }

        [Key]
        public int IdCategoria { get; set; }
        [StringLength(50)]
        public string NombreCategoria { get; set; } = null!;

        [InverseProperty("IdCategoriaNavigation")]
        public virtual ICollection<Cancion> Cancions { get; set; }
    }
}
