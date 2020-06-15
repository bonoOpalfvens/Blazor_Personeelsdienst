using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Personeelsdienst.Models;
using Personeelsdienst.Models.IRepositories;
using Personeelsdienst.Shared;

namespace Personeelsdienst.Pages.BeheerderPages
{
    public class AfwezigheidBewerkenBase : ComponentBase
    {
        [Parameter]
        public string Id { get; set; }
        [Parameter]
        public string Id2 { get; set; }
        [Inject]
        protected IBeheerderRepository BeheerderRepository { get; set; }
        [Inject]
        protected IAfwezigheidRepository AfwezigheidRepository { get; set; }
        [Inject]
        protected UserManager<IdentityUser> UserManager { get; set; }
        [Inject]
        protected IHttpContextAccessor HttpContextAccessor { get; set; }
        [Inject]
        protected NavigationManager Navigation { get; set; }
        [Inject]
        protected IModalService Modal { get; set; }

        protected EditContext _editContext;
        protected Beheerder _beheerder;
        protected Afwezigheid _afwezigheid;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            _beheerder = BeheerderRepository.GetByEmail(UserManager.GetUserName(HttpContextAccessor.HttpContext.User));
            _afwezigheid = AfwezigheidRepository.GetById(long.Parse(Id2));

            _editContext = new EditContext(_afwezigheid);
        }

        #region EventHandlers
        protected async void VerwijderAfwezigheid(MouseEventArgs e)
        {
            var confirmModal = Modal.Show<ConfirmDelete>("Afwezigheid verwijderen");
            var result = await confirmModal.Result;

            if (!result.Cancelled)
            {
                AfwezigheidRepository.Verwijder(_afwezigheid.Id);
                Navigation.NavigateTo($"/Beheerder/Entiteit/{Id}/Afwezigheid/Overzicht/Delete");
            }
        }

        protected void VerwerkBewijs(bool bewijsOk)
        {
            _afwezigheid.BewijsOk = bewijsOk;
            _afwezigheid.Verwerkt = true;
            AfwezigheidRepository.SaveChanges();
            Navigation.NavigateTo($"/Beheerder/Entiteit/{Id}/Afwezigheid/Overzicht/Edit");
        }
        #endregion
    }
}
