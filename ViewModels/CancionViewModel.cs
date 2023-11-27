using ProyectoSoundify.Models.dbModels;

namespace ProyectoSoundify.ViewModels
{
    public class CancionViewModel
    {
        public IEnumerable<Cancion> Cancion { get; set; } = null!;
        public IEnumerable<FavoritoViewModel> Favorito { get; set;} = null!;
        public ApplicationUser User { get; set; } = null!;
        public string ReturnUrl { get; set; } = null!;
    }
}
