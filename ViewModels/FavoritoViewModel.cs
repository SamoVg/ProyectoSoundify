using ProyectoSoundify.Models;
using ProyectoSoundify.Models.dbModels;

namespace ProyectoSoundify.ViewModels
{

    public class FavoritoViewModel
    {
        
        public ApplicationUser User { get; set; } = null!;
        public Cancion Cancion { get; set; }=null!;
        public string ReturnUrl { get; set; } = null!;
    }
}
