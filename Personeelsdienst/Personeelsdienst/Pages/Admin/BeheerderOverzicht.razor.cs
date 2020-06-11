using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Personeelsdienst.Models;
using Personeelsdienst.Models.IRepositories;
using Personeelsdienst.Shared;
using System.Collections.Generic;

namespace Personeelsdienst.Pages.Admin
{
    public class BeheerderOverzichtBase : ComponentBase
    {
        [Parameter]
        public string Text { get; set; }
        [Inject]
        public IBeheerderRepository BeheerderRepository { get; set; }
        [Inject]
        public NavigationManager Navigation { get; set; }
        [Inject]
        public IModalService Modal { get; set; }
        public IList<Beheerder> Beheerders => BeheerderRepository.GetAll();

        protected void VerwijderBeheerder(MouseEventArgs e, long id)
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
            BeheerderRepository.Verwijder(id);
            Navigation.NavigateTo("/Admin/Beheerder/Overzicht/Delete");
        }
    }
}
