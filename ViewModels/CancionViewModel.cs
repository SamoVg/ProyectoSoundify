using ProyectoSoundify.Models.dbModels;

namespace ProyectoSoundify.ViewModels
{
    public class CancionViewModel
    {
        public ApplicationUser ApplicationUser { get; set; } = null!;
        public Cancion Cancion { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
        public string ReturnUrl { get; set; } = null!;
    }
}
