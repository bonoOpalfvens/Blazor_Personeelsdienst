using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Personeelsdienst.Models;
using Personeelsdienst.Models.IRepositories;
using System.Collections.Generic;

namespace Personeelsdienst.Pages.Entiteit
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
        protected NavigationManager Navigation { get; set; }
        protected IList<Personeelslid> Personeelsleden { get; set; }
        protected Models.Entiteit _entiteit;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            _entiteit = EntiteitRepository.GetByEmail(UserManager.GetUserName(HttpContextAccessor.HttpContext.User));
            Personeelsleden = PersoneelslidRepository.GetByEntiteit(_entiteit.Id);
        }

        protected void VerwijderPersoneelslid(MouseEventArgs e, long id)
        {
            PersoneelslidRepository.Verwijder(id);
            Navigation.NavigateTo("/Entiteit/Personeelslid/Overzicht/Delete");
        }
    }
}
