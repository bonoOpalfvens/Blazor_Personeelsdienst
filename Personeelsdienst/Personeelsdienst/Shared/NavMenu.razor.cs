using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Personeelsdienst.Models;
using Personeelsdienst.Models.IRepositories;
using System.Linq;
using System.Security.Claims;

namespace Personeelsdienst.Shared
{
    public class NavMenuBase : ComponentBase
    {
        [Inject]
        public SignInManager<IdentityUser> SignInManager { get; set; }
        [Inject]
        public UserManager<IdentityUser> UserManager { get; set; }
        [Inject]
        protected IBeheerderRepository BeheerderRepository { get; set; }
        public bool IsAdmin() => SignInManager.Context.User.Claims.Any(c => c.Type.Equals(ClaimTypes.Role) && c.Value.Equals("admin"));
        public bool IsBeheerder() => SignInManager.Context.User.Claims.Any(c => c.Type.Equals(ClaimTypes.Role) && c.Value.Equals("beheerder"));
        public bool IsEntiteit() => SignInManager.Context.User.Claims.Any(c => c.Type.Equals(ClaimTypes.Role) && c.Value.Equals("entiteit"));

        public Beheerder Beheerder() => BeheerderRepository.GetByEmail(UserManager.GetUserName(SignInManager.Context.User));
    }
}
