using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Personeelsdienst.Models;
using Personeelsdienst.Models.IRepositories;
using Personeelsdienst.Shared;
using System.Collections.Generic;
using System.Linq;

namespace Personeelsdienst.Pages.Admin
{
    public class BeheerderBewerkenBase : ComponentBase
    {
        [Parameter]
        public string Id { get; set; }
        [Inject]
        protected IEntiteitRepository EntiteitRepository { get; set; }
        [Inject]
        protected IBeheerderRepository BeheerderRepository { get; set; }
        [Inject]
        protected NavigationManager Navigation { get; set; }
        [Inject]
        protected IModalService Modal { get; set; }
        protected IList<Entiteit> Entiteiten;

        protected EditContext _editContext;
        protected BeheerderFormModel _beheerderFormModel;
        protected Beheerder _beheerder;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Entiteiten = EntiteitRepository.GetAll();
            _beheerder = BeheerderRepository.GetById(long.Parse(Id));
            if (_beheerder is null) Navigation.NavigateTo("/Error");

            _beheerderFormModel = new BeheerderFormModel(Entiteiten.ToList(), _beheerder.Entiteiten.Select(e => e.Entiteit).ToList()) { Email = _beheerder.Email };
            _editContext = new EditContext(_beheerderFormModel);
        }

        protected void HandleValidSubmit()
        {
            _beheerder.Entiteiten = _beheerderFormModel.Entiteiten.Where(e => e.BoolProperty).Select(e => new EntiteitBeheerder { EntiteitId = e.Entiteit.Id, BeheerderId = _beheerder.Id }).ToList();
            BeheerderRepository.SaveChanges();
            Navigation.NavigateTo("/Admin/Beheerder/Overzicht/Edit");
        }

        #region EventHandlers
        protected void VoegEntiteitToe(MouseEventArgs e, Entiteit entiteit)
        {
            _beheerderFormModel.Entiteiten.First(e => e.Entiteit.Equals(entiteit)).BoolProperty = true;
        }
        protected void VerwijderEntiteit(MouseEventArgs e, Entiteit entiteit)
        {
            _beheerderFormModel.Entiteiten.First(e => e.Entiteit.Equals(entiteit)).BoolProperty = false;
        }
        protected void VerwijderBeheerder(MouseEventArgs e)
        {
            /*
            var confirmModal = Modal.Show<ConfirmDelete>("Beheerder verwijderen");
            var result = confirmModal.Result;

            if (!result.Result.Cancelled)
            {
                BeheerderRepository.Verwijder(_beheerder.Id);
                Navigation.NavigateTo("/Admin/Beheerder/Overzicht/Delete");
            }
            */
            BeheerderRepository.Verwijder(_beheerder.Id);
            Navigation.NavigateTo("/Admin/Beheerder/Overzicht/Delete");
        }
        #endregion

        #region DataModel
        protected class BeheerderFormModel
        {
            public string Email { get; set; }
            public List<EntiteitBool> Entiteiten { get; set; }

            public BeheerderFormModel(List<Entiteit> entiteiten, List<Entiteit> entiteitenBeheerder)
            {
                Entiteiten = entiteiten.Select(e => new EntiteitBool { Entiteit = e, BoolProperty = entiteitenBeheerder.Contains(e) }).ToList();
            }

            public class EntiteitBool
            {
                public Entiteit Entiteit { get; set; }
                public bool BoolProperty { get; set; }
            }
        }
        #endregion
    }
}
