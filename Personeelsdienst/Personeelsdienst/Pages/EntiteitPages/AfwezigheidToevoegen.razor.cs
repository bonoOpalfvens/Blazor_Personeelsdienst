using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Personeelsdienst.Models;
using Personeelsdienst.Models.Enums;
using Personeelsdienst.Models.IRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Personeelsdienst.Pages.EntiteitPages
{
    public class AfwezigheidToevoegenBase : ComponentBase
    {
        [Inject]
        protected IEntiteitRepository EntiteitRepository { get; set; }
        [Inject]
        protected IAfwezigheidRepository AfwezigheidRepository { get; set; }
        [Inject]
        protected IPersoneelslidRepository PersoneelslidRepository { get; set; }
        [Inject]
        protected IHttpContextAccessor HttpContextAccessor { get; set; }
        [Inject]
        protected UserManager<IdentityUser> UserManager { get; set; }
        [Inject]
        protected NavigationManager Navigation { get; set; }

        protected EditContext _editContext;
        protected AfwezigheidFormModel _afwezigheidFormModel;
        protected bool _formInvalid;
        protected Models.Entiteit _entiteit;
        protected List<Personeelslid> _personeelsleden;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            _entiteit = EntiteitRepository.GetByEmail(UserManager.GetUserName(HttpContextAccessor.HttpContext.User));
            _personeelsleden = PersoneelslidRepository.GetByEntiteit(_entiteit.Id).ToList();
            _afwezigheidFormModel = new AfwezigheidFormModel();
            _editContext = new EditContext(_afwezigheidFormModel);
            _editContext.OnFieldChanged += HandleFieldChanged;
        }
        protected void HandleValidSubmit()
        {
            Afwezigheid afwezigheid = new Afwezigheid(_personeelsleden.FirstOrDefault(p => p.Id.Equals(long.Parse(_afwezigheidFormModel.Personeelslid))), _afwezigheidFormModel.RedenAfwezigheid, _afwezigheidFormModel.BeginDatum) { EindDatum = _afwezigheidFormModel.EindDatum, Vervanger = _afwezigheidFormModel.Vervanger };
            AfwezigheidRepository.VoegToe(afwezigheid);
            Navigation.NavigateTo("/Entiteit/Afwezigheid/Overzicht/Create");
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
        protected class AfwezigheidFormModel
        {
            [Required(ErrorMessage = "Personeelslid is verplicht")]
            public string Personeelslid { get; set; } = "";
            [Required(ErrorMessage = "Reden van afwezigheid is verplicht")]
            public RedenAfwezigheid RedenAfwezigheid { get; set; } = RedenAfwezigheid.Ziekteverlof;
            [Required(ErrorMessage = "Begindatum is verplicht")]
            public DateTime BeginDatum { get; set; } = DateTime.Now;
            public DateTime? EindDatum { get; set; }
            public string Vervanger { get; set; }
            //public byte[] Bijlage { get; set; }
        }
        #endregion
    }
}
