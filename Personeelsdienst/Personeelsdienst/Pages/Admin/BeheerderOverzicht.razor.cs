using Microsoft.AspNetCore.Components;
using Personeelsdienst.Models;
using Personeelsdienst.Models.IRepositories;
using System.Collections.Generic;

namespace Personeelsdienst.Pages.Admin
{
    public class BeheerderOverzichtBase : ComponentBase
    {
        [Inject]
        public IBeheerderRepository BeheerderRepository { get; set; }
        public IList<Beheerder> Beheerders => BeheerderRepository.GetAll();
    }
}
