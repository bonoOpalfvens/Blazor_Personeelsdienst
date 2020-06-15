using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Personeelsdienst.Models;
using Personeelsdienst.Models.IRepositories;
using System.Collections.Generic;
using System.Linq;

namespace Personeelsdienst.Pages.BeheerderPages
{
    public class AfwezigheidOverzichtBase : ComponentBase
    {
        [Parameter]
        public string Text { get; set; }
        [Parameter]
        public string Id { get; set; }
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
        protected Beheerder _beheerder;
        protected long _id { get { return long.Parse(Id); } set { Id = value.ToString(); } }
        protected Models.Entiteit _entiteit { get { return _beheerder.Entiteiten.First(e => e.Entiteit.Id.Equals(_id)).Entiteit; } }
        protected List<Afwezigheid> Afwezigheden() => AfwezigheidRepository.GetByEntiteit(_id).ToList();

        protected override void OnInitialized()
        {
            base.OnInitialized();
            _beheerder = BeheerderRepository.GetByEmail(UserManager.GetUserName(HttpContextAccessor.HttpContext.User));
            if (Id is null) _id = _beheerder.Entiteiten.OrderBy(e => e.Entiteit.Entiteitsnaam).FirstOrDefault().EntiteitId;
        }
    }
}
