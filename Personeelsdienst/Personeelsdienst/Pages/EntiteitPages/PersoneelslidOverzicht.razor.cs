using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Personeelsdienst.Models;
using Personeelsdienst.Models.IRepositories;
using Personeelsdienst.Shared;
using System.Collections.Generic;

namespace Personeelsdienst.Pages.EntiteitPages
{
    public class PersoneelslidOverzichtBase : ComponentBase
    {
        [Parameter]
        public string Text { get; set; }
        [Inject]
        protected IEntiteitRepository EntiteitRepository { get; set; }
        [Inject]
        protected IPersoneelslidRepository PersoneelslidRepository { get; set; }
        [Inject]
        protected IHttpContextAccessor HttpContextAccessor { get; set; }
        [Inject]
        protected UserManager<IdentityUser> UserManager { get; set; }
        [Inject]
        public IModalService Modal { get; set; }
        [Inject]
        protected NavigationManager Navigation { get; set; }
        protected IList<Personeelslid> Personeelsleden { get; set; }
        protected Models.Entiteit _entiteit;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            _entiteit = EntiteitRepository.GetByEmail(UserManager.GetUserName(HttpContextAccessor.HttpContext.User));
            Personeelsleden = PersoneelslidRepository.GetByEntiteit(_entiteit.Id);
        }

        protected async void VerwijderPersoneelslid(MouseEventArgs e, long id)
        {
            var confirmModal = Modal.Show<ConfirmDelete>("Personeelslid verwijderen");
            var result = await confirmModal.Result;

            if (!result.Cancelled)
            {
                PersoneelslidRepository.Verwijder(id);
                Navigation.NavigateTo("/Entiteit/Personeelslid/Overzicht/Delete");
            }
        }
    }
}
