using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Personeelsdienst.Models.IRepositories;
using Personeelsdienst.Shared;
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
        public IModalService Modal { get; set; }
        [Inject]
        public NavigationManager Navigation { get; set; }
        protected IList<Models.Entiteit> Entiteiten => EntiteitRepository.GetAll();

        protected async void VerwijderEntiteit(MouseEventArgs e, long id)
        {
            var confirmModal = Modal.Show<ConfirmDelete>("Entiteit verwijderen");
            var result = await confirmModal.Result;

            if (!result.Cancelled)
            {
                EntiteitRepository.Verwijder(id);
                Navigation.NavigateTo("/Admin/Entiteit/Overzicht/Delete");
            }
        }
    }
}
