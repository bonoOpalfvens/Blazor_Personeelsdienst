using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Personeelsdienst.Models;
using Personeelsdienst.Models.IRepositories;
using System.ComponentModel.DataAnnotations;

namespace Personeelsdienst.Pages.Entiteit
{
    public class PersoneelslidToevoegenBase : ComponentBase
    {
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

        protected EditContext _editContext;
        protected PersoneelslidFormModel _personeelslidFormModel;
        protected bool _formInvalid;
        protected Models.Entiteit _entiteit;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            _entiteit = EntiteitRepository.GetByEmail(UserManager.GetUserName(HttpContextAccessor.HttpContext.User));

            _personeelslidFormModel = new PersoneelslidFormModel();
            _editContext = new EditContext(_personeelslidFormModel);
            _editContext.OnFieldChanged += HandleFieldChanged;
        }
        protected void HandleValidSubmit()
        {
            Personeelslid personeelslid = new Personeelslid(_personeelslidFormModel.Naam, _entiteit);
            PersoneelslidRepository.VoegToe(personeelslid);
            Navigation.NavigateTo("/Entiteit/Personeelslid/Overzicht/Create");
        }

        #region FormBackgroundLogic
        private void HandleFieldChanged(object sender, FieldChangedEventArgs e)
        {
            _formInvalid = !_editContext.Validate();
            StateHasChanged();
        }

        public void Dispose() => _editContext.OnFieldChanged -= HandleFieldChanged;
        #endregion

        #region DataModel
        protected class PersoneelslidFormModel
        {
            [MinLength(1, ErrorMessage = "Naam is verplicht")]
            public string Naam { get; set; }
        }
        #endregion
    }
}
