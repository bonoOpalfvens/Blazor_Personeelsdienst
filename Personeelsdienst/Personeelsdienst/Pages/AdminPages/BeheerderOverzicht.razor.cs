using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Personeelsdienst.Models;
using Personeelsdienst.Models.IRepositories;
using Personeelsdienst.Shared;
using System.Collections.Generic;

namespace Personeelsdienst.Pages.AdminPages
{
    public class BeheerderOverzichtBase : ComponentBase
    {
        [Parameter]
        public string Text { get; set; }
        [Inject]
        public IBeheerderRepository BeheerderRepository { get; set; }
        [Inject]
        public UserManager<IdentityUser> UserManager { get; set; }
        [Inject]
        public NavigationManager Navigation { get; set; }
        [Inject]
        public IModalService Modal { get; set; }
        public IList<Beheerder> Beheerders => BeheerderRepository.GetAll();

        protected async void VerwijderBeheerder(MouseEventArgs e, long id)
        {
            var confirmModal = Modal.Show<ConfirmDelete>("Beheerder verwijderen");
            var result = await confirmModal.Result;

            if (!result.Cancelled)
            {
                BeheerderRepository.Verwijder(id);
                Navigation.NavigateTo("/Admin/Beheerder/Overzicht/Delete");
            }
        }
    }
}
