using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Personeelsdienst.Models;
using Personeelsdienst.Models.Enums;
using Personeelsdienst.Models.IRepositories;
using Personeelsdienst.Shared;
using System;
using System.ComponentModel.DataAnnotations;

namespace Personeelsdienst.Pages.Entiteit
{
    public class AfwezigheidBewerkenBase : ComponentBase
    {
        [Parameter]
        public string Id { get; set; }
        [Inject]
        protected IEntiteitRepository EntiteitRepository { get; set; }
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
        protected AfwezigheidFormModel _afwezigheidFormModel;
        protected bool _formInvalid;
        protected Models.Entiteit _entiteit;
        protected Afwezigheid _afwezigheid;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            _entiteit = EntiteitRepository.GetByEmail(UserManager.GetUserName(HttpContextAccessor.HttpContext.User));
            _afwezigheid = AfwezigheidRepository.GetById(long.Parse(Id));
            if (_afwezigheid is null) Navigation.NavigateTo("/Error");

            _afwezigheidFormModel = new AfwezigheidFormModel(_afwezigheid);
            _editContext = new EditContext(_afwezigheidFormModel);
            _editContext.OnFieldChanged += HandleFieldChanged;
        }
        protected void HandleValidSubmit()
        {
            _afwezigheid.BeginDatum = _afwezigheidFormModel.BeginDatum;
            _afwezigheid.EindDatum = _afwezigheidFormModel.EindDatum;
            _afwezigheid.RedenAfwezigheid = _afwezigheidFormModel.RedenAfwezigheid;
            _afwezigheid.Vervanger = _afwezigheidFormModel.Vervanger;
            _afwezigheid.BewijsOk = false;
            _afwezigheid.Verwerkt = false;
            AfwezigheidRepository.SaveChanges();
            Navigation.NavigateTo("/Entiteit/Afwezigheid/Overzicht/Edit");
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
        protected async void VerwijderAfwezigheid(MouseEventArgs e)
        {
            var confirmModal = Modal.Show<ConfirmDelete>("Afwezigheid verwijderen");
            var result = await confirmModal.Result;

            if (!result.Cancelled)
            {
                AfwezigheidRepository.Verwijder(_afwezigheid.Id);
                Navigation.NavigateTo("/Entiteit/Afwezigheid/Overzicht/Delete");
            }
        }
        #endregion

        #region DataModel
        protected class AfwezigheidFormModel
        {
            public Personeelslid Personeelslid { get; set; }
            [Required(ErrorMessage = "Reden van afwezigheid is verplicht")]
            public RedenAfwezigheid RedenAfwezigheid { get; set; }
            public string RedenAfwezigheidString { get; set; }
            [Required(ErrorMessage = "Begindatum is verplicht")]
            public DateTime BeginDatum { get; set; }
            public DateTime? EindDatum { get; set; }
            public string Vervanger { get; set; }
            //public byte[] Bijlage { get; set; }

            public AfwezigheidFormModel(Afwezigheid afwezigheid)
            {
                Personeelslid = afwezigheid.Personeelslid;
                RedenAfwezigheid = afwezigheid.RedenAfwezigheid;
                RedenAfwezigheidString = Enum.GetName(typeof(RedenAfwezigheid), afwezigheid.RedenAfwezigheid);
                BeginDatum = afwezigheid.BeginDatum;
                EindDatum = afwezigheid.EindDatum;
                Vervanger = afwezigheid.Vervanger;
            }
        }
        #endregion
    }
}
