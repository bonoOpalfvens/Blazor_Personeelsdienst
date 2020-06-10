using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Personeelsdienst.Pages
{
    public class RoleResolverModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (User.HasClaim(c => c.Type.Equals(ClaimTypes.Role) && c.Value.Equals("admin")))
            {
                return RedirectToPage("/Admin");
            }
            else if (User.HasClaim(c => c.Type.Equals(ClaimTypes.Role) && c.Value.Equals("beheerder")))
            {
                return RedirectToPage("/Beheerder");
            }
            else
            {
                return RedirectToPage("/Entiteit");
            }
        }
    }
}