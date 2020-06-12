using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Personeelsdienst.Models.IRepositories;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;

namespace Personeelsdienst.Pages.Admin
{
    public class EntiteitToevoegenBase : ComponentBase
    {
        [Inject]
        protected IEntiteitRepository EntiteitRepository { get; set; }
        [Inject]
        protected UserManager<IdentityUser> UserManager { get; set; }
        [Inject]
        protected NavigationManager Navigation { get; set; }

        protected EditContext _editContext;
        protected EntiteitFormModel _entiteitFormModel;
        protected bool _formInvalid = true;

        protected bool _userAlreadyExists = false;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            _entiteitFormModel = new EntiteitFormModel();
            _editContext = new EditContext(_entiteitFormModel);
            _editContext.OnFieldChanged += HandleFieldChanged;
        }
        protected async void HandleValidSubmit()
        {
            if (UserManager.Users.FirstOrDefault(u => u.Email.ToLower().Equals(_entiteitFormModel.Email)) is null)
            {
                _userAlreadyExists = false;
                Models.Entiteit entiteit = new Models.Entiteit(_entiteitFormModel.Entiteitsnaam, _entiteitFormModel.Email);
                EntiteitRepository.VoegToe(entiteit);

                IdentityUser entiteitUser = new IdentityUser { UserName = entiteit.Email, Email = entiteit.Email };
                await UserManager.CreateAsync(entiteitUser, _entiteitFormModel.Password);
                await UserManager.AddClaimAsync(entiteitUser, new Claim(ClaimTypes.Role, "entiteit"));

                Navigation.NavigateTo("/Admin/Entiteit/Overzicht/Create");
            }
            else
            {
                _userAlreadyExists = true;
            }
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
        protected class EntiteitFormModel
        {
            [Required(ErrorMessage = "Entiteitsnaam is verplicht")]
            [MinLength(1, ErrorMessage = "Entiteitsnaam is verplicht")]
            public string Entiteitsnaam { get; set; }
            [Required(ErrorMessage = "Email is verplicht")]
            [EmailAddress(ErrorMessage = "Email is verplicht")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Wachtwoord is verplicht")]
            public string Password { get; set; }
        }
        #endregion
    }
}
