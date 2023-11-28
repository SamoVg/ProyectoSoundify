using Microsoft.AspNetCore.Identity;

namespace ProyectoSoundify.ViewModels
{
    public class UserRolesViewModel
    {
        public RoleManager<IdentityRole> role { get; set; } = null!;
        public string RoleId { get; set; } = null!;
        public string RoleName { get; set; } = null!;
        public bool IsSelected { get; set; }
    }
}
