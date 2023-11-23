using ProyectoSoundify.Models.dbModels;
namespace ProyectoSoundify.ViewModels
{
    public class LoginViewModelcs
    {
        public ApplicationUser User { get; set; } = null!;
        public string ReturnUrl { get; set; } = null!;
    }
}
