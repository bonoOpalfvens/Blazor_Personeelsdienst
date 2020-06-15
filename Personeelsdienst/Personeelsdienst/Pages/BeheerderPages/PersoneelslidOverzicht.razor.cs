using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Personeelsdienst.Models;
using Personeelsdienst.Models.IRepositories;
using Personeelsdienst.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Personeelsdienst.Pages.BeheerderPages
{
    public class PersoneelslidOverzichtBase : ComponentBase
    {
        [Parameter]
        public string Text { get; set; }
        [Parameter]
        public string Id { get; set; }
        [Inject]
        protected IBeheerderRepository BeheerderRepository { get; set; }
        [Inject]
        protected IPersoneelslidRepository PersoneelslidRepository { get; set; }
        [Inject]
        protected IHttpContextAccessor HttpContextAccessor { get; set; }
        [Inject]
        protected UserManager<IdentityUser> UserManager { get; set; }
        [Inject]
        protected IModalService Modal { get; set; }
        [Inject]
        protected NavigationManager Navigation { get; set; }
        protected IList<Personeelslid> Personeelsleden => PersoneelslidRepository.GetByEntiteit(_entiteit.Id);
        protected long _id { get { return long.Parse(Id); } set { Id = value.ToString(); } }
        protected Beheerder _beheerder;
        protected Entiteit _entiteit { get { return _beheerder.Entiteiten.FirstOrDefault(e => e.EntiteitId.Equals(_id)).Entiteit; } }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            _beheerder = BeheerderRepository.GetByEmail(UserManager.GetUserName(HttpContextAccessor.HttpContext.User));
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
