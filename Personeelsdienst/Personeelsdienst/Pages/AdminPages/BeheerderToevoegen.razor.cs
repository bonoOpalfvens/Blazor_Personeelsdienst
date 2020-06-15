using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Personeelsdienst.Models;
using Personeelsdienst.Models.IRepositories;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;

namespace Personeelsdienst.Pages.AdminPages
{
    public class BeheerderToevoegenBase : ComponentBase
    {
        [Inject]
        protected IEntiteitRepository EntiteitRepository { get; set; }
        [Inject]
        protected IBeheerderRepository BeheerderRepository { get; set; }
        [Inject]
        protected UserManager<IdentityUser> UserManager { get; set; }
        [Inject]
        protected NavigationManager Navigation { get; set; }
        protected IList<Models.Entiteit> Entiteiten;

        protected EditContext _editContext;
        protected BeheerderFormModel _beheerderFormModel;
        protected bool _formInvalid = true;

        protected bool _userAlreadyExists = false;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Entiteiten = EntiteitRepository.GetAll();
            _beheerderFormModel = new BeheerderFormModel(Entiteiten.ToList());
            _editContext = new EditContext(_beheerderFormModel);
            _editContext.OnFieldChanged += HandleFieldChanged;
        }

        protected async void HandleValidSubmit()
        {
            if (UserManager.Users.FirstOrDefault(u => u.Email.ToLower().Equals(_beheerderFormModel.Email)) is null)
            {
                _userAlreadyExists = false;

                Beheerder beheerder = new Beheerder(_beheerderFormModel.Email);
                beheerder.Entiteiten.AddRange(_beheerderFormModel.Entiteiten.Where(e => e.BoolProperty.Equals(true)).Select(e => new EntiteitBeheerder { EntiteitId = e.Entiteit.Id, Beheerder = beheerder }));
                BeheerderRepository.VoegToe(beheerder);

                IdentityUser beheerderUser = new IdentityUser { UserName = beheerder.Email, Email = beheerder.Email };
                await UserManager.CreateAsync(beheerderUser);
                await UserManager.AddClaimAsync(beheerderUser, new Claim(ClaimTypes.Role, "beheerder"));

                Navigation.NavigateTo("/Admin/Beheerder/Overzicht/Create");
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

        #region EventHandlers
        protected void VoegEntiteitToe(MouseEventArgs e, Models.Entiteit entiteit)
        {
            _beheerderFormModel.Entiteiten.First(e => e.Entiteit.Equals(entiteit)).BoolProperty = true;
        }
        protected void VerwijderEntiteit(MouseEventArgs e, Models.Entiteit entiteit)
        {
            _beheerderFormModel.Entiteiten.First(e => e.Entiteit.Equals(entiteit)).BoolProperty = false;
        }
        #endregion

        #region DataModel
        protected class BeheerderFormModel
        {
            [Required(ErrorMessage = "Email is verplicht")]
            [EmailAddress(ErrorMessage = "Ongeldige email")]
            public string Email { get; set; }
            [Required(ErrorMessage = "Wachtwoord is verplicht")]
            public string Password { get; set; }
            public List<EntiteitBool> Entiteiten { get; set; }

            public BeheerderFormModel(List<Models.Entiteit> entiteiten)
            {
                Entiteiten = entiteiten.Select(e => new EntiteitBool { Entiteit = e, BoolProperty = false }).ToList();
            }

            public class EntiteitBool
            {
                public Models.Entiteit Entiteit { get; set; }
                public bool BoolProperty { get; set; }
            }
        }
        #endregion
    }
}
