using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Personeelsdienst.Models;
using Personeelsdienst.Models.IRepositories;
using Personeelsdienst.Shared;
using System.Collections.Generic;

namespace Personeelsdienst.Pages.Entiteit
{
    public class AfwezigheidOverzichtBase : ComponentBase
    {
        [Parameter]
        public string Text { get; set; }
        [Inject]
        protected IEntiteitRepository EntiteitRepository { get; set; }
        [Inject]
        protected IAfwezigheidRepository AfwezigheidRepository { get; set; }
        [Inject]
        protected IHttpContextAccessor HttpContextAccessor { get; set; }
        [Inject]
        protected UserManager<IdentityUser> UserManager { get; set; }
        [Inject]
        public IModalService Modal { get; set; }
        [Inject]
        protected NavigationManager Navigation { get; set; }
        protected IList<Afwezigheid> Afwezigheden { get; set; }
        protected Models.Entiteit _entiteit;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            _entiteit = EntiteitRepository.GetByEmail(UserManager.GetUserName(HttpContextAccessor.HttpContext.User));
            Afwezigheden = AfwezigheidRepository.GetByEntiteit(_entiteit.Id);
        }
        protected async void VerwijderAfwezigheid(MouseEventArgs e, long id)
        {
            var confirmModal = Modal.Show<ConfirmDelete>("Afwezigheid verwijderen");
            var result = await confirmModal.Result;

            if (!result.Cancelled)
            {
                AfwezigheidRepository.Verwijder(id);
                Navigation.NavigateTo("/Entiteit/Afwezigheid/Overzicht/Delete");
            }
        }
    }
}
