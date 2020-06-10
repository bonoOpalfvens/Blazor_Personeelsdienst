using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;

namespace Personeelsdienst.Pages
{
    public class IndexBase : ComponentBase
    {
        [Inject]
        NavigationManager NavigationManager { get; set; }
        [Inject]
        public SignInManager<IdentityUser> SignInManager { get; set; }

        private bool IsAdmin() => SignInManager.Context.User.Claims.Any(c => c.Type.Equals(ClaimTypes.Role) && c.Value.Equals("admin"));
        private bool IsBeheerder() => SignInManager.Context.User.Claims.Any(c => c.Type.Equals(ClaimTypes.Role) && c.Value.Equals("beheerder"));
        private bool IsEntiteit() => SignInManager.Context.User.Claims.Any(c => c.Type.Equals(ClaimTypes.Role) && c.Value.Equals("entiteit"));

        protected override void OnInitialized()
        {
            if (IsEntiteit()) NavigationManager.NavigateTo("/Entiteit");
            else if (IsBeheerder()) NavigationManager.NavigateTo("/Beheerder");
            else if (IsAdmin()) NavigationManager.NavigateTo("/Admin");
            else NavigationManager.NavigateTo("/Error");
        }
    }
}
