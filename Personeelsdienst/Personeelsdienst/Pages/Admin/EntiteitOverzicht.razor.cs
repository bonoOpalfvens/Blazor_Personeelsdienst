using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Personeelsdienst.Models;
using Personeelsdienst.Models.IRepositories;
using System.Collections.Generic;

namespace Personeelsdienst.Pages.Admin
{
    public class EntiteitOverzichtBase : ComponentBase
    {
        [Parameter]
        public string Text { get; set; }
        [Inject]
        protected IEntiteitRepository EntiteitRepository { get; set; }
        [Inject]
        protected UserManager<IdentityUser> UserManager { get; set; }
        [Inject]
        public NavigationManager Navigation { get; set; }
        protected IList<Entiteit> Entiteiten => EntiteitRepository.GetAll();

        protected void VerwijderEntiteit(MouseEventArgs e, long id)
        {
            /*
            var confirmModal = Modal.Show<ConfirmDelete>("Beheerder verwijderen");
            var result = confirmModal.Result;

            if (!result.Result.Cancelled)
            {
                BeheerderRepository.Verwijder(id);
                Navigation.NavigateTo("/Admin/Beheerder/Overzicht/Delete");
            }
            */
            EntiteitRepository.Verwijder(id);
            Navigation.NavigateTo("/Admin/Entiteit/Overzicht/Delete");
        }
    }
}
